using System.Threading;

namespace ErgodicMage.DapperRepository;

public static class DapperExtensions
{
    public static IEnumerable<T> Get<T>(this IDbConnection connection, SqlBuilderSelect builder, object? values,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = builder.Build()!;
        var dynamicParameters = builder.BuildDynamicParameters(values);
        return connection.Query<T>(sql, dynamicParameters, transaction, true, commandTimeout);
    }

    public static Task<IEnumerable<T>> GetAsync<T>(this IDbConnection connection, SqlBuilderSelect builder, object? values,
        CancellationToken cancellationToken = default, IDbTransaction ? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = builder.Build()!;
        var dynamicParameters = builder.BuildDynamicParameters(values);
        CommandDefinition command = new(sql, dynamicParameters, transaction, commandTimeout, cancellationToken: cancellationToken);
        return connection.QueryAsync<T>(command);
    }

    public static int Count<T>(this IDbConnection connection, SqlCountBuilder builder, object? values,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = builder.Build()!;
        DynamicParameters? dynamicParameters = builder.BuildDynamicParameters(values);
        return connection.QueryFirst<int>(sql, dynamicParameters, transaction, commandTimeout);
    }

    public static Task<int> CountAsync<T>(this IDbConnection connection, SqlCountBuilder builder, object? values, 
        CancellationToken cancellationToken = default, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = builder.Build()!;
        DynamicParameters? dynamicParameters = builder.BuildDynamicParameters(values);
        CommandDefinition command = new(sql, dynamicParameters, transaction, commandTimeout, cancellationToken: cancellationToken);
        return connection.QueryFirstAsync<int>(command);
    }
}
