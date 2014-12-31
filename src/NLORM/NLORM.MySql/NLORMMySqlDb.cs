using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLORM.Core;
using MySql.Data.MySqlClient;

namespace NLORM.MySql
{
    public class NLORMMySqlDb : NLORMBaseDb
    {
        public NLORMMySqlDb(string dbConnectionString)
        {
            DbConnection = new MySqlConnection(dbConnectionString);
            SqlBuilder = new MySqlSqlBuilder();
        }
    }
}
