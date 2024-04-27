using System.ComponentModel;

namespace ErgodicMage.DapperRepository;

public sealed class SqlCountBuilder : SqlBuilderWhere
{
    #region Constructor
    public SqlCountBuilder(DapperRepositorySettings settings, ClassMapper mapper) : base(settings, mapper) { }
    #endregion

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
    public SqlCountBuilder WhereEqual(ColumnMapper column) => (AddWhereEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereNotEqual(ColumnMapper column) => (AddWhereNotEqual(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereNull(ColumnMapper column) => (AddWhereNull(column) as SqlCountBuilder)!;
    public SqlCountBuilder WhereNotNull(ColumnMapper column) => (AddWhereNull(column) as SqlCountBuilder)!;
    #endregion
}
