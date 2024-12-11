using System.Text.Json;
using ViteSales.Data.Contracts;
using ViteSales.Data.Entities;
using ViteSales.Data.Extensions;
using ViteSales.Data.Models;
using ViteSales.ERP.GL.Exceptions;
using ViteSales.ERP.GL.Maintenance.Account;

namespace ViteSales.ERP.GL.Settings;

public class SettingsImpl(IViteSalesDataContext ctx)
{
    private readonly Dictionary<string, string> _defaults = new ()
    {
        { "OpeningBalance", "{\"OpeningBalanceInYtd\":true,\"CanDirectEditArapOpeningBalance\":false,\"LockOpeningBalance\":false}" },
        { "Account", "{\"AccountCodeFormat\":\"AAA-AAAA\",\"AutoDebtorCodeFormat\":\"PPP-F000\",\"AutoCreditorCodeFormat\":\"PPP-F000\",\"SearchAccountMode\":0,\"ShowAccountDescription2InLookupEdit\":false,\"ChartOfAccountOrder\":\"CP,RE,FA,OA,CA,CL,LL,OL,SL,SA,CO,OI,EI,EP,TX,AP\"}" },
        { "AccountBook", "{\"BlockDelete\":false,\"DatabaseGuid\":\"B07FF7B4-4B83-4BCD-A869-A827FFDFE777\",\"DatabaseGuidStatus\":0,\"DatabaseGuidStatus2\":true,\"DatabaseGuidRandomNumber\":\"\",\"DatabaseGuidRandomNumber2\":\"\",\"AllowUpgradeFromOldAutoCount\":true,\"RemoteCreditControlPoolingInterval\":10,\"June1UpdateHasPaid\":true,\"June1UpdateLicenseKey\":\"\"}" },
        { "AdvancedQuotation", "{\"LastSavedQTValidity\":\"\",\"LastSavedQTDeliveryTerm\":\"\",\"LastSavedQTPaymentTerm\":\"\",\"LastSavedQTCC\":\"\",\"LastSavedRecallLastQTValues\":false,\"QuotationCommandFormStartup\":true,\"QuotationDescription\":\"QUOTATION\",\"Quotation5CentsRoundingOption\":0,\"AllowSendApproverNotifEmail\":true,\"AllowSendSenderNotifEmail\":true}" },
        { "AP", "{\"PostingOptionInAPDocuments\":1,\"PostDetailDescriptionInAPDocuments\":true,\"PostingOptionInAPDocumentsDetail\":1,\"AllowDuplicateSupplierDocumentNumber\":true}" },
        { "AR", "{\"PostDetailDescriptionInARDocuments\":true,\"PostingOptionInARDocuments\":1,\"PostingOptionInARDocumentsDetail\":1}" },
        { "ARAP", "{\"UseTodayDateForDefaultKnockOffDate\":false,\"KnockOffDateMustGreaterThanOrEqualToDocumentDate\":true,\"AllowEditAndDeleteARAPDocumentsFromOtherSource\":true,\"StatementWord\":\"We shall be grateful if you will let us have payment as soon as possible. Any discrepancy in this statement must be reported to us in writing within 10 days.\",\"ContactInfoFormat\":\"\",\"WithholdingTaxAccountForPayment\":\"\",\"EnableWithholdingTax\":false,\"AgingFormat\":0,\"AgingFormat2\":0,\"WithholdingTaxAccountForReceipt\":\"\"}" },
        { "ARCreditNote", "{\"ARCreditNoteDescription\":\"\",\"ARCreditNoteCommandFormStartup\":true,\"LastSavedARCreditNoteJournalType\":\"SALES\",\"DefaultARCreditNoteJournalType\":\"SALES\"}" },
        { "ARInvoice", "{\"ARInvoiceCommandFormStartup\":true,\"ARInvoiceDescription\":\"\",\"LastSavedARInvoiceJournalType\":\"SALES\",\"DefaultARInvoiceJournalType\":\"SALES\"}" },
        { "ARReceivePayment", "{\"ARPaymentCommandFormStartup\":true,\"ARPaymentDescription\":\"\",\"ARDiscountCreditNoteFollowORNo\":false,\"PostDocumentDescriptionToARPaymentAccounts\":false,\"PostDocumentDescriptionToARPaymentBankAccounts\":false}" },
        { "CancelSO", "{\"CancelSODescription\":\"CANCEL S/O\",\"CancelSOCommandFormStartup\":true}" },
        { "CashBook", "{\"CashBookCommandFormStartup\":true,\"PostDetailDescriptionInCashBookEntry\":true,\"EnableCashBookEntryGridDescriptionMRU\":true,\"EnableAutoUpdateFirstPaymentAmountInCashBookEntry\":true,\"RCHQDescription\":\"RETURNED CHEQUE\"}" },
        { "CashSale", "{\"DefaultDebtorCodeInCashSale\":\"\",\"ShowBigNetTotal\":true,\"AllowCreditSale\":true,\"CheckCreditControlInCashSale\":true,\"CashSaleDescription\":\"CASH SALE\",\"CashSaleCommandFormStartup\":true,\"CashSale5CentsRoundingOption\":0,\"CashSaleORNoFollowCSNo\":true,\"UsePrice2InCashSale\":false,\"CopyAccountFromDeliveryOrderWhenTransferToCashSale\":false,\"DefaultCashSaleJournalType\":\"SALES\"}" },
        { "CreditNote", "{\"CreditNoteCommandFormStartup\":true,\"CreditNote5CentsRoundingOption\":0,\"CreditNoteDescription\":\"CREDIT NOTE\"}" },
        { "Decimal", "{\"QuantityDecimal\":0,\"SalesPriceDecimal\":2,\"PurchasePriceDecimal\":2,\"CostDecimal\":4,\"PercentDecimal\":2,\"CurrencyDecimal\":2,\"CurrencyNegativePattern\":1,\"CurrencyRateDecimal\":6,\"WeightDecimal\":2,\"VolumeDecimal\":2,\"MemberPointDecimal\":0,\"SalesFooter1ParamDecimal\":2,\"SalesFooter2ParamDecimal\":2,\"SalesFooter3ParamDecimal\":2,\"PurchaseFooter1ParamDecimal\":2,\"PurchaseFooter2ParamDecimal\":2,\"PurchaseFooter3ParamDecimal\":2,\"BankerRounding\":false,\"DisplayFixedSizeQuantityDecimal\":false,\"DisplayFixedSizeSalesPriceDecimal\":true,\"DisplayFixedSizePurchasePriceDecimal\":true,\"DisplayFixedSizeCostDecimal\":false,\"DisplayFixedSizePercentageDecimal\":false,\"DisplayFixedSizeCurrencyRateDecimal\":false,\"DisplayFixedSizeWeightDecimal\":false,\"DisplayFixedSizeVolumeDecimal\":false,\"DisplayFixedSizeMemberPointDecimal\":false,\"StockCostEncodeString\":\"0123456789,.\",\"MidpointRounding\":0,\"ZeroCurrencyAsEmptyFormatString\":\"#,0.00;-#,0.00;#\",\"ZeroCurrencyAsDashFormatString\":\"#,0.00;-#,0.00;-\",\"IntegerDecimal\":0}" },
        { "DeliveryOrder", "{\"DeliveryOrderDescription\":\"DELIVERY ORDER\",\"DeliveryOrder5CentsRoundingOption\":0,\"DeliveryOrderCommandFormStartup\":true}" },
        { "FinancialReport", "{\"UseNewFRSFormat\":false,\"UseLiveStockBalance\":false,\"UseOldCalculationForBSMonth\":false}" },
        { "FiscalYear", "{\"FiscalYearStartPeriod\":24289,\"ActualDataStartPeriod\":24289}" },
        { "DefaultCurrency", "{\"Country\": \"UK\",\"LocalCurrencyCode\": \"GBP\",\"LocalCurrencySymbol\": \"\u00a3\",\"LocalCurrencyName\": \"British Pound\"}" },
        { "GoodsReceivedNote", "{\"GoodsReceiveCommandFormStartup\":true,\"UpdateCostFromGoodsReceivedNote\":0,\"GoodsReceiveDescription\":\"GOODS RECEIVED NOTE\",\"GoodsReceiveNote5CentsRoundingOption\":0}" },
        { "GST", "{\"TaxCodeDisplayName\":\"Tax\",\"MustSpecifyTaxCode\":false,\"EnableTaxDocumentNumberManagement\":false,\"AllowDifferentGSTCurrencyRate\":false,\"InclusiveSalesGST\":false,\"UnitPriceIsTaxInclusive\":false,\"UseDefaultTaxCodeInARAP\":true,\"TaxBaseCurrencyCode\":\"\",\"GSTRate\":6.0,\"IRASGSTRegistrationDate\":\"\",\"IRASGSTTaxablePeriod\":3,\"EnableIRASGSTFiling\":false,\"IsFirstTimeIRASGSTFilingRemind\":false,\"IsSecondTimeIRASGSTFilingRemind\":false,\"GSTPayableDescription\":\"GST payable @ {0}% on {1}\",\"GSTPayableDescriptionForZeroPercentageOnly\":\"GST payable @ {0}% on {1}\",\"GSTPayableDescriptionForSalesTax\":\"Sales Tax @ {0}% on {1}\",\"GSTPayableDescriptionForServiceTax\":\"Service Tax @ {0}% on {1}\",\"GSTPayableDescriptionForNonGST\":\"Tax @ {0}% on {1}\"}" },
        { "Invoice", "{\"Invoice5CentsRoundingOption\":0,\"InvoiceDescription\":\"INVOICE\",\"InvoiceORNoFollowIVNo\":true,\"InvoiceCommandFormStartup\":true}" },
        { "Invoicing", "{\"UseRealCostForMinimumSalePrice\":false,\"PromptIfBelowReorderLevel\":false,\"DoNotTabIntoMoreHeader\":true,\"CalculateUnitPriceFromSubTotal\":true,\"CalculateAgingInInvoicingReport\":false,\"UseFontStyleInInvoicing\":false,\"DefaultSalesItemQty\":\"1\",\"AllowDuplicateYourPONo\":true,\"AllowOverTransferOfQtyFromQT\":false,\"NotifySalesOrderOption\":0,\"AlwaysFocusOnFirstControlInCreditCardPaymentWindow\":false,\"AllowNegativeSerialNumberBalanceQty\":false,\"EnforceReturnedItemSalesLocationMatchInSerialNumber\":true,\"UpdateItemPriceOption\":3,\"UpdateItemPriceConfirmationOption\":0,\"UseFirstDetailProjectDepartmentForFooterPosting\":false,\"UseFirstDetailTariffCodeForFooterPosting\":false,\"UseTransferedDocumentDate\":false,\"RoundingAdjustmentAccountNo\":\"\",\"AllowOverTransferOfQtyFromSO\":false,\"AllowOverTransferOfQtyFromPQ\":false,\"AllowOverTransferOfQtyFromRQ\":false,\"AllowOverTransferOfQtyFromPO\":false,\"AutoConvertDiscountToPercentage\":false,\"WarnZeroNetTotal\":true,\"SeparateDiscountAccNoPosting\":false,\"SmartUseUnitPriceForMinimumPriceComparison\":false,\"ApplyPackageTaxCode\":false}" },
        { "Item", "{\"RemindInvalidStockLevelWhenSaveStockItem\":true,\"ItemCodeFormat\":\"%G<00000>\",\"ItemCodeFormatStartNumber\":1,\"DefaultUOM\":\"\",\"ItemCommandFormStartup\":true,\"UseClassicItemInquiry\":false,\"EnforceItemCostPriceConstraint\":true,\"AutoCalculateMultiPricing\":false,\"DoNotWarnMinSellingPrice\":false,\"DoNotWarnMaxSellingPrice\":false,\"AutoUpdateBOMCost\":1,\"LastSavedItemGroup\":\"PHONE\",\"LastSavedItemType\":\"SAM\",\"LastSavedItemBrand\":\"\",\"LastSavedItemClass\":\"\",\"LastSavedItemCategory\":\"\",\"LastSavedPrice1Name\":\"Price 1\",\"LastSavedPrice2Name\":\"Price 2\",\"LastSavedPrice3Name\":\"Price 3\",\"LastSavedPrice4Name\":\"Price 4\",\"LastSavedPrice5Name\":\"Price 5\",\"LastSavedPrice6Name\":\"Price 6\",\"Price1Name\":\"Price 1\",\"Price2Name\":\"Price 2\",\"Price3Name\":\"Price 3\",\"Price4Name\":\"Price 4\",\"Price5Name\":\"Price 5\",\"Price6Name\":\"Price 6\"}" },
        { "License", "{\"UniqueHardwareID\":\"\",\"AccountingProductID\":\"\",\"AccountingLicenseInfo\":\"\",\"AccountingModuleMask\":\"oytNdYF8mTBtTSrPT2hvv/qy6tTdy0AlStQy3fLIfRvRNoAWHjfxHNH+WNLHWGQAUaVLyFIPGuGwI5YqXV6fcFMFlFU3/m6ssTXa/3PL4rS1LyVrcK8HGImwgmAhpYsAftk+T1LLZQwiKGi/zptDcclELlJGp+/stzwuiqSAx4QHmPM/jluHmrNkfHoqITw5l2NS+5xGaovAkMgDN28uvU5eB+xiZKuuogW8akw+rQj31JzTYdQu06lyd9hVDiZhrp/1uUazS42JUF1znWhdZ3Rv7YImaSkS15l1qIoXwZEvSGDIArqYQqcLWWPaIzMImmYvAHc0k2aJTVWnINemrulLK5SqCs/dSnNeHApKd3R8FXLBfMHrJ2B3o/gG+INGYEf4Oyk/Q37N/Qkiydq+4FMRNR9QFnb8miA2lTQpf1KIF+Ik9YB3J6yooGNDZWcrrUwyq01Knpgh80rorr22Qnsbvnwg3lrew20sYULT1v5TZxGm4ptsZAgItiuMUwCzcCvq3yyjUer8YIAhdY3Pz8bhOZTH8WkAqoknvLuWdSVyKLK5ZW6KY5C1XeKNyIF7ehsz3xcc7mWKOjyV5jcOvtKhvDoU7P8t4NSbeYC18Qt4qVooyEFmBJHEO2c3ALqKVwaFmIIu9Vg0ZE4LljmiG63iHo51pPwf+Q0eUIpe8Y4jAgcO33J5TCiy5uIDDzhIBCGtq04ZMqROUJqjx3IyFMTRB8sKCzd3cZXcHBs7c7Hw6z+4hEVkFl5P2DpyhdUi2/+Jety7zTyAR/ZmSJ6aG2+B2EiS9ban8W8Yok9iosx0j7a119iRzSILV2NmTv2W23fvbb2zOuWTYOZ5EK9aUocmrB0GmgkPrKbVPJAMKJvVeVXHDuchF8zg6dwi0P38wjFoRDTI9amgOWsWk4YDgskafCpvmfVco6V2o2bgOqonBS2ABn02PTIpEX5b3WchzRG6jt0NmzSdQzQoUZWTXf+Lfv0lYRb+Z4iWgsVjIoGyu6w1EKz5qyBUgFYbjWrUcbktFMimuOdEwu21lLjrYvMzizsziOPzEVb6PLGiw4yaCD+cVnWtVBjyqUWM0MlReGwCuinySrquVdWx0/APOzakX67EgahekjVOa4OD5tOqFuAxHX37zJZt0uvT1HYjIwB3pNfK/uGfUHyxx1fBVFsGBd3mKbU3IhraUYhQGJ/HCwZ6SPH/8EKJy9uirHoA4syzYgCs/0xKYhxwoidPDMDi1AbIL3WqptouKht10h6Jmzuy2mF2cTjpFSHR6a2dJoYPAK3Rgpu4kRNBYWCEZEIU2bjIe2KA3KOuLgqjiOm8x1YP5OlUWUKOWSYa9tuALRHJ6RF7XZjSfJuAuO/GxbVR2pbg4Ofqq9djMWpNf2WiFbBcH+jITar85tgka7ql+4dOlVV41D2GawRMuMx/5UC/uSuHloty6VdBxIwkAWesrO/Zyt3bfWxZlA2tDfjqADuz3Xrzf1BSuIdxpqr0SYjgPe4l9737kXkimkHaKrvSn8zcVTMKwkSBg/U6LQ7e\",\"UniqueLicenseID\":\"NK+CdjdoleUOq74I1RD7ekEs//85s9IfaB8E4ZDrS8asSrQNHBKjCYPOIdBDK9P3\"}" },
        { "LicenseControl", "{\"LicenseKey\":\"BD933756-DE15-4DFD-9610-20258BA08538\"}" },
        { "UnitedKingdomGST", "{\"ApplyUnitedKingdomGSTLogicOption\":0,\"GSTRegistrationDate\":\"\",\"GSTStartDate\":\"\",\"GSTTaxablePeriod\":3,\"MustProcessARBadDebtReliefAfterSixMonth\":true,\"MustProcessOutstandingDOOver21Days\":true,\"MustProcessAdvancedPayment\":true,\"Enable30DaysAdvancedPaymentWarning\":false,\"AutomaticConvertZRLtoSR0\":true,\"HasUpdatedSR0\":true,\"MustProcessAPBadDebtReliefAfterSixMonth\":true,\"ARBadDebtAccNo\":\"\",\"ARBadDebtReliefAccNo\":\"\",\"ARBadDebtRecoveryAccNo\":\"\",\"APBadDebtAccNo\":\"\",\"APBadDebtReliefAccNo\":\"\",\"APBadDebtRecoveryAccNo\":\"\",\"GSTPayableDescription\":\"GST payable @ {0}% on {1}\",\"GSTPayableDescriptionForZeroPercentageOnly\":\"GST payable @ {0}% on {1}\",\"GSTPayableDescriptionForSalesTax\":\"Sales Tax @ {0}% on {1}\",\"GSTPayableDescriptionForServiceTax\":\"Service Tax @ {0}% on {1}\",\"GSTPayableDescriptionForNonGST\":\"Tax @ {0}% on {1}\",\"DefaultSR0TaxCode\":\"SR-0\",\"DefaultTX0TaxCode\":\"TX-0\"}" },
        { "ModuleControl", "{\"ModuleLastUpdate\":487,\"ModuleID\":0,\"EnableActivityStream\":false,\"EnableTax\":true}" },
        { "PurchaseConsignment", "{\"PurchaseConsignmentCommandFormStartup\":true,\"PurchaseConsignmentDescription\":\"\"}" },
        { "PurchaseInvoice", "{\"PurhcaseInvoicePVNoFollowPINo\":true,\"PurchaseInvoiceCommandFormStartup\":true,\"PurchaseInvoiceDescription\":\"PURCHASE INVOICE\",\"PurchaseInvoice5CentsRoundingOption\":0}" },
        { "PurchaseOrder", "{\"AllowOverTransferOfQtyFromAO\":false,\"PurchaseOrderCommandFormStartup\":true,\"PurchaseOrderDescription\":\"PURCHASE ORDER\",\"PurchaseOrder5CentsRoundingOption\":0}" },
        { "PurchaseRequest", "{\"PurchaseRequestCommandFormStartup\":true,\"PurchaseRequestDescription\":\"PURCHASE REQUEST\",\"PurchaseRequest5CentsRoundingOption\":0}" },
        { "PurchaseRequestQuotation", "{\"RequestForQuotationCommandFormStartup\":true,\"RequestForQuotationDescription\":\"REQUEST QUOTATION\",\"RequestQuotation5CentsRoundingOption\":0}" },
        { "PurchaseReturn", "{\"PurchaseReturnCommandFormStartup\":true,\"PurchaseReturnDescription\":\"PURCHASE RETURN\",\"PurchaseReturn5CentsRoundingOption\":0}" },
        { "Quotation", "{\"LastSavedQTValidity\":\"\",\"LastSavedQTDeliveryTerm\":\"\",\"LastSavedQTPaymentTerm\":\"\",\"LastSavedQTCC\":\"\",\"LastSavedRecallLastQTValues\":false,\"QuotationCommandFormStartup\":true,\"QuotationDescription\":\"QUOTATION\",\"Quotation5CentsRoundingOption\":0}" },
        { "SalesOrder", "{\"SalesOrderDescription\":\"SALES ORDER\",\"SalesOrder5CentsRoundingOption\":0,\"SalesOrderCommandFormStartup\":true}" },
        { "System", "{\"AccessRightCmdFileVersion\":\"1.0.9.0\",\"PrimaryHelpSource\":0,\"ShowNews\":true,\"AccessRightLastUpdate\":0,\"SetupFilePath\":\"\",\"ExternalLinkRootPath\":\"\"}" },
        { "SystemOptionPolicy", "{\"PostToGLPolicy\":0,\"PostToStockPolicy\":0,\"ShowSummaryFooterPolicy\":0,\"ShowInstantInfoPolicy\":0,\"CanEditUnitPricePolicy\":0,\"CanEditTransferredDocumentsPolicy\":0,\"CanEditPostToGLPolicy\":0,\"CanEditPostToStockPolicy\":0,\"SetSystemOptionPolicy\":true,\"EnablePrintedDocumentAccessRight\":true,\"EnableFilterByCurrentUserLocationAccessRight\":false,\"EnableFilterByCurrentUserLocationInDataEntry\":false,\"PostToGL\":true,\"PostToStock\":true,\"ShowSummaryFooter\":true,\"ShowInstantInfo\":true,\"CanEditUnitPrice\":true,\"CanEditTransferredDocuments\":false,\"CanEditPostToGL\":true,\"CanEditPostToStock\":true}" }
    };

