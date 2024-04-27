using System.ComponentModel;

namespace ErgodicMage.DapperRepository;

public abstract class SqlBuilderWhere : SqlBuilder
{
    #region Constructor
    protected string? _whereId;
    public SqlBuilderWhere(DapperRepositorySettings settings, ClassMapper mapper) : base(settings, mapper) { }
    #endregion

    #region Properties
    public IList<Where>? WhereConditions { get; set; }
    #endregion

    #region Fluent
    public SqlBuilder AddWhere(Where where)
    {
        WhereConditions ??= new List<Where>();
        WhereConditions.Add(where);
        return this;
    }

    public SqlBuilder AddWhereId()
    {
        _whereId = _mapper.GetWhereId();
        return this;
    }

    public SqlBuilder AddWhereEqual(ColumnMapper column) => AddWhere(new Where(column));
    public SqlBuilder AddWhereNotEqual(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.NotEquals));
    public SqlBuilder AddWhereNull(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.Null));
    public SqlBuilder AddWhereNotNull(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.NotNull));
    #endregion

    #region Build Where Conditions
    protected void BuildWhereIdStatement(StringBuilder sb)
        => sb.Append(_whereId);

    protected void BuildWhereStatement(StringBuilder sb)
    {
        if (WhereConditions is null || WhereConditions.Count == 0) return;

        sb.Append("WHERE ");

        if (!string.IsNullOrEmpty(_whereId))
        {
            sb.Append(_whereId);
            return;
        }

        bool first = true;
        foreach (var condition in WhereConditions)
        {
            if (!first) sb.Append(WhereHandlers.GetAndOrNot(condition));

            sb.Append(condition.ColumnName);
            sb.Append(WhereHandlers.GetWhereOperator(condition));
            sb.Append(condition.ValueName);

            first = false;
        }
    }


    #endregion
}
