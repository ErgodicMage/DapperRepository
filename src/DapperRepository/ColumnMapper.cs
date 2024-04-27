namespace ErgodicMage.DapperRepository;

public class ColumnMapper
{
    public string? ClassName { get; set; }
    public string? ColumnName { get; set; }
    public int Length { get; set; } = 0;
    public ColumnAttributes Attributes { get; set; } = ColumnAttributes.None;
}
