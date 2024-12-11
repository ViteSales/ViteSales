using System.Data;
using ViteSales.Data.Contracts;
using ViteSales.Data.Entities;
using ViteSales.Data.Extensions;
using ViteSales.Data.Models;
using ViteSales.Data.Utils;
using ViteSales.ERP.GL.Exceptions;
using ViteSales.ERP.GL.Settings;

namespace ViteSales.ERP.GL.Maintenance.Account;

public class FiscalYearImpl(IViteSalesDataContext ctx)
{
    public (FiscalYearPeriod, FiscalYearPeriod) GetFiscalYearPeriod()
    {
        var activeFiscalYear = GetActiveFiscalYear();
        if (activeFiscalYear == null)
        {
            throw new NoActiveFiscalYearException<string>("No active fiscal year found. Please create a fiscal year.");
        }
        var settings = new SettingsImpl(ctx);
        var fys = settings.Get<SettingsFiscalYear>("FiscalYear");
        if (fys == null)
        {
            throw new NullReferenceException("No fiscal year settings found. Please create fiscal year settings.");
        }

        var startPeriod = new FiscalYearPeriod(fys.FiscalYearStartPeriod);
        var actualPeriod = new FiscalYearPeriod(fys.ActualDataStartPeriod);
        if (actualPeriod.PeriodNo < startPeriod.PeriodNo)
        {
            fys.ActualDataStartPeriod = startPeriod.PeriodNo;
            settings.Save("FiscalYear", fys);
            
            actualPeriod.PeriodNo = startPeriod.PeriodNo;
        }
        return (startPeriod, actualPeriod);
    }
    
    public bool IsOpeningBalanceInYtd()
    {
        var settings = new SettingsImpl(ctx);
        var (startPeriod, actualPeriod) = GetFiscalYearPeriod();

        if (startPeriod.PeriodNo != actualPeriod.PeriodNo)
        {
            var openingBalance = settings.Get<SettingsOpeningBalance>("OpeningBalance");
            if (openingBalance == null)
            {
                throw new NullReferenceException("No opening balance settings found. Please create opening balance settings.");
            }
            return openingBalance.OpeningBalanceInYtd;
        }
        return false;
    }
    
    public List<FiscalYear>? Load()
    {
        var dt = ctx.Resource.FiscalYears.OrderBy(row => row.FromDate).ToList();
        if (dt.Count == 0)
        {
            return _createDefaults();
        }
        return dt;
    }
    
    public FiscalYear? GetActiveFiscalYear()
    {
        var dt = Load();
        if (dt == null) return null;
        var activeRow = dt.FirstOrDefault(row => row.IsActive == "T");
        if (activeRow != null)
        {
            return activeRow;
        }
        return null;
    }

    public DateTime GetStartDateOfFiscalYear(DateTime date)
    {
        var fiscalYearList = Load();
        var (_, actualPeriod) = GetFiscalYearPeriod();
        if (fiscalYearList == null) return actualPeriod.StartDate;
        foreach (var row in fiscalYearList)
        {
            var start = Converter.ToDateTime(row.FromDate);
            var end = Converter.ToDateTime(row.ToDate);
            if (date >= start && date <= end)
            {
                if (Converter.TextToBoolean(row.IsActive))
                {
                    return actualPeriod.StartDate;
                }

                return start;
            }
        }
        return actualPeriod.StartDate;
    }
    
    private bool _isValidTransactionDate(DateTime date)
    {
        if (IsTransactionDateInValidRange(out var startDate, out var endDate))
        {
            return date >= startDate && date <= endDate;
        }
        return false;
    }

    public bool IsTransactionDateInValidRange(out DateTime startDate, out DateTime endDate)
    {
        var fiscalYearList = Load();
        if (fiscalYearList == null || fiscalYearList.Count == 0)
        {
            startDate = DateTime.MinValue;
            endDate = DateTime.MinValue;
            return false;
        }

        startDate = Converter.ToDateTime(fiscalYearList[0].FromDate);
        endDate = Converter.ToDateTime(fiscalYearList[^1].ToDate);
        return true;
    }

    public bool GetFiscalYearRange(DateTime date, out DateTime startDate, out DateTime endDate)
    {
        var fiscalYearList = Load();
        if (fiscalYearList != null)
        {
            foreach (var row in fiscalYearList)
            {
                var start = Converter.ToDateTime(row.FromDate);
                var end = Converter.ToDateTime(row.ToDate);
                if (date >= start && date <= end)
                {
                    startDate = start;
                    endDate = end;
                    return true;
                }
            }
        }
        startDate = DateTime.MinValue;
        endDate = DateTime.MinValue;
        return false;
    }
    
    public bool CheckTransactionDate(DateTime transactionDate)
    {
        var fiscalYearList = Load();
        if (fiscalYearList == null || fiscalYearList.Count == 0)
        {
            throw new NoActiveFiscalYearException<string>("No active fiscal year found. Please create a fiscal year.");
        }
        var settings = new SettingsImpl(ctx);
        var transactionLock = settings.Get<SettingsTransactionLock>("TransactionLock");
        if (transactionLock is { LockTransaction: true })
        {
            throw new TransactionLockException<SettingsTransactionLock>(transactionLock.LockMessage);
        }
        var period = new FiscalYearPeriod(transactionDate);
        var periodLock = ctx.Resource.PeriodLocks
            .FirstOrDefault(row => row.PeriodNo == period.PeriodNo);
        if (periodLock != null && Converter.TextToBoolean(periodLock.Lock))
        {
            var sd = period.StartDate.ToString("yyyy/MM/dd");
            var ed = period.EndDate.ToString("yyyy/MM/dd");
            throw new TransactionLockException<string>($"Transaction is locked for period {sd} - {ed}");
        }
        return _isValidTransactionDate(transactionDate);
    }

    private List<FiscalYear>? _createDefaults()
    {
        var fys = new SettingsImpl(ctx).Get<SettingsFiscalYear>("FiscalYear");
        if (fys != null)
        {
            var fiscalYearStartPeriod = fys.FiscalYearStartPeriod;
            var periodNo = fiscalYearStartPeriod + 11;
            
            var startPeriod = new FiscalYearPeriod(fiscalYearStartPeriod);
            var endPeriod = new FiscalYearPeriod(periodNo);

            ctx.Resource.FiscalYears.Add(new FiscalYear()
            {
                FiscalYearName = $"Fiscal Year {endPeriod.EndDate.Year}",
                FromDate = startPeriod.StartDate,
                ToDate = endPeriod.EndDate,
                IsActive = Converter.BooleanToText(true)
            });
            ctx.Resource.SaveChanges();
            return Load();
        }

        return null;
    }
}

public class FiscalYearPeriod(int periodNo)
{
    private int _periodNo = periodNo;
    
    public int Month => (_periodNo + 11) % 12 + 1;

    public int Year => (_periodNo - 1) / 12;

    public int PeriodNo
    {
        get => _periodNo;
        set => _periodNo = value;
    }

    public DateTime Date => new DateTime(Year, Month, 1);

    public DateTime StartDate => new DateTime(Year, Month, 1);

    public DateTime EndDate => new DateTime(Year, Month, 1).AddMonths(1).AddDays(-1.0);

    public FiscalYearPeriod(DateTime date) : this(date.Year * 12 + date.Month)
    {
    }
}