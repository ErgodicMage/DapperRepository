using Microsoft.Extensions.Caching.Memory;

namespace ErgodicMage.DapperRepository;

internal sealed class DapperRepositoryCache
{
    #region Constructors
    private readonly IMemoryCache _memoryCache;

    public DapperRepositoryCache()
    {
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
    }

    public DapperRepositoryCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    #endregion

    #region CachedProperties
    #endregion

    #region Cache Operations
    private static string KeyName<T>(string key) => $"{typeof(T).Name}.{key}";

    public TValue GetorSet<Type, TValue>(string key, Func<TValue> create, MemoryCacheEntryOptions? options = null)
        => _memoryCache.GetorSet<TValue>(KeyName<Type>(key), create, options);

    public TValue GetorSet<TValue>(string key, Func<TValue> create, MemoryCacheEntryOptions? options = null)
        => _memoryCache.GetorSet<TValue>(key, create, options);

    public string GetorSet(string key, string value, MemoryCacheEntryOptions? options = null)
        => _memoryCache.GetorSet(key, value, options);

    public object? Get(string key) => _memoryCache.Get(key);

    #endregion

}
