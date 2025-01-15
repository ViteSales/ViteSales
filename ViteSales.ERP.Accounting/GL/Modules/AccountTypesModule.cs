using System.Data;
using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.Accounting.GL.Entities;
using ViteSales.ERP.Accounting.GL.Interfaces;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.Accounting.GL.Modules;

[ModuleName("Account Maintenance")]
[ModuleEntities(
    typeof(AccountLedgerEntry),
    typeof(AccountTypes))
]
public class AccountTypesModule: IAccountTypes
{
    public void OnLoad(ServiceProvider provider)
    {
     
    }

    public async Task OnSave(DataTable dataTable)
    {
        
    }

    public void OnChangeEvent(EventChange data)
    {
        throw new NotImplementedException();
    }

    public static List<object> DefaultValues()
    {
        return
        [
            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Funds",
                AccountRootType = "Equity",
                ParentAccount = "",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Current Assets",
                AccountRootType = "Asset",
                ParentAccount = "Funds",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Accounts Receivable",
                AccountRootType = "Asset",
                ParentAccount = "Current Assets",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Debtors",
                AccountRootType = "Asset",
                ParentAccount = "Accounts Receivable",
                AccountType = "Receivable",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Bank Accounts",
                AccountRootType = "Asset",
                ParentAccount = "Current Assets",
                AccountType = "Bank",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Cash",
                AccountRootType = "Asset",
                ParentAccount = "Current Assets",
                AccountType = "Cash",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Cash on Hand",
                AccountRootType = "Asset",
                ParentAccount = "Cash",
                AccountType = "Cash",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Loans Receivable",
                AccountRootType = "Asset",
                ParentAccount = "Current Assets",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Investments",
                AccountRootType = "Asset",
                ParentAccount = "Current Assets",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Deposits",
                AccountRootType = "Asset",
                ParentAccount = "Investments",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Inventory",
                AccountRootType = "Asset",
                ParentAccount = "Current Assets",
                AccountType = "Inventory",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Inventory on Hand",
                AccountRootType = "Asset",
                ParentAccount = "Inventory",
                AccountType = "Inventory",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "VAT Receivable",
                AccountRootType = "Asset",
                ParentAccount = "Current Assets",
                AccountType = "VAT",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Fixed Assets",
                AccountRootType = "Asset",
                ParentAccount = "Funds",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Capital Equipment",
                AccountRootType = "Asset",
                ParentAccount = "Fixed Assets",
                AccountType = "Fixed Asset",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Electronic Equipment",
                AccountRootType = "Asset",
                ParentAccount = "Fixed Assets",
                AccountType = "Fixed Asset",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Furniture and Fixtures",
                AccountRootType = "Asset",
                ParentAccount = "Fixed Assets",
                AccountType = "Fixed Asset",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Office Equipment",
                AccountRootType = "Asset",
                ParentAccount = "Fixed Assets",
                AccountType = "Fixed Asset",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Plant and Machinery",
                AccountRootType = "Asset",
                ParentAccount = "Fixed Assets",
                AccountType = "Fixed Asset",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Buildings",
                AccountRootType = "Asset",
                ParentAccount = "Fixed Assets",
                AccountType = "Fixed Asset",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Accumulated Depreciation",
                AccountRootType = "Asset",
                ParentAccount = "Fixed Assets",
                AccountType = "Accumulated Depreciation",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Retained Earnings",
                AccountRootType = "Equity",
                ParentAccount = "Funds",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Equity Accounts",
                AccountRootType = "Equity",
                ParentAccount = "Funds",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Share Capital",
                AccountRootType = "Equity",
                ParentAccount = "Equity Accounts",
                AccountType = "Share Capital",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Share Premium",
                AccountRootType = "Equity",
                ParentAccount = "Equity Accounts",
                AccountType = "Share Premium",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Current Liabilities",
                AccountRootType = "Liability",
                ParentAccount = "",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Accounts Payable",
                AccountRootType = "Liability",
                ParentAccount = "Current Liabilities",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Creditors",
                AccountRootType = "Liability",
                ParentAccount = "Accounts Payable",
                AccountType = "Payable",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "PAYE Payable",
                AccountRootType = "Liability",
                ParentAccount = "Accounts Payable",
                AccountType = "Tax",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "VAT Payable",
                AccountRootType = "Liability",
                ParentAccount = "Current Liabilities",
                AccountType = "VAT",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "VAT",
                AccountRootType = "Liability",
                ParentAccount = "VAT Payable",
                AccountType = "Tax",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "VAT Exempt",
                AccountRootType = "Liability",
                ParentAccount = "VAT Payable",
                AccountType = "Tax",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Unsecured Loans",
                AccountRootType = "Liability",
                ParentAccount = "Loans Payable",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Bank Overdraft",
                AccountRootType = "Liability",
                ParentAccount = "Loans Payable",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Fixed Deposit Account",
                AccountRootType = "Asset",
                ParentAccount = "Bank Accounts",
                AccountType = "Bank",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Discounts Received",
                AccountRootType = "Income",
                ParentAccount = "Indirect Income",
                AccountType = "Income",
                IsGroup = false
            },
            // Additional UK-specific accounts can be added here

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Expenses",
                AccountRootType = "Expense",
                ParentAccount = "",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Direct Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Expenses",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Cost of Goods Sold",
                AccountRootType = "Expense",
                ParentAccount = "Direct Expenses",
                AccountType = "Cost of Goods Sold",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Inventory Valuation Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Direct Expenses",
                AccountType = "Inventory Valuation",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Inventory Adjustment",
                AccountRootType = "Expense",
                ParentAccount = "Direct Expenses",
                AccountType = "Inventory Adjustment",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Indirect Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Expenses",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Administrative Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Commission on Sales",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Depreciation",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "Depreciation",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Entertainment Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Freight and Forwarding Charges",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "Freight",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Legal Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Marketing Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Miscellaneous Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Office Maintenance Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Office Rent",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Postal Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Print and Stationery",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Rounded Off",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "Rounding Adjustment",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Salary",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Sales Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Telephone Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Travel Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Utility Expenses",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Write Off",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Exchange Gain/Loss",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Gain/Loss on Asset Disposal",
                AccountRootType = "Expense",
                ParentAccount = "Indirect Expenses",
                AccountType = "",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Income",
                AccountRootType = "Income",
                ParentAccount = "",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Direct Income",
                AccountRootType = "Income",
                ParentAccount = "Income",
                AccountType = "Revenue",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Sales",
                AccountRootType = "Income",
                ParentAccount = "Direct Income",
                AccountType = "Revenue",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Service Income",
                AccountRootType = "Income",
                ParentAccount = "Direct Income",
                AccountType = "Revenue",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Indirect Income",
                AccountRootType = "Income",
                ParentAccount = "Income",
                AccountType = "Other Income",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Other Income",
                AccountRootType = "Income",
                ParentAccount = "Indirect Income",
                AccountType = "Other Income",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Equity",
                AccountRootType = "Equity",
                ParentAccount = "",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Share Capital",
                AccountRootType = "Equity",
                ParentAccount = "Equity",
                AccountType = "Share Capital",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Share Premium",
                AccountRootType = "Equity",
                ParentAccount = "Equity",
                AccountType = "Share Premium",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Retained Earnings",
                AccountRootType = "Equity",
                ParentAccount = "Equity",
                AccountType = "Retained Earnings",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Capital Reserves",
                AccountRootType = "Equity",
                ParentAccount = "Equity",
                AccountType = "Reserves",
                IsGroup = false
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Loans Payable",
                AccountRootType = "Liability",
                ParentAccount = "Current Liabilities",
                AccountType = "",
                IsGroup = true
            },

            new AccountTypes
            {
                Id = Guid.NewGuid(),
                AccountName = "Secured Loans",
                AccountRootType = "Liability",
                ParentAccount = "Loans Payable",
                AccountType = "",
                IsGroup = false
            },
        ];
    }
}