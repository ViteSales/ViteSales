using System.Reflection;

namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class BindDataAttribute(string methodName) : Attribute
{
    public string MethodName { get; } = methodName;

    public object? InvokeMethod(object target)
    {
        // Retrieve the method using reflection
        var method = target.GetType().GetMethod(
            MethodName,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance
        );

        if (method == null)
        {
            throw new ArgumentException($"Method '{MethodName}' not found in type '{target.GetType()}'.");
        }

        var isStatic = method.IsStatic;
        return method.Invoke(isStatic ? null : target, null);
    }
}