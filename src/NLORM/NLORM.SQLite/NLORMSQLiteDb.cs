using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using NLORM.Core;

namespace NLORM.SQLite
{
    public class NLORMSQLiteDb : NLORMBaseDb
    {
        public NLORMSQLiteDb(string connectionString)
        {
            DbConnection = new SQLiteConnection(connectionString);
            SqlBuilder = new SQLiteSqlBuilder();
        }
    }
}
