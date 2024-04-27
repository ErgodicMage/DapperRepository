﻿using System.Reflection.Emit;

namespace ErgodicMage.DapperRepository;

public sealed class Where
{
    public Where() { }

    public Where(ColumnMapper column, WhereOperator op = WhereOperator.Equals)
    {
        Initiaize(column);
        Operator = op;
    }

    public Where(WhereAndOrNot andornot, ColumnMapper column, WhereOperator op = WhereOperator.Equals)
    {
        AndOrNot = andornot;
        Initiaize(column);
        Operator = op;
    }


    public WhereAndOrNot AndOrNot { get; set; } = WhereAndOrNot.None;
    public string? ColumnName { get; set; }
    public WhereOperator Operator { get; set; }
    public string? ValueName { get; set; }

    private void Initiaize(ColumnMapper column)
    {
        ColumnName = string.IsNullOrEmpty(column.ColumnName) ? column.ClassName : column.ColumnName;
        ValueName = $"@{column.ColumnName}";
    }
}

public enum WhereAndOrNot
{
    None,
    And,
    Or,
    Not
}

public enum WhereOperator
{
    Null,
    NotNull,
    Equals,
    NotEquals,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Between,
    Like,
    In
}

internal static class WhereHandlers
{
    public static string? GetAndOrNot(Where where)
    {
        return where.AndOrNot switch
        {
            WhereAndOrNot.And => " AND ",
            WhereAndOrNot.Or => " OR ",
            WhereAndOrNot.Not => " NOT ",
            _ => " "
        };
    }

    public static string? GetWhereOperator(Where where)
    {
        return where.Operator switch
        {
            WhereOperator.Null => " IS NULL ",
            WhereOperator.NotNull => " IS NOT NULL ",
            WhereOperator.Equals => "=",
            WhereOperator.NotEquals => "<>",
            WhereOperator.GreaterThan => ">",
            WhereOperator.GreaterThanOrEqual => "<=",
            WhereOperator.LessThan => "<",
            WhereOperator.LessThanOrEqual => "<=",
            WhereOperator.Between => " BETWEEN ",
            WhereOperator.In => " IN ",
            WhereOperator.Like => " LIKE ",
            _ => " "
        };
    }
}