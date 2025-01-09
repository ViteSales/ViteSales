using StackExchange.Redis;
using ViteSales.ERP.Shared.Interfaces;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Shared.Cache;

public class CacheClient(CacheDbCredentials auth, string prefix): ICacheClient, IDisposable
{
    private IDatabase? _db;
    private ConnectionMultiplexer? _redis;
    
    public async Task Connect()
    {
        var conf = new ConfigurationOptions {
            EndPoints = { $"{auth.Host}:{auth.Port}" },
            User = auth.User,
            Password = auth.Password,
            DefaultDatabase = auth.Database
        };
        _redis = await ConnectionMultiplexer.ConnectAsync(conf);
        _db = _redis.GetDatabase();
    }

    public async Task<bool> Set<T>(string key, T value, TimeSpan? expiry) where T : notnull
    {
        if (value.ToString() == null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        ArgumentNullException.ThrowIfNull(_db);
        var done = await _db.StringSetAsync(GetKey(key), new RedisValue(value.ToString()));
        if (done && expiry.HasValue)
        {
            await _db.KeyExpireAsync(GetKey(key), expiry.Value);
        }
        return done;
    }
    
    public async Task<bool> Remove(string key)
    {
        ArgumentNullException.ThrowIfNull(_db);
        if (!await _db.KeyExistsAsync(GetKey(key)))
        {
            return false;
        }
        return await _db.KeyDeleteAsync(GetKey(key));
    }

    public async Task<T?> Get<T>(string key)
    {
        ArgumentNullException.ThrowIfNull(_db);
        if (!await _db.KeyExistsAsync(GetKey(key)))
        {
            return default;
        }
        var value = await _db.StringGetAsync(GetKey(key));
        if (!value.HasValue || value.IsNull)
        {
            return default;
        }
        return (T)Convert.ChangeType(value, typeof(T));
    }
    
    private RedisKey GetKey(string key)
    {
        return new RedisKey($"{prefix}:{key}");
    }

    public void Dispose()
    {
        _redis?.Close();
    }
}