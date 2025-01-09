namespace ViteSales.Shared.Interfaces;

public interface ICacheClient
{
    public Task Connect();
    public Task<bool> Set<T>(string key, T value, TimeSpan? expiry) where T : notnull;
    public Task<bool> Remove(string key);
    public Task<T?> Get<T>(string key);
}