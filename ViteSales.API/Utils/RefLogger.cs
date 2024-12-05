using Serilog;
using ILogger = ViteSales.API.Contracts.ILogger;

namespace ViteSales.API.Utils;

public class RefLogger: ILogger
{
    public void Verbose(string messageTemplate)
    {
        Log.Verbose(messageTemplate);
    }
    
    public void Verbose<T>(string messageTemplate, T propertyValue)
    {
        Log.Verbose(messageTemplate, propertyValue);
    }
    
    public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Log.Verbose(messageTemplate, propertyValue0, propertyValue1);
    }
    
    public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Log.Verbose(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    
    public void Verbose(string messageTemplate, params object[] propertyValues)
    {
        Log.Verbose(messageTemplate, propertyValues);
    }

    public void Debug(string messageTemplate)
    {
        Log.Debug(messageTemplate);
    }
    
    public void Debug<T>(string messageTemplate, T propertyValue)
    {
        Log.Debug(messageTemplate, propertyValue);
    }
    
    public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Log.Debug(messageTemplate, propertyValue0, propertyValue1);
    }
    
    public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Log.Debug(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    
    public void Debug(string messageTemplate, params object[] propertyValues)
    {
        Log.Debug(messageTemplate, propertyValues);
    }

    public void Information(string messageTemplate)
    {
        Log.Information(messageTemplate);
    }
    
    public void Information<T>(string messageTemplate, T propertyValue)
    {
        Log.Information(messageTemplate, propertyValue);
    }
    
    public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Log.Information(messageTemplate, propertyValue0, propertyValue1);
    }
    
    public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Log.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    
    public void Information(string messageTemplate, params object[] propertyValues)
    {
        Log.Information(messageTemplate, propertyValues);
    }

    public void Warning(string messageTemplate)
    {
        Log.Warning(messageTemplate);
    }
    
    public void Warning<T>(string messageTemplate, T propertyValue)
    {
        Log.Warning(messageTemplate, propertyValue);
    }
    
    public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Log.Warning(messageTemplate, propertyValue0, propertyValue1);
    }
    
    public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Log.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    
    public void Warning(string messageTemplate, params object[] propertyValues)
    {
        Log.Warning(messageTemplate, propertyValues);
    }

    public void Error(string messageTemplate)
    {
        Log.Error(messageTemplate);
    }
    
    public void Error<T>(string messageTemplate, T propertyValue)
    {
        Log.Error(messageTemplate, propertyValue);
    }
    
    public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Log.Error(messageTemplate, propertyValue0, propertyValue1);
    }
    
    public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Log.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    
    public void Error(string messageTemplate, params object[] propertyValues)
    {
        Log.Error(messageTemplate, propertyValues);
    }

    public void Fatal(string messageTemplate)
    {
        Log.Fatal(messageTemplate);
    }
    
    public void Fatal<T>(string messageTemplate, T propertyValue)
    {
        Log.Fatal(messageTemplate, propertyValue);
    }
    
    public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Log.Fatal(messageTemplate, propertyValue0, propertyValue1);
    }
    
    public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Log.Fatal(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    
    public void Fatal(string messageTemplate, params object[] propertyValues)
    {
        Log.Fatal(messageTemplate, propertyValues);
    }
}