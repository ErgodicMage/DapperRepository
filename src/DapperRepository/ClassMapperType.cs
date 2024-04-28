namespace ErgodicMage.DapperRepository;

public class ClassMapper<T> : ClassMapper where T : class
{
    #region Constructor
    public ClassMapper() : base()
    {
        SetTable();
        SetColumns();
    }

    public ClassMapper(DapperRepositorySettings settings) : base(settings)
    {
        SetTable();
        SetColumns();
    }
    #endregion

    #region Get
    public override string? GetTableName()
    {
        if (!string.IsNullOrEmpty(_fullTableName)) return _fullTableName;
        SetTable();
        return base.GetTableName();
    }

    public override string? GetWhereId()
    {
        if (!string.IsNullOrEmpty (_whereId)) return _whereId;
        SetTable();
        SetColumns();
        return base.GetWhereId();
    }

    public override string? GetSelectColumns()
    {
        if (!string.IsNullOrEmpty(_selectColumns)) return _selectColumns;
        SetTable();
        SetColumns();
        return base.GetSelectColumns();
    }

    public override string? GetInsertStatement()
    {
        if (!string.IsNullOrEmpty(_insertStatement)) return _insertStatement;
        SetTable();
        SetColumns();
        return base.GetInsertStatement();
    }
    #endregion

    #region Set
    public void SetTable()
    {
        if (Table is not null) return;
        Type type = typeof(T);

        var tableattr = CustomAttributesHelper.TableAttribute(type);
        if (tableattr is not null)
            Table = new() { TableName = tableattr.Name, Schema = tableattr.Schema, Alias = type.Name };
        else
            Table = new() { TableName = type.Name };
    }

    public void SetColumns()
    {
        if (Columns is not null && Columns.Count > 0) return;

        PropertyInfo[] properties = typeof(T).GetProperties();
        if (properties is null || properties.Length == 0) return;

        Columns ??= new List<ColumnMapper>();

        foreach (PropertyInfo property in properties)
        {
            ColumnMapper column = new() {ClassName = property.Name};

            SetColumnAttribute(column, property);

            column.Attributes = GetColumnAttributes(property);

            Columns.Add(column);
        }
    }

    private static void SetColumnAttribute(ColumnMapper column, PropertyInfo property)
    {
        dynamic? columnAttribute = CustomAttributesHelper.ColumnAttribute(property);
        if (columnAttribute is null) return;
        column.ColumnName = columnAttribute.Name;
        column.Length = columnAttribute.Length;
    }

    private static ColumnAttributes GetColumnAttributes(PropertyInfo property)
    {
        dynamic[]? CustomAttributes = CustomAttributesHelper.AllCustomAttributes(property);
        if (CustomAttributes is null || CustomAttributes.Length == 0) return ColumnAttributes.None;

        ColumnAttributes returnAttributes = ColumnAttributes.None;

        foreach (dynamic attribute in CustomAttributes)
        {
            returnAttributes |= attribute switch
            {
                KeyAttribute => ColumnAttributes.Key,
                NonAutoKeyAttribute => ColumnAttributes.NonAutoKey,
                ForeignKeyAttribute => ColumnAttributes.ForeignKey,
                RequiredAttribute => ColumnAttributes.Required,
                IgnoreSelectAttribute => ColumnAttributes.IgnoreSelect,
                IgnoreInsertAttribute => ColumnAttributes.IgnoreInsert,
                IgnoreUpdateAttribute => ColumnAttributes.IgnoreUpdate,
                IgnoreWhereAttribute => ColumnAttributes.IgnoreWhere,
                ReadOnlyAttribute => ColumnAttributes.ReadOnly,
                NotMappedAttribute => ColumnAttributes.NotMapped,
                _ => ColumnAttributes.None
            };
        }

        return returnAttributes;
    }
    #endregion
}
