using FluentValidation;
using ViteSales.Data.Entities;

namespace ViteSales.Data.Validator;

public class GlMastValidator: AbstractValidator<Glmast>
{
    public GlMastValidator()
    {
        RuleFor(gl => gl.AccNo)
            .NotEmpty().WithMessage("Account Number is required.")
            .MaximumLength(12).WithMessage("Account Number must not exceed 12 characters.");

        RuleFor(gl => gl.AccType)
            .NotEmpty().WithMessage("Account Type is required.")
            .MaximumLength(2).WithMessage("Account Type must not exceed 2 characters.");

        RuleFor(gl => gl.CurrencyCode)
            .NotEmpty().WithMessage("Currency Code is required.")
            .MaximumLength(5).WithMessage("Currency Code must not exceed 5 characters.");

        RuleFor(gl => gl.Description)
            .MaximumLength(100).WithMessage("Description must not exceed 100 characters.");

        RuleFor(gl => gl.Desc2)
            .MaximumLength(100).WithMessage("Secondary Description must not exceed 100 characters.");

        RuleFor(gl => gl.ParentAccNo)
            .MaximumLength(12).WithMessage("Parent Account Number must not exceed 12 characters.");

        RuleFor(gl => gl.SpecialAccType)
            .MaximumLength(3).WithMessage("Special Account Type must not exceed 3 characters.");

        RuleFor(gl => gl.CashFlowCategory)
            .MaximumLength(1).WithMessage("Cash Flow Category must be a single character.")
            .Matches("^[A-Z]?$").WithMessage("Cash Flow Category must be a valid character.");

        RuleFor(gl => gl.Siccode)
            .MaximumLength(5).WithMessage("SIC Code must not exceed 5 characters.");

        RuleFor(gl => gl.InputTaxCode)
            .MaximumLength(14).WithMessage("Input Tax Code must not exceed 14 characters.");

        RuleFor(gl => gl.OutputTaxCode)
            .MaximumLength(14).WithMessage("Output Tax Code must not exceed 14 characters.");

        RuleFor(gl => gl.TariffCode)
            .MaximumLength(12).WithMessage("Tariff Code must not exceed 12 characters.");

        RuleFor(gl => gl.SgeFilingDataId)
            .MaximumLength(128).WithMessage("SGe Filing Data ID must not exceed 128 characters.");
    }
}