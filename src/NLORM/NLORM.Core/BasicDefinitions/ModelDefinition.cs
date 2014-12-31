using System.Collections.Generic;

namespace NLORM.Core.BasicDefinitions
{
    public class ModelDefinition
    {
        public string TableName { get { return _tableName; } }
        public Dictionary<string, ColumnFieldDefinition> PropertyColumnDic { get; private set; }
        private readonly string _tableName;
        public ModelDefinition(string tablename, Dictionary<string, ColumnFieldDefinition> propertycolumnDic)
        {
            _tableName = tablename;
            PropertyColumnDic = propertycolumnDic;
        }
        
    }
}
