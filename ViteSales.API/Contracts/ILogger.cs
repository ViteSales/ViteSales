namespace ViteSales.API.Contracts;

public interface ILogger
{
    void Verbose(string messageTemplate);
    void Verbose<T>(string messageTemplate, T propertyValue);
    void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    void Verbose(string messageTemplate, params object[] propertyValues);

    void Debug(string messageTemplate);
    void Debug<T>(string messageTemplate, T propertyValue);
    void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    void Debug(string messageTemplate, params object[] propertyValues);

    void Information(string messageTemplate);
    void Information<T>(string messageTemplate, T propertyValue);
    void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    void Information(string messageTemplate, params object[] propertyValues);

    void Warning(string messageTemplate);
    void Warning<T>(string messageTemplate, T propertyValue);
    void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    void Warning(string messageTemplate, params object[] propertyValues);

    void Error(string messageTemplate);
    void Error<T>(string messageTemplate, T propertyValue);
    void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    void Error(string messageTemplate, params object[] propertyValues);

    void Fatal(string messageTemplate);
    void Fatal<T>(string messageTemplate, T propertyValue);
    void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    void Fatal(string messageTemplate, params object[] propertyValues);
}