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
    public SqlBuilderWhere AddWhere(Where where)
    {
        WhereConditions ??= new List<Where>();
        WhereConditions.Add(where);
        return this;
    }

    public SqlBuilderWhere AddWhereId()
    {
        _whereId = _mapper.GetWhereId();
        return this;
    }

    public SqlBuilderWhere AddWhereEqual(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.Equals));
    public SqlBuilderWhere AddAndWhereEqual(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.And, column, WhereOperator.Equals));
    public SqlBuilderWhere AddOrWhereEqual(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.Or, column, WhereOperator.Equals));
    public SqlBuilderWhere AddWhereEqual(string columnName) => AddWhere(new Where(columnName, WhereOperator.Equals));
    public SqlBuilderWhere AddAndWhereEqual(string columnName) => AddWhere(new Where(WhereAndOrNot.And, columnName, WhereOperator.Equals));
    public SqlBuilderWhere AddOrWhereEqual(string columnName) => AddWhere(new Where(WhereAndOrNot.Or, columnName, WhereOperator.Equals));

    public SqlBuilderWhere AddWhereNotEqual(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.NotEquals));
    public SqlBuilderWhere AddAndWhereNotEqual(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.And, column, WhereOperator.NotEquals));
    public SqlBuilderWhere AddOrWhereNotEqual(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.Or, column, WhereOperator.NotEquals));
    public SqlBuilderWhere AddWhereNotEqual(string columnName) => AddWhere(new Where(columnName, WhereOperator.NotEquals));
    public SqlBuilderWhere AddAndWhereNotEqual(string columnName) => AddWhere(new Where(WhereAndOrNot.And, columnName, WhereOperator.NotEquals));
    public SqlBuilderWhere AddOrWhereNotEqual(string columnName) => AddWhere(new Where(WhereAndOrNot.Or, columnName, WhereOperator.NotEquals));


    public SqlBuilderWhere AddWhereNull(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.Null));
    public SqlBuilderWhere AddAndWhereNull(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.And, column, WhereOperator.Null));
    public SqlBuilderWhere AddOrWhereNull(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.Or, column, WhereOperator.Null));
    public SqlBuilderWhere AddWhereNull(string columnName) => AddWhere(new Where(columnName, WhereOperator.Null));
    public SqlBuilderWhere AddAndWhereNull(string columnName) => AddWhere(new Where(WhereAndOrNot.And, columnName, WhereOperator.Null));
    public SqlBuilderWhere AddOrWhereNull(string columnName) => AddWhere(new Where(WhereAndOrNot.Or, columnName, WhereOperator.Null));

    public SqlBuilderWhere AddWhereNotNull(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.NotNull));
    public SqlBuilderWhere AddAndWhereNotNull(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.And, column, WhereOperator.NotNull));
    public SqlBuilderWhere AddOrWhereNotNull(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.Or, column, WhereOperator.NotNull));
    public SqlBuilderWhere AddWhereNotNull(string columnName) => AddWhere(new Where(columnName, WhereOperator.NotNull));
    public SqlBuilderWhere AddAndWhereNotNull(string columnName) => AddWhere(new Where(WhereAndOrNot.And, columnName, WhereOperator.NotNull));
    public SqlBuilderWhere AddOrWhereNotNull(string columnName) => AddWhere(new Where(WhereAndOrNot.Or, columnName, WhereOperator.NotNull));

    public SqlBuilderWhere AddWhereGreaterThan(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.GreaterThan));
    public SqlBuilderWhere AddAndWhereGreaterThan(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.And, column, WhereOperator.GreaterThan));
    public SqlBuilderWhere AddOrWhereGreaterThan(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.Or, column, WhereOperator.GreaterThan));
    public SqlBuilderWhere AddWhereGreaterThan(string columnName) => AddWhere(new Where(columnName, WhereOperator.GreaterThan));
    public SqlBuilderWhere AddAndWhereGreaterThan(string columnName) => AddWhere(new Where(WhereAndOrNot.And, columnName, WhereOperator.GreaterThan));
    public SqlBuilderWhere AddOrWhereGreaterThan(string columnName) => AddWhere(new Where(WhereAndOrNot.Or, columnName, WhereOperator.GreaterThan));

    public SqlBuilderWhere AddWhereGreaterThanOrEqual(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.GreaterThanOrEqual));
    public SqlBuilderWhere AddAndWhereGreaterThanOrEqual(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.And, column, WhereOperator.GreaterThanOrEqual));
    public SqlBuilderWhere AddOrWhereGreaterThanOrEqual(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.Or, column, WhereOperator.GreaterThanOrEqual));
    public SqlBuilderWhere AddWhereGreaterThanOrEqual(string columnName) => AddWhere(new Where(columnName, WhereOperator.GreaterThanOrEqual));
    public SqlBuilderWhere AddAndWhereGreaterThanOrEqual(string columnName) => AddWhere(new Where(WhereAndOrNot.And, columnName, WhereOperator.GreaterThanOrEqual));
    public SqlBuilderWhere AddOrWhereGreaterThanOrEqual(string columnName) => AddWhere(new Where(WhereAndOrNot.Or, columnName, WhereOperator.GreaterThanOrEqual));

    public SqlBuilderWhere AddWhereLessThan(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.LessThan));
    public SqlBuilderWhere AddAndWhereLessThan(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.And, column, WhereOperator.LessThan));
    public SqlBuilderWhere AddOrWhereLessThan(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.Or, column, WhereOperator.LessThan));
    public SqlBuilderWhere AddWhereLessThan(string columnName) => AddWhere(new Where(columnName, WhereOperator.LessThan));
    public SqlBuilderWhere AddAndWhereLessThan(string columnName) => AddWhere(new Where(WhereAndOrNot.And, columnName, WhereOperator.LessThan));
    public SqlBuilderWhere AddOrWhereLessThan(string columnName) => AddWhere(new Where(WhereAndOrNot.Or, columnName, WhereOperator.LessThan));

    public SqlBuilderWhere AddWhereLessThanOrEqual(ColumnMapper column) => AddWhere(new Where(column, WhereOperator.LessThanOrEqual));
    public SqlBuilderWhere AddAndWhereLessThanOrEqual(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.And, column, WhereOperator.LessThanOrEqual));
    public SqlBuilderWhere AddOrWhereLessThanOrEqual(ColumnMapper column) => AddWhere(new Where(WhereAndOrNot.Or, column, WhereOperator.LessThanOrEqual));
    public SqlBuilderWhere AddWhereLessThanOrEqual(string columnName) => AddWhere(new Where(columnName, WhereOperator.LessThanOrEqual));
    public SqlBuilderWhere AddAndWhereLessThanOrEqual(string columnName) => AddWhere(new Where(WhereAndOrNot.And, columnName, WhereOperator.LessThanOrEqual));
    public SqlBuilderWhere AddOrWhereLessThanOrEqual(string columnName) => AddWhere(new Where(WhereAndOrNot.Or, columnName, WhereOperator.LessThanOrEqual));

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

        if (!string.IsNullOrEmpty(_whereId))
        {
            sb.Append(_whereId);
            return;
        }

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
