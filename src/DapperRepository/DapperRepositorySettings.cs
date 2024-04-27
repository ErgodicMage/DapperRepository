using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace ErgodicMage.DapperRepository;

public sealed class DapperRepositorySettings
{
    private readonly DapperRepositoryCache _cache;

    public DapperRepositorySettings()
    {
        _cache = new();
    }

    public DapperRepositorySettings(IConfiguration config, IMemoryCache? cache)
    {
        _cache = cache is null ? new() : new(cache);
        var section = config.GetSection("ConnectionStrings").GetChildren();
        foreach (IConfigurationSection c in section)
            AddConnectionString(c.Key, c.Value!);
    }

    public DapperRepositorySettings(IDictionary<string, string> connectionStrings, IMemoryCache? cache)
    {
        _cache = cache is null ? new() : new(cache);
        foreach (var c in connectionStrings)
            AddConnectionString(c.Key, c.Value!);
    }

    public static DapperRepositorySettings DefaultSettings { get; set; } = new DapperRepositorySettings();

    internal DapperRepositoryCache Cache { get => _cache; }

    public int DefaultCommandTimeout { get; set; } = 30;

    public string? LastSQLCommand => _cache.Get("LastSQLCommand") as string;

    public void SetLastSQLCommand(string sqlCommand) => _cache.GetorSet("LastSQLCommand", sqlCommand);

    public string? ConnectionString(string connectionName)
        => _cache.Get(ConnectionKey(connectionName)) as string;

    public void AddConnectionString(string name, string connectionString)
        => _cache.GetorSet(ConnectionKey(name), connectionString, new MemoryCacheEntryOptions() { Priority = CacheItemPriority.NeverRemove });

    private static string ConnectionKey(string key) => $"ConnectionString.{key}";

    public void TrimStrings(bool on = true)
    {
        if (on && !SqlMapper.HasTypeHandler(typeof(TrimmedStringHandler)))
            SqlMapper.AddTypeHandler(new TrimmedStringHandler());
        else if (!on && SqlMapper.HasTypeHandler(typeof(TrimmedStringHandler)))
            SqlMapper.RemoveTypeMap(typeof(TrimmedStringHandler));
    }

    public void BooleanYNConverter(bool on = true)
    {
        if (on && !SqlMapper.HasTypeHandler(typeof(BooleanYNHandler)))
            SqlMapper.AddTypeHandler(new BooleanYNHandler());
        else if (!on && SqlMapper.HasTypeHandler(typeof(BooleanYNHandler)))
            SqlMapper.RemoveTypeMap(typeof(BooleanYNHandler));
    }
}
