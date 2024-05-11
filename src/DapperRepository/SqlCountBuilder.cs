using System.ComponentModel;

namespace ErgodicMage.DapperRepository;

public sealed class SqlCountBuilder : SqlBuilderWhere
{
    #region Constructor
    public SqlCountBuilder(DapperRepositorySettings settings, ClassMapper mapper) : base(settings, mapper) { }
    #endregion

    public static SqlCountBuilder CreateCountBuilder(DapperRepositorySettings settings, ClassMapper mapper)
        => new(settings, mapper);

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
}
