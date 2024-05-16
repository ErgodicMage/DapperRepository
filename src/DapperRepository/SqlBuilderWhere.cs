using System.ComponentModel;

namespace ErgodicMage.DapperRepository;

public abstract class SqlBuilderWhere : SqlBuilder
{
    #region Constructor
    protected string? _whereId;
    public SqlBuilderWhere(DapperRepositorySettings settings, ClassMapper mapper) : base(settings, mapper) { }
    #endregion

    #region Properties
    public List<Where>? WhereConditions { get; set; }
    #endregion

    #region Add Where
    public void AddWhere(Where where)
    {
        WhereConditions ??= new List<Where>();
        WhereConditions.Add(where);
    }

    public void AddWhere(ICollection<Where> conditions)
    {
        if (conditions == null) return;
        WhereConditions ??= new List<Where>();
        WhereConditions.AddRange(conditions);
    }

    public void AddWhereId() => _whereId ??= _mapper.GetWhereId();
    #endregion

    #region Build Where Conditions
    protected void BuildWhereIdStatement(StringBuilder sb)
        => sb.Append(_whereId);

    protected void BuildWhereStatement(StringBuilder sb)
    {
        sb.Append("WHERE ");
        if (!string.IsNullOrEmpty(_whereId))
        {
            sb.Append(_whereId);
            return;
        }

        if (WhereConditions is null || WhereConditions.Count == 0) return;

        bool first = true;
        foreach (var condition in WhereConditions)
        {
            if (!first)
            {
                if (condition.AndOrNot == WhereAndOrNot.None) condition.AndOrNot = WhereAndOrNot.And;
                sb.Append(WhereHandlers.GetAndOrNot(condition));
            }

            sb.Append(condition.ColumnName);
            sb.Append(WhereHandlers.GetWhereOperator(condition));
            sb.Append(condition.ValueName);

            first = false;
        }
    }
    #endregion
}
