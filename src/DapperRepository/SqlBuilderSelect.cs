using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErgodicMage.DapperRepository;
public class SqlBuilderSelect : SqlBuilderWhere
{
    #region Constructor
    public SqlBuilderSelect(DapperRepositorySettings settings, ClassMapper mapper) : base(settings, mapper) { }
    #endregion

    #region Build
    public override string? Build()
    {
        return string.Empty;
    }
    #endregion

}
