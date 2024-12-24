using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using FluentValidation;
using SqlKata.Execution;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Database;

namespace ViteSales.ERP.SDK.Validator;

public class FormValidator<T> : AbstractValidator<T> where T : class
{
    public FormValidator()
    {
        var properties = typeof(T).GetProperties();
        foreach (var prop in properties)
        {
            var bindAttr = prop.GetCustomAttribute<BindDataTypeAttribute>();
            var errorMessageAttr = prop.GetCustomAttribute<ErrorMessageAttribute>();
            var errorMessage = $"Validation Error: {prop.Name}";
            if (errorMessageAttr != null)
            {
                errorMessage = $"{prop.Name}: {errorMessageAttr.Message}";
            }
            if (bindAttr != null)
            {
                switch (bindAttr.Type)
                {
                    case FieldTypes.AutoNumber:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Empty()
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Boolean:
                    case FieldTypes.Checkbox:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is bool)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.ReadableId:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => x?.Length <= 40)
                            .NotEmpty()
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Currency:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is decimal)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.SmallText:
                    case FieldTypes.ShortCode:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .NotEmpty()
                            .Must(x => x?.Length <= 80)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Date:
                    case FieldTypes.DateTime:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is DateTime)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Email:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .EmailAddress()
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.File:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .NotEmpty()
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Guid:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is Guid)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Html:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => !string.IsNullOrWhiteSpace(x))
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Image:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .NotEmpty()
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.MultiLine:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .NotEmpty()
                            .MaximumLength(1000)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Geography:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is string)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.MultiSelect:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is IEnumerable<object>)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Numeric:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is int or double or float)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Password:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .NotEmpty()
                            .MinimumLength(8)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Phone:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Matches(@"^\+?[1-9]\d{1,14}$") 
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Select:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .NotNull()
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Text:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => x?.Length <= 200)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Char:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => x?.Length <= 2)
                            .WithMessage(errorMessage);
                        break;
                    case FieldTypes.Url:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute) || string.IsNullOrEmpty(x))
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Time:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is TimeSpan)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Vector:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .NotEmpty()
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Json:
                    case FieldTypes.Jsonb:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => IsValidJson(x))
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Xml:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as XDocument)
                            .Must(x => x is not null)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Binary:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is byte[])
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Enum:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => x?.Length <= 255)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Interval:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is TimeSpan)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Int4Range:
                    case FieldTypes.Int8Range:
                    case FieldTypes.NumRange:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is IEnumerable<int> ints && ints.Count() == 2)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.TsRange:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is IEnumerable<DateTime> times && times.Count() == 2)
                            .WithMessage(errorMessage);
                        break;

                    case FieldTypes.Hstore:
                        RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as IDictionary<string, string>)
                            .NotEmpty()
                            .WithMessage(errorMessage);
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }

    private bool IsValidJson(object? json)
    {
        try
        {
            switch (json)
            {
                case null:
                    return false;
                case string str:
                    JsonSerializer.Deserialize<object>(str);
                    return true;
                default:
                    return json is JsonArray or JsonObject or JsonNode or JsonElement or JsonDocument or JsonValue or IEnumerable<object>;
            }
        }
        catch
        {
            return false;
        }
    }

    private bool IsValidXml(string? xml)
    {
        try
        {
            if (string.IsNullOrEmpty(xml)) return false;
            var xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xml);
            return true;
        }
        catch
        {
            return false;
        }
    }
}