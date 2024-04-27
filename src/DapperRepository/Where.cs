using System.Reflection.Emit;

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
    //public string? Value { get; set; }

    private void Initiaize(ColumnMapper column)
        => ColumnName = string.IsNullOrEmpty(column.ColumnName) ? column.ClassName : column.ColumnName;
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
