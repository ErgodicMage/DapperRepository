using System.ComponentModel;

namespace ErgodicMage.DapperRepository;

public sealed class SqlCountBuilder : SqlBuilderWhere
{
    #region Constructor
    public SqlCountBuilder(DapperRepositorySettings settings, ClassMapper mapper) : base(settings, mapper) { }
    #endregion

    public static SqlCountBuilder CreateCountBuilder(DapperRepositorySettings settings, ClassMapper mapper)
        => new SqlCountBuilder(settings, mapper);

    #region Build
    public override string? Build()
    {
        if (!string.IsNullOrEmpty(_sqlStatement)) return _sqlStatement;

        StringBuilder sb = new();
        sb.Append("SELECT COUNT(1) FROM ");
        sb.Append(TableName);
        sb.Append(' ');
        BuildWhereStatement(sb);

        _sqlStatement = sb.ToString();
        return _sqlStatement;
    }
    #endregion

    #region Fluent
    public SqlCountBuilder WhereId() => (AddWhereId() as SqlCountBuilder)!;
    public SqlCountBuilder Where(Where where) => (AddWhere(where) as SqlCountBuilder)!;

    public SqlCountBuilder WhereEqual(ColumnMapper column) => (AddWhereEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereEqual(ColumnMapper column) => (AddAndWhereEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereEqual(ColumnMapper column) => (AddOrWhereEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereEqual(string columnName) => (AddWhereEqual(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereEqual(string columnName) => (AddAndWhereEqual(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereEqual(string columnName) => (AddOrWhereEqual(columnName) as SqlCountBuilder)!;

    public SqlCountBuilder WhereNotEqual(ColumnMapper column) => (AddWhereNotEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereNotEqual(ColumnMapper column) => (AddAndWhereNotEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereNotEqual(ColumnMapper column) => (AddOrWhereNotEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereNotEqual(string columnName) => (AddWhereNotEqual(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereNotEqual(string columnName) => (AddAndWhereNotEqual(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereNotEqual(string columnName) => (AddOrWhereNotEqual(columnName) as SqlCountBuilder)!;

    public SqlCountBuilder WhereNull(ColumnMapper column) => (AddWhereNull(column) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereNull(ColumnMapper column) => (AddAndWhereNull(column) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereNull(ColumnMapper column) => (AddOrWhereNull(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereNull(string columnName) => (AddWhereNull(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereNull(string columnName) => (AddAndWhereNull(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereNull(string columnName) => (AddOrWhereNull(columnName) as SqlCountBuilder)!;

    public SqlCountBuilder WhereNotNull(ColumnMapper column) => (AddWhereNull(column) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereNotNull(ColumnMapper column) => (AddAndWhereNull(column) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereNotNull(ColumnMapper column) => (AddOrWhereNull(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereNotNull(string columnName) => (AddWhereNull(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereNotNull(string columnName) => (AddAndWhereNull(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereNotNull(string columnName) => (AddOrWhereNull(columnName) as SqlCountBuilder)!;

    public SqlCountBuilder WhereGreaterThan(ColumnMapper column) => (AddWhereGreaterThan(column) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereGreaterThan(ColumnMapper column) => (AddAndWhereGreaterThan(column) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereGreaterThan(ColumnMapper column) => (AddOrWhereGreaterThan(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereGreaterThan(string columnName) => (AddWhereGreaterThan(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereGreaterThan(string columnName) => (AddAndWhereGreaterThan(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereGreaterThan(string columnName) => (AddOrWhereGreaterThan(columnName) as SqlCountBuilder)!;

    public SqlCountBuilder WhereGreaterThanOrEqual(ColumnMapper column) => (AddWhereGreaterThanOrEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereGreaterThanOrEqual(ColumnMapper column) => (AddAndWhereGreaterThanOrEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereGreaterThanOrEqual(ColumnMapper column) => (AddOrWhereGreaterThanOrEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereGreaterThanOrEqual(string columnName) => (AddWhereGreaterThanOrEqual(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereGreaterThanOrEqual(string columnName) => (AddAndWhereGreaterThanOrEqual(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereGreaterThanOrEqual(string columnName) => (AddOrWhereGreaterThanOrEqual(columnName) as SqlCountBuilder)!;

    public SqlCountBuilder WhereLessThan(ColumnMapper column) => (AddWhereLessThan(column) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereLessThan(ColumnMapper column) => (AddAndWhereLessThan(column) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereLessThan(ColumnMapper column) => (AddOrWhereLessThan(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereLessThan(string columnName) => (AddWhereLessThan(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereLessThan(string columnName) => (AddAndWhereLessThan(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereLessThan(string columnName) => (AddOrWhereLessThan(columnName) as SqlCountBuilder)!;

    public SqlCountBuilder WhereLessThanOrEqual(ColumnMapper column) => (AddWhereLessThanOrEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereLessThanOrEqual(ColumnMapper column) => (AddAndWhereLessThanOrEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereLessThanOrEqual(ColumnMapper column) => (AddOrWhereLessThanOrEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereLessThanOrEqual(string columnName) => (AddWhereLessThanOrEqual(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder AndWhereLessThanOrEqual(string columnName) => (AddAndWhereLessThanOrEqual(columnName) as SqlCountBuilder)!;
    public SqlCountBuilder OrWhereLessThanOrEqual(string columnName) => (AddOrWhereLessThanOrEqual(columnName) as SqlCountBuilder)!;
    #endregion
}
