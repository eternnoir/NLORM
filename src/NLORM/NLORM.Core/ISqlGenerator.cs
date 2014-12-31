using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NLORM.Core.BasicDefinitions;

namespace NLORM.Core
{
    public interface ISqlGenerator
    {
        string GenCreateTableSql(ModelDefinition md);
        string GenDropTableSql(ModelDefinition md);
        string GenInsertSql(ModelDefinition md);
        string GenSelectSql(ModelDefinition md);
        string GenDeleteSql(ModelDefinition md);
        string GenUpdateSql(ModelDefinition md,Object obj);
    }
}
