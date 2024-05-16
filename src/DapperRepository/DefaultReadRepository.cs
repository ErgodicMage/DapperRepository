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
    public T? GetId(Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetId(connection, key, transaction, commandTimeout);
    }

    public T? GetId(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        SqlBuilderSelect builder = new(_settings, new ClassMapper<T>());
        builder.WhereId();
        string sql = builder.Build()!;
        DynamicParameters? dynamicParameters = builder.BuildDynamicParameters(key);
        return connection.QueryFirstOrDefault<T>(sql, dynamicParameters, transaction, GetTimeout(commandTimeout));
    }

    public IEnumerable<T> Get(SqlBuilderSelect builder, object? values,
        IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, builder, values, transaction, commandTimeout);
    }

    public IEnumerable<T> Get(IDbConnection connection, SqlBuilderSelect builder, object? values, 
        IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        string sql = builder.Build()!;
        DynamicParameters? dynamicParameters = builder.BuildDynamicParameters(values);
        return connection.Query<T>(sql, dynamicParameters, transaction, Buffer, GetTimeout(commandTimeout));
    }

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
}