    private readonly List<AccType> _accTypes =
    [
        new() { AccTypeCode = "CP", Description = "CAPITAL", IsBstype = "T", IsSystemType = "T" },
        new() { AccTypeCode = "RE", Description = "RETAINED EARNING", IsBstype = "T", IsSystemType = "T" },
        new() { AccTypeCode = "FA", Description = "FIXED ASSETS", IsBstype = "T", IsSystemType = "T" },
        new() { AccTypeCode = "OA", Description = "OTHER ASSETS", IsBstype = "T", IsSystemType = "T" },
        new() { AccTypeCode = "CA", Description = "CURRENT ASSETS", IsBstype = "T", IsSystemType = "T" },
        new() { AccTypeCode = "CL", Description = "CURRENT LIABILITIES", IsBstype = "T", IsSystemType = "T" },
        new() { AccTypeCode = "LL", Description = "LONG TERM LIABILITIES", IsBstype = "T", IsSystemType = "T" },
        new() { AccTypeCode = "OL", Description = "OTHER LIABILITIES", IsBstype = "T", IsSystemType = "T" },
        new() { AccTypeCode = "SL", Description = "SALES", IsBstype = "F", IsSystemType = "T" },
        new() { AccTypeCode = "SA", Description = "SALES ADJUSTMENTS", IsBstype = "F", IsSystemType = "T" },
        new() { AccTypeCode = "CO", Description = "COST OF GOODS SOLD", IsBstype = "F", IsSystemType = "T" },
        new() { AccTypeCode = "OI", Description = "OTHER INCOMES", IsBstype = "F", IsSystemType = "T" },
        new() { AccTypeCode = "EI", Description = "EXTRA-ORDINARY INCOME", IsBstype = "F", IsSystemType = "T" },
        new() { AccTypeCode = "EP", Description = "EXPENSES", IsBstype = "F", IsSystemType = "T" },
        new() { AccTypeCode = "TX", Description = "TAXATION", IsBstype = "F", IsSystemType = "T" },
        new() { AccTypeCode = "AP", Description = "APPROPRIATION A/C", IsBstype = "F", IsSystemType = "T" }
    ];
    
