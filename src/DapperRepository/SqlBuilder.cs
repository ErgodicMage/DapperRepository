namespace ErgodicMage.DapperRepository;

public abstract class SqlBuilder
{
    #region Constructor
    protected readonly DapperRepositorySettings _settings;
    protected readonly ClassMapper _mapper;
    protected string? _sqlStatement;

    protected SqlBuilder(DapperRepositorySettings settings, ClassMapper mapper)
    {
        _settings = settings;
        _mapper = mapper;
    }
    #endregion

    #region Properties
    public TableMapper? Table { get => _mapper.Table; }
    public string? TableName { get => Builders.BuildFullTableName(Table!); }
    public List<ColumnMapper>? Columns { get => _mapper.Columns; }
    public string? SqlStatement { get => _sqlStatement; }
    #endregion

    public abstract string? Build();
}
