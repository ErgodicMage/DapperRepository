namespace ErgodicMage.DapperRepository;

[Flags]
public enum ColumnAttributes
{
    None = 0,
    Key = 1,
    NonAutoKey = 2,
    ForeignKey = 4,
    Required = 8,
    IgnoreSelect = 16,
    IgnoreInsert = 32,
    IgnoreUpdate = 64,
    IgnoreWhere = 128,
    ReadOnly = IgnoreInsert | IgnoreUpdate,
    NotMapped = IgnoreSelect | IgnoreInsert | IgnoreUpdate | IgnoreWhere,
}
