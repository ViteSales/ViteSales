namespace ViteSales.ERP.Shared.Interfaces;

public interface ICacheClient
{
    public Task ConnectAsync();
    public Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry) where T : notnull;
    public Task<bool> RemoveAsync(string key);
    public Task<T?> GetAsync<T>(string key);
}