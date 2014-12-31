using System;

namespace NLORM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : BaseAttribute
    {
        public ColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }
        public string ColumnName { get; set; }
    }
 }
