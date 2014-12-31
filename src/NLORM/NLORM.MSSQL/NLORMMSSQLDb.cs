using NLORM.Core;
using System.Data.SqlClient;

namespace NLORM.MSSQL
{
	public class NLORMMSSQLDb : NLORMBaseDb
	{
		public NLORMMSSQLDb( string connectionString)
		{
            this.DbConnection = new SqlConnection(connectionString);
			this.SqlBuilder = new MSSQLSqlBuilder();
		}
	}
}
