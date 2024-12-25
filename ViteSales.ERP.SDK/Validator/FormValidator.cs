using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using FluentValidation;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Validator;

public class FormValidator<T> : AbstractValidator<T> where T : class
{
    public FormValidator()
    {
        var properties = typeof(T).GetProperties();
        foreach (var prop in properties)
        {
            IRuleBuilderOptions<T, object?>? rule = null;
            var bindAttr = prop.GetCustomAttribute<BindDataTypeAttribute>();
            
            var instance = Activator.CreateInstance(prop.DeclaringType ?? throw new InvalidOperationException("Property does not have a declaring type"));
            var propertyValue = prop.GetValue(instance);
            
            var errorMessageAttr = prop.GetCustomAttribute<ErrorMessageAttribute>();
            var errorMessage = $"Validation Error: Property <{prop.Name}> ";
            if (propertyValue != null)
            {
                errorMessage = $"{errorMessage} Value <{propertyValue}> ";
            }
            if (errorMessageAttr != null)
            {
                errorMessage = $"{errorMessage} {errorMessageAttr.Message}";
            }
            var requiredAttr = prop.GetCustomAttribute<RequiredAttribute>();
            
            if (bindAttr != null)
            {
                switch (bindAttr.Type)
                {
                    case FieldTypes.AutoNumber:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Empty();
                        break;

                    case FieldTypes.Boolean:
                    case FieldTypes.Checkbox:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is bool);
                        break;

                    case FieldTypes.ReadableId:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => x?.Length <= 40)
                            .NotEmpty();
                        break;

                    case FieldTypes.Currency:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is decimal);
                        break;

                    case FieldTypes.SmallText:
                    case FieldTypes.ShortCode:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => x?.Length <= 80);
                        break;

                    case FieldTypes.Date:
                    case FieldTypes.DateTime:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is DateTime);
                        break;

                    case FieldTypes.Email:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .EmailAddress();
                        break;

                    case FieldTypes.File:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .NotEmpty();
                        break;

                    case FieldTypes.Guid:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is Guid);
                        break;

                    case FieldTypes.Html:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => !string.IsNullOrWhiteSpace(x));
                        break;

                    case FieldTypes.Image:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .NotEmpty();
                        break;

                    case FieldTypes.MultiLine:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .MaximumLength(1000);
                        break;

                    case FieldTypes.Geography:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is string);
                        break;

                    case FieldTypes.MultiSelect:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is IEnumerable<object>);
                        break;

                    case FieldTypes.Numeric:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is int or double or float);
                        break;

                    case FieldTypes.Password:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .NotEmpty()
                            .MinimumLength(8);
                        break;

                    case FieldTypes.Phone:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Matches(@"^\+?[1-9]\d{1,14}$") ;
                        break;

                    case FieldTypes.Select:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .NotNull();
                        break;

                    case FieldTypes.Text:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => x?.Length <= 200);
                        break;

                    case FieldTypes.Char:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => x?.Length <= 2);
                        break;
                    case FieldTypes.Url:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute) || string.IsNullOrEmpty(x));
                        break;

                    case FieldTypes.Time:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is TimeSpan);
                        break;

                    case FieldTypes.Vector:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .NotEmpty();
                        break;

                    case FieldTypes.Json:
                    case FieldTypes.Jsonb:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(IsValidJson);
                        break;

                    case FieldTypes.Xml:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as XDocument)
                            .Must(x => x is not null);
                        break;

                    case FieldTypes.Binary:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is byte[]);
                        break;

                    case FieldTypes.Enum:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as string)
                            .Must(x => x?.Length <= 255);
                        break;

                    case FieldTypes.Interval:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is TimeSpan);
                        break;

                    case FieldTypes.Int4Range:
                    case FieldTypes.Int8Range:
                    case FieldTypes.NumRange:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is IEnumerable<int> ints && ints.Count() == 2);
                        break;

                    case FieldTypes.TsRange:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x))
                            .Must(x => x is IEnumerable<DateTime> times && times.Count() == 2);
                        break;

                    case FieldTypes.Hstore:
                        rule = RuleFor(x => x.GetType().GetProperty(prop.Name)!.GetValue(x) as IDictionary<string, string>)
                            .NotEmpty();
                        break;

                    default:
                        throw new NotImplementedException();
                }
                if (requiredAttr != null && rule != null)
                {
                    rule = rule.NotEmpty();
                    errorMessage = $"[Required] {errorMessage}";
                }
                rule?.WithMessage(errorMessage);
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