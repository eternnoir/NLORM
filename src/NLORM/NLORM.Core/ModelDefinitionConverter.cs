using System;
using System.Collections.Generic;
using System.Reflection;
using NLORM.Core.BasicDefinitions;
using NLORM.Core.Attributes;

namespace NLORM.Core
{
    public class ModelDefinitionConverter
    {
        public ModelDefinition ConverClassToModelDefinition<T>() 
        {

            return ConverClassToModelDefinition(typeof(T));
        }

        public ModelDefinition ConverClassToModelDefinition(Type T)
        {
            string tableName = GetTableNameByType(T);
            var fiedlDic = GetColumnFieldDefinition(T);
            var ret = new ModelDefinition(tableName, fiedlDic);
            return ret;
        }

        private string GetTableNameByType(Type classType)
        {
            var tableNameAttr = GetTableNameAttrByType(classType);
            string ret = tableNameAttr == null ? classType.Name : tableNameAttr.TableName;
            return ret;
        }

        private TableNameAttribute GetTableNameAttrByType(Type classType)
        {
            return (TableNameAttribute)Attribute.GetCustomAttribute(classType, typeof(TableNameAttribute));
        }

        private Dictionary<string, ColumnFieldDefinition> GetColumnFieldDefinition(Type classType)
        {
            var propeties = classType.GetProperties();
			var ret = new Dictionary<string, ColumnFieldDefinition>();
            foreach (var pro in propeties)
            {
                var columnFieldDef = GetColumnFieldDefByProprty(pro);

				if ( GenCloumn( pro) )
					ret.Add(pro.Name, columnFieldDef);
            }
            return ret;
        }

        private ColumnFieldDefinition GetColumnFieldDefByProprty(PropertyInfo prop)
        {
            object[] attrs = prop.GetCustomAttributes(true);
            var ret = new ColumnFieldDefinition();
            ColumnNameAttribute colNameAttr = null;
            ColumnTypeAttribute colTypeAttr = null; 
            foreach (object attr in attrs)
            {
                if (attr.GetType().Equals(typeof(ColumnNameAttribute)))
                {
                    colNameAttr = attr as ColumnNameAttribute;
                }
                if (attr.GetType().Equals(typeof(ColumnTypeAttribute)))
                {
                    colTypeAttr = attr as ColumnTypeAttribute;
                }
            }
            AsignColNameAttrToDef(ret, colNameAttr,prop);
            AsignColTypeAttrToDef(ret, colTypeAttr,prop);
            return ret;
        }

        private void AsignColNameAttrToDef(ColumnFieldDefinition colunmF,
            ColumnNameAttribute colNameAttr,PropertyInfo prop)
        {
            colunmF.ColumnName = colNameAttr == null ? prop.Name : colNameAttr.ColumnName;
        }

        private void AsignColTypeAttrToDef(ColumnFieldDefinition colunmF, 
            ColumnTypeAttribute colTypeAttr,PropertyInfo prop)
        {
            if (colTypeAttr != null)
            {
                colunmF.PropName = prop.Name;
                colunmF.FieldType = colTypeAttr.DBType;
                colunmF.Length = colTypeAttr.Length;
                colunmF.Nullable = colTypeAttr.Nullable;
                colunmF.Comment = colTypeAttr.Comment;
            }
            else
            {
                colunmF.PropName = prop.Name;
                colunmF.FieldType = Dapper.SqlMapper.LookupDbType(prop.PropertyType,prop.Name);
            }
        }

		private bool GenCloumn(PropertyInfo prop)
		{
			var tag = true;

			var attrs = prop.GetCustomAttributes( true);

		    foreach ( object attr in attrs)
			{
			    var ngattr = attr as NotGenColumnAttribute;
			    if (ngattr != null)
			    {
			        tag = false;
			    }
			}

		    return tag;
		}
    }
}
