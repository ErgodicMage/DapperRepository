using Microsoft.Extensions.Caching.Memory;

namespace ErgodicMage.DapperRepository;

internal static class MemoryCacheExtensions
{
    public static T GetorSet<T>(this IMemoryCache cache, string key, Func<T> create, MemoryCacheEntryOptions? options = null)
    {
        if (cache.TryGetValue(key, out T? retObj))
            return retObj!;

        return cache.Set(key, create(), options);
    }

    public static string GetorSet(this IMemoryCache cache, string key, string obj, MemoryCacheEntryOptions? options = null)
    {
        if (cache.TryGetValue(key, out string? retObj))
            return retObj!;

        return cache.Set(key, obj, options);
    }
}
