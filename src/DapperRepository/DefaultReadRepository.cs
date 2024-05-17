using System.Threading;

namespace ErgodicMage.DapperRepository;

public abstract class DefaultReadRepository<T, Key> where T : class
{
    #region Constructor
    protected readonly DapperRepositorySettings _settings;

    public DefaultReadRepository(DapperRepositorySettings settings)
    {
        _settings = settings;
    }
    #endregion

    #region Connection
    protected abstract IDbConnection GetConnection();

    protected string? GetConnectionString(string connectionStringName) => _settings.ConnectionString(connectionStringName);
    #endregion

    #region Default Handling
    public int? DefaultTimeout { get; set; }

    protected int? GetTimeout(int? timeout) => (timeout is not null) ? timeout : DefaultTimeout;

    public bool Buffer { get; set; } = true;
    #endregion

    #region Get
    public T? Get(Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, key, transaction, commandTimeout);
    }

    public T? Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        SqlBuilderSelect builder = new(_settings, new ClassMapper<T>());
        builder.WhereId();
        string sql = builder.Build()!;
        DynamicParameters? dynamicParameters = builder.BuildDynamicParameters(key);
        return connection.QueryFirstOrDefault<T>(sql, dynamicParameters, transaction, GetTimeout(commandTimeout));
    }

    public IEnumerable<T> Get(SqlBuilderSelect builder, object? values, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, builder, values, transaction, commandTimeout);
    }

    public IEnumerable<T> Get(IDbConnection connection, SqlBuilderSelect builder, object? values,
        IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Get<T>(builder, values, transaction, GetTimeout(commandTimeout));
    //{
    //    string sql = builder.Build()!;
    //    DynamicParameters? dynamicParameters = builder.BuildDynamicParameters(values);
    //    return connection.Query<T>(sql, dynamicParameters, transaction, Buffer, GetTimeout(commandTimeout));
    //}

    public IEnumerable<T> Get(object? whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, whereConditions, transaction, commandTimeout);
    }

    public IEnumerable<T> Get(IDbConnection connection, object? whereConditions,
        IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        SqlBuilderSelect builder = new(_settings, new ClassMapper<T>());
        builder.Where(whereConditions);
        return Get(connection, builder, whereConditions, transaction, commandTimeout);
    }

    public IEnumerable<T> GetAll(IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetAll(connection, transaction, commandTimeout);
    }

    public IEnumerable<T> GetAll(IDbConnection connection, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        SqlBuilderSelect builder = new(_settings, new ClassMapper<T>());
        return Get(connection, builder, null, transaction, commandTimeout);
    }
    #endregion

    #region Count
    public int Count(SqlCountBuilder builder, object? values, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Count(connection, builder, values, transaction, commandTimeout);
    }

    public int Count(IDbConnection connection, SqlCountBuilder builder, object? values,
        IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Count<T>(builder, values, transaction, GetTimeout(commandTimeout));
    //{
    //    string sql = builder.Build()!;
    //    DynamicParameters? dynamicParameters = builder.BuildDynamicParameters(values);
    //    return connection.QueryFirst<int>(sql, dynamicParameters, transaction, GetTimeout(commandTimeout));
    //}

    public int Count(object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Count(whereConditions, transaction, commandTimeout);
    }

    public int Count(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, 
        int? commandTimeout = null)
    {
        SqlCountBuilder builder = new(_settings, new ClassMapper<T>());
        if (whereConditions is not null)
            builder.Where(whereConditions);
        return Count(connection, builder, whereConditions, transaction, commandTimeout);
    }
    #endregion

    #region Get Async
    public Task<T?> GetAsync(Key key, CancellationToken cancellationToken = default, IDbTransaction? transaction = null, 
        int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetAsync(connection, key, cancellationToken, transaction, commandTimeout);
    }

    public Task<T?> GetAsync(IDbConnection connection, Key key, CancellationToken cancellationToken = default, 
        IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        SqlBuilderSelect builder = new(_settings, new ClassMapper<T>());
        builder.WhereId();
        string sql = builder.Build()!;
        DynamicParameters? dynamicParameters = builder.BuildDynamicParameters(key);
        return connection.QueryFirstOrDefaultAsync<T>(sql, dynamicParameters, transaction, GetTimeout(commandTimeout));
    }

    public async Task<IEnumerable<T>> Get(SqlBuilderSelect builder, object? values, CancellationToken cancellationToken = default, 
        IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetAsync(connection, builder, values, cancellationToken, transaction, commandTimeout);
    }

    public Task<IEnumerable<T>> GetAsync(IDbConnection connection, SqlBuilderSelect builder, object? values,
        CancellationToken cancellationToken = default, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetAsync<T>(builder, values, cancellationToken, transaction, GetTimeout(commandTimeout));
    //{
    //    string sql = builder.Build()!;
    //    DynamicParameters? dynamicParameters = builder.BuildDynamicParameters(values);
    //    return connection.Query<T>(sql, dynamicParameters, transaction, Buffer, GetTimeout(commandTimeout));
    //}

    public async Task<IEnumerable<T>> GetAsync(object? whereConditions, CancellationToken cancellationToken = default, 
        IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetAsync(connection, whereConditions, cancellationToken, transaction, commandTimeout);
    }

    public Task<IEnumerable<T>> GetAsync(IDbConnection connection, object? whereConditions, 
        CancellationToken cancellationToken = default, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        SqlBuilderSelect builder = new(_settings, new ClassMapper<T>());
        builder.Where(whereConditions);
        return GetAsync(connection, builder, whereConditions, cancellationToken, transaction, commandTimeout);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default, IDbTransaction? transaction = null, 
        int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetAllAsync(connection, cancellationToken, transaction, commandTimeout);
    }

    public Task<IEnumerable<T>> GetAllAsync(IDbConnection connection, CancellationToken cancellationToken = default, 
        IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        SqlBuilderSelect builder = new(_settings, new ClassMapper<T>());
        return GetAsync(connection, builder, null, cancellationToken, transaction, commandTimeout);
    }
    #endregion

    #region Count Async
    public async Task<int> CountAsync(SqlCountBuilder builder, object? values, CancellationToken cancellationToken = default, 
        IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await CountAsync(connection, builder, values, cancellationToken, transaction, commandTimeout);
    }

    public Task<int> CountAsync(IDbConnection connection, SqlCountBuilder builder, object? values,
        CancellationToken cancellationToken = default, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.CountAsync<T>(builder, values, cancellationToken, transaction, GetTimeout(commandTimeout));
    //{
    //    string sql = builder.Build()!;
    //    DynamicParameters? dynamicParameters = builder.BuildDynamicParameters(values);
    //    return connection.QueryFirst<int>(sql, dynamicParameters, transaction, GetTimeout(commandTimeout));
    //}

    public async Task<int> CountAsync(object? whereConditions = null, CancellationToken cancellationToken = default, 
        IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await CountAsync(whereConditions, cancellationToken, transaction, commandTimeout);
    }

    public Task<int> CountAsync(IDbConnection connection, object? whereConditions = null, 
        CancellationToken cancellationToken = default,  IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        SqlCountBuilder builder = new(_settings, new ClassMapper<T>());
        if (whereConditions is not null)
            builder.Where(whereConditions);
        return CountAsync(connection, builder, whereConditions, cancellationToken, transaction, commandTimeout);
    }
    #endregion
}
