using System;

namespace NLORM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : BaseAttribute
    {
        public TableNameAttribute(string tableName)
        {
            TableName = tableName;
        }
        public string TableName { get; set; }
    }
}