    public void CreateDefaults()
    {
        using var transaction = ctx.Resource.Database.BeginTransaction();
        try
        {
            foreach (var kv in _defaults)
            {
                ctx.Resource.Settings.Add(new Setting()
                {
                    Name = kv.Key,
                    Value = kv.Value,
                });
            }
            ctx.Resource.SaveChanges();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }

        foreach (var kv in _defaults)
        {
            if (kv.Key == "DefaultCurrency")
            {
                var gs = JsonSerializer.Deserialize<SettingsDefaultCurrency>(kv.Value);
                if (gs == null)
                {
                    throw new NoCurrencyException<string>("No Currency Setting found",kv.Value);
                }

                var m = new List<Currency>()
                {
                    new ()
                    {
                        CurrencyCode = gs.LocalCurrencyCode,
                        CurrencyWord = gs.LocalCurrencyName,
                        CurrencySymbol = gs.LocalCurrencySymbol,
                        BankBuyRate = 1,
                        BankSellRate = 1,
                        Guid = Guid.NewGuid(),
                    }
                }.ToDataTable();
                var currImpl = new CurrencyImpl(ctx);
                currImpl.Save(m);
            }

            if (kv.Key == "Account")
            {
                var accTypeImpl = new AccTypeImpl(ctx);
                accTypeImpl.Save(_accTypes.ToDataTable());
            }
        }
    }

    public T? Get<T>(string key) where T: class
    {
        var json = ctx.Resource.Settings.FirstOrDefault(x => x.Name == key);
        return json?.Value == null ? null : JsonSerializer.Deserialize<T>(json.Value);
    }

    public void Save<T>(string key, T value) where T : class
    {
        var json = JsonSerializer.Serialize(value);
        var setting = ctx.Resource.Settings.FirstOrDefault(x => x.Name == key);
        if (setting == null)
        {
            ctx.Resource.Settings.Add(new Setting()
            {
                Name = key,
                Value = json,
            });
        }
        else
        {
            setting.Value = json;
        }
        ctx.Resource.SaveChanges();
    }
}