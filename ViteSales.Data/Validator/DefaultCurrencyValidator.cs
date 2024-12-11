using FluentValidation;
using ViteSales.Data.Models;

namespace ViteSales.Data.Validator;

public class DefaultCurrencyValidator: AbstractValidator<SettingsDefaultCurrency>
{
    public DefaultCurrencyValidator()
    {
        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.");

        RuleFor(x => x.LocalCurrencyCode)
            .NotEmpty().WithMessage("Local Currency Code is required.");

        RuleFor(x => x.LocalCurrencySymbol)
            .NotEmpty().WithMessage("Local Currency Symbol is required.");

        RuleFor(x => x.DataInputEncoding)
            .NotEmpty().WithMessage("Data Input Encoding is required.");

        RuleFor(x => x.LocalCurrencyName)
            .NotEmpty().WithMessage("Local Currency Name is required.");
    }
}