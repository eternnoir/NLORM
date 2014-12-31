using NLORM.Core;

namespace NLORM.MSSQL
{
    public class MSSQLSqlBuilder : BaseSqlBuilder
    {
        public override ISqlBuilder CreateOne()
        {
            return new MSSQLSqlBuilder();
        }
    }
}
