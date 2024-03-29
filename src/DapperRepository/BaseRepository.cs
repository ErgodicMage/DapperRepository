﻿namespace DapperRepository;

public abstract class BaseRepository<T, Key> : BaseReadRepository<T, Key>, IRepository<T, Key> where T : class
{
    #region Constructors
    protected BaseRepository() : base()
    { }

    protected BaseRepository(DapperRepositorySettings settings) : base(settings)
    { }
    #endregion

    #region Insert
    public Key Insert(T entity, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Insert(connection, entity, null, commandTimeout);
    }

    public Key Insert(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Insert<T, Key>(entity, transaction, commandTimeout);

    public async Task<Key> InsertAsync(T entity, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await InsertAsync(connection, entity, null, commandTimeout);
    }

    public Task<Key> InsertAsync(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.InsertAsync<T, Key>(entity, transaction, commandTimeout);
    #endregion

    #region Update
    public int Update(T entity, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Update(connection, entity, null, commandTimeout);
    }

    public int Update(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Update<T>(entity, transaction, commandTimeout);

    public int Update(Key key, object set, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Update(connection, key, set, null, commandTimeout);
    }

    public int Update(IDbConnection connection, Key key, object set, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Update<T, Key>(key, set, transaction, commandTimeout);

    public int UpdateWhere(object where, object set, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return UpdateWhere(connection, where, set, null, commandTimeout);
    }

    public int UpdateWhere(IDbConnection connection, object where, object set, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.UpdateWhere<T>(where, set, transaction, commandTimeout);

    public async Task<int> UpdateAsync(T entity, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await UpdateAsync(connection, entity, null, commandTimeout);
    }

    public Task<int> UpdateAsync(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.UpdateAsync<T>(entity, transaction, commandTimeout);

    public async Task<int> UpdateAsync(Key key, object set, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await UpdateAsync(connection, key, set, null, commandTimeout);
    }

    public Task<int> UpdateAsync(IDbConnection connection, Key key, object set, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.UpdateAsync<T, Key>(key, set, transaction, commandTimeout);

    public async Task<int> UpdateWhereAsync(object where, object set, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await UpdateWhereAsync(connection, where, set, null, commandTimeout);
    }

    public Task<int> UpdateWhereAsync(IDbConnection connection, object where, object set, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.UpdateWhereAsync<T>(where, set, transaction, commandTimeout);
    #endregion

    #region Delete
    public int Delete(Key id, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Delete(connection, id, null, commandTimeout);
    }

    public int Delete(IDbConnection connection, Key id, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        string sql = DeleteBuilder<T>.BuildDeleteIdStatement();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        return connection.Execute(sql, dynParameters, transaction, commandTimeout);
    }

    public int Delete(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Delete(connection, whereConditions, null, commandTimeout);
    }

    public int Delete(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.DeleteWhere<T>(whereConditions, transaction, commandTimeout);

    public async Task<int> DeleteAsync(Key id, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await DeleteAsync(connection, id, null, commandTimeout);
    }

    public Task<int> DeleteAsync(IDbConnection connection, Key id, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        string sql = DeleteBuilder<T>.BuildDeleteIdStatement();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        return connection.ExecuteAsync(sql, dynParameters, transaction, commandTimeout);
    }

    public async Task<int> DeleteAsync(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await DeleteAsync(connection, whereConditions, null, commandTimeout);
    }

    public Task<int> DeleteAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.DeleteWhereAsync<T>(whereConditions, transaction, commandTimeout);
    #endregion
}
