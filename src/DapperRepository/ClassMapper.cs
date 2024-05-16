namespace ErgodicMage.DapperRepository;

public class ClassMapper
{
    #region Constructors
    private readonly DapperRepositorySettings _settings;

    public ClassMapper() => _settings = DapperRepositorySettings.DefaultSettings;

    public ClassMapper(DapperRepositorySettings settings) =>_settings = settings;
    #endregion

    #region Properties
    public TableMapper? Table { get; protected set; }

    public List<ColumnMapper>? Columns { get; protected set; }
    #endregion

    #region Add Mappings
    public ClassMapper AddTable(TableMapper table)
    {
        Table = table;
        return this;
    }

    public ClassMapper AddTable(string name, string? alias = null, string? schema = null)
    {
        Table = new() { TableName = name, Alias = alias, Schema = schema };
        return this;
    }

    public ClassMapper AddColumn(ColumnMapper column)
    {
        Columns ??= new List<ColumnMapper>();
        if (!Columns.Contains(column)) 
            Columns.Add(column);
        return this;
    }

    public ClassMapper AddColumn(string className, string columnName, ColumnAttributes attributes, int length = 0)
        => AddColumn(new() { ClassName = className, ColumnName = columnName, Attributes = attributes, Length = length });

    public ClassMapper AddColumn(string className, ColumnAttributes attributes, int length = 0)
    => AddColumn(new() { ClassName = className, Attributes = attributes, Length = length });
    #endregion

    #region Get
    protected string? _fullTableName;
    public virtual string? GetTableName()
    {
        if (!string.IsNullOrEmpty(_fullTableName)) return _fullTableName;
        if (Table is null) return null;
        _fullTableName = Builders.BuildFullTableName(Table);
        return _fullTableName;
    }

    protected string? _whereId;
    public virtual string? GetWhereId()
    {
        if (!string.IsNullOrEmpty(_whereId)) return _whereId;
        if (Table is null || Columns is null || Columns.Count == 0) return null;
        _whereId = Builders.BuildWhereId(Table, Columns);
        return _whereId;
    }

    protected string? _selectColumns;
    public virtual string? GetSelectColumns()
    {
        if (!string.IsNullOrEmpty(_selectColumns)) return _selectColumns;
        if (Table is null || Columns is null || Columns.Count == 0) return null;
        _selectColumns = Builders.BuildSelectColumns(Table, Columns);
        return _selectColumns;
    }

    protected string? _insertStatement;
    public virtual string? GetInsertStatement()
    {
        if (!string.IsNullOrEmpty(_insertStatement)) return _insertStatement;
        if (Table is null || Columns is null || Columns.Count == 0) return null;
        _insertStatement = Builders.BuildInsertStatement(Table, Columns);
        return _insertStatement;
    }
    #endregion
}
