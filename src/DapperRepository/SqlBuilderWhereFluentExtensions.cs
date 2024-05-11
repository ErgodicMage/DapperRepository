using System.Runtime.CompilerServices;

namespace ErgodicMage.DapperRepository;

public static class SqlBuilderWhereFluentExtensions
{
    public static SqlBuilderWhere Where(this SqlBuilderWhere builder, Where where)
    {
        builder.AddWhere(where);
        return builder;
    }

    public static SqlBuilderWhere WhereId(this SqlBuilderWhere builder)
    {
        builder.AddWhereId();
        return builder;
    }

    public static SqlBuilderWhere WhereEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(column, WhereOperator.Equals));
    public static SqlBuilderWhere AndWhereEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.And, column, WhereOperator.Equals));
    public static SqlBuilderWhere OrWhereEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.Or, column, WhereOperator.Equals));
    public static SqlBuilderWhere WhereEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(columnName, WhereOperator.Equals));
    public static SqlBuilderWhere AndWhereEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.And, columnName, WhereOperator.Equals));
    public static SqlBuilderWhere OrWhereEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.Or, columnName, WhereOperator.Equals));

    public static SqlBuilderWhere AddWhereNotEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(column, WhereOperator.NotEquals));
    public static SqlBuilderWhere AddAndWhereNotEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.And, column, WhereOperator.NotEquals));
    public static SqlBuilderWhere AddOrWhereNotEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.Or, column, WhereOperator.NotEquals));
    public static SqlBuilderWhere AddWhereNotEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(columnName, WhereOperator.NotEquals));
    public static SqlBuilderWhere AddAndWhereNotEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.And, columnName, WhereOperator.NotEquals));
    public static SqlBuilderWhere AddOrWhereNotEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.Or, columnName, WhereOperator.NotEquals));

    public static SqlBuilderWhere AddWhereNull(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(column, WhereOperator.Null));
    public static SqlBuilderWhere AddAndWhereNull(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.And, column, WhereOperator.Null));
    public static SqlBuilderWhere AddOrWhereNull(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.Or, column, WhereOperator.Null));
    public static SqlBuilderWhere AddWhereNull(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(columnName, WhereOperator.Null));
    public static SqlBuilderWhere AddAndWhereNull(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.And, columnName, WhereOperator.Null));
    public static SqlBuilderWhere AddOrWhereNull(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.Or, columnName, WhereOperator.Null));

    public static SqlBuilderWhere AddWhereNotNull(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(column, WhereOperator.NotNull));
    public static SqlBuilderWhere AddAndWhereNotNull(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.And, column, WhereOperator.NotNull));
    public static SqlBuilderWhere AddOrWhereNotNull(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.Or, column, WhereOperator.NotNull));
    public static SqlBuilderWhere AddWhereNotNull(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(columnName, WhereOperator.NotNull));
    public static SqlBuilderWhere AddAndWhereNotNull(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.And, columnName, WhereOperator.NotNull));
    public static SqlBuilderWhere AddOrWhereNotNull(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.Or, columnName, WhereOperator.NotNull));

    public static SqlBuilderWhere AddWhereGreaterThan(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(column, WhereOperator.GreaterThan));
    public static SqlBuilderWhere AddAndWhereGreaterThan(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.And, column, WhereOperator.GreaterThan));
    public static SqlBuilderWhere AddOrWhereGreaterThan(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.Or, column, WhereOperator.GreaterThan));
    public static SqlBuilderWhere AddWhereGreaterThan(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(columnName, WhereOperator.GreaterThan));
    public static SqlBuilderWhere AddAndWhereGreaterThan(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.And, columnName, WhereOperator.GreaterThan));
    public static SqlBuilderWhere AddOrWhereGreaterThan(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.Or, columnName, WhereOperator.GreaterThan));

    public static SqlBuilderWhere AddWhereGreaterThanOrEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(column, WhereOperator.GreaterThanOrEqual));
    public static SqlBuilderWhere AddAndWhereGreaterThanOrEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.And, column, WhereOperator.GreaterThanOrEqual));
    public static SqlBuilderWhere AddOrWhereGreaterThanOrEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.Or, column, WhereOperator.GreaterThanOrEqual));
    public static SqlBuilderWhere AddWhereGreaterThanOrEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(columnName, WhereOperator.GreaterThanOrEqual));
    public static SqlBuilderWhere AddAndWhereGreaterThanOrEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.And, columnName, WhereOperator.GreaterThanOrEqual));
    public static SqlBuilderWhere AddOrWhereGreaterThanOrEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.Or, columnName, WhereOperator.GreaterThanOrEqual));

    public static SqlBuilderWhere AddWhereLessThan(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(column, WhereOperator.LessThan));
    public static SqlBuilderWhere AddAndWhereLessThan(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.And, column, WhereOperator.LessThan));
    public static SqlBuilderWhere AddOrWhereLessThan(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.Or, column, WhereOperator.LessThan));
    public static SqlBuilderWhere AddWhereLessThan(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(columnName, WhereOperator.LessThan));
    public static SqlBuilderWhere AddAndWhereLessThan(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.And, columnName, WhereOperator.LessThan));
    public static SqlBuilderWhere AddOrWhereLessThan(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.Or, columnName, WhereOperator.LessThan));

    public static SqlBuilderWhere AddWhereLessThanOrEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(column, WhereOperator.LessThanOrEqual));
    public static SqlBuilderWhere AddAndWhereLessThanOrEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.And, column, WhereOperator.LessThanOrEqual));
    public static SqlBuilderWhere AddOrWhereLessThanOrEqual(this SqlBuilderWhere builder, ColumnMapper column) 
        => builder.Where(new Where(WhereAndOrNot.Or, column, WhereOperator.LessThanOrEqual));
    public static SqlBuilderWhere AddWhereLessThanOrEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(columnName, WhereOperator.LessThanOrEqual));
    public static SqlBuilderWhere AddAndWhereLessThanOrEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.And, columnName, WhereOperator.LessThanOrEqual));
    public static SqlBuilderWhere AddOrWhereLessThanOrEqual(this SqlBuilderWhere builder, string columnName) 
        => builder.Where(new Where(WhereAndOrNot.Or, columnName, WhereOperator.LessThanOrEqual));

}
