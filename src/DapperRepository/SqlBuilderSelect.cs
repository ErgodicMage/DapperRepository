using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgodicMage.DapperRepository;
public class SqlBuilderSelect : SqlBuilderWhere
{
    #region Constructor
    protected DynamicParameters _dynamicParameters;
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public SqlBuilderSelect(DapperRepositorySettings settings, ClassMapper mapper) : base(settings, mapper) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    #endregion

    #region Build
    public override string? Build()
    {
        return string.Empty;
    }

    public override DynamicParameters? BuildDynamicParameters(object? values)
    {
        if (WhereConditions is null && Columns is null)
            return null;

        DynamicParameters returnParameters = new(base.BuildDynamicParameters(values));
        returnParameters.AddDynamicParams(BuildColumnsDynamicParameters());

        return returnParameters;
    }

    private DynamicParameters? BuildColumnsDynamicParameters()
    {
        if (_dynamicParameters is not null)
            return _dynamicParameters;

        var selectColumns = GetSelectColumns();
        if (!selectColumns.Any())
            return null;

        _dynamicParameters = new DynamicParameters();
        foreach (var column in selectColumns)
        {
            string name = string.IsNullOrEmpty(column.ClassName) ? column.ColumnName! : column.ClassName;
            _dynamicParameters.Add(name, direction: ParameterDirection.Output, size: column.Length != 0 ? column.Length : null);
        }

        return _dynamicParameters;
    }

    private IEnumerable<ColumnMapper> GetSelectColumns()
        => Columns!.Where(c => (c.Attributes & ColumnAttributes.IgnoreSelect) != 0);
    #endregion

}
