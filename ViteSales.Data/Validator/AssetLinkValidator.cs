using FluentValidation;
using ViteSales.Data.Entities;

namespace ViteSales.Data.Validator;

public class AssetLinkValidator: AbstractValidator<AssetLink>
{
    public AssetLinkValidator()
    {
        RuleFor(assetLink => assetLink.AssetAccNo)
            .NotEmpty().WithMessage("AssetAccNo is required.")
            .MaximumLength(12).WithMessage("AssetAccNo must not exceed 12 characters.");

        RuleFor(assetLink => assetLink.AssetDeprnAccNo)
            .NotEmpty().WithMessage("AssetDeprnAccNo is required.")
            .MaximumLength(12).WithMessage("AssetDeprnAccNo must not exceed 12 characters.");
    }
}