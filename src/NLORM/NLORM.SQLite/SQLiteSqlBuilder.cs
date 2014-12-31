using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using NLORM.Core;
using NLORM.Core.BasicDefinitions;

namespace NLORM.SQLite
{
    public class SQLiteSqlBuilder : BaseSqlBuilder 
    {

        public override ISqlBuilder CreateOne()
        {
            return new SQLiteSqlBuilder();
        }
    }
}
