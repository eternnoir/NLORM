using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using NLORM.Core.BasicDefinitions;
using System.Dynamic;
using System.Collections.Generic;

namespace NLORM.Core
{
    public class BaseSqlGenerator : ISqlGenerator
    {
        private const string StringDeafultLength = "255";
        private Dictionary<DbType, Func<ColumnFieldDefinition,string>> createSqlFuncDic = null;

        public string GenCreateTableSql(ModelDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append("CREATE TABLE " + md.TableName + " (");
            var i = 1;
            foreach (var cfd in md.PropertyColumnDic.Values)
            {
                ret.Append(GenColumnCreateTableSql(cfd));
                if (i != md.PropertyColumnDic.Values.Count)
                {
                    ret.Append(" ,");
                }
                i++;
            }
            ret.Append(")");
            return ret.ToString();
        }

        virtual public string GenCreateString(ColumnFieldDefinition cfd)
        {
            var length = string.IsNullOrEmpty(cfd.Length) ? StringDeafultLength : cfd.Length;
            return GenCreateSqlByType(cfd, "varchar", length);
        }

        virtual public string GenCreateInteger(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "INTEGER");
        }

        virtual public string GenCreateDateTime(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "DATETIME");
        }

        virtual public string GenCreateDecimal(ColumnFieldDefinition cfd)
        {
            var length = string.IsNullOrEmpty(cfd.Length) ? StringDeafultLength : cfd.Length;
            return GenCreateSqlByType(cfd, "decimal", length);
        }

        virtual public string GenCreateBit(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "bit", null);
        }

        virtual public string GenCreateFloat(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "float", null);
        }

        virtual public string GenCreateReal(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "real", null);
        }

        virtual public string GenCreateTime(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "time", null);
        }

        virtual public string GenCreateTinyint(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "tinyint", null);
        }

        virtual public string GenCreateBigint(ColumnFieldDefinition cfd)
        {
            return GenCreateSqlByType(cfd, "bigint", null);
        }

        private string GenCreateSqlByType(ColumnFieldDefinition cfd, string type, string length = "")
        {
            var ret = "";
            ret += " " + cfd.ColumnName + " ";
            var nullable = cfd.Nullable ? "" : "not null";
            if (string.IsNullOrEmpty(length))
            {
                ret += type + " " + nullable;
            }
            else
            {
                ret += type + "(" + length + ") " + nullable;
            }
            return ret;
        }

        virtual public string GenDropTableSql(ModelDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append(" DROP TABLE ");
            ret.Append(md.TableName);
            return ret.ToString();
        }

        virtual public string GenInsertSql(ModelDefinition md)
        {
            var ret = new StringBuilder();

            var fields = new StringBuilder();
            var valueFields = new StringBuilder();
            fields.Append(GenInsertColFields(md));
            valueFields.Append(GenInsertValueFields(md));
            ret.Append(" INSERT INTO ");
            ret.Append(md.TableName + "( ");
            ret.Append(fields);
            ret.Append(" ) VALUES ( ");
            ret.Append(valueFields);
            ret.Append(") ");
            return ret.ToString();
        }

        virtual public string GenSelectSql(ModelDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append(" SELECT ");
            int i = 1;
            foreach (var cdf in md.PropertyColumnDic.Values)
            {
                ret.Append(cdf.ColumnName + " " + cdf.PropName);
                if (i < md.PropertyColumnDic.Count)
                {
                    ret.Append(" , ");
                    i++;
                }
            }
            ret.Append(" FROM " + md.TableName + " ");
            return ret.ToString();
        }

        virtual public string GenDeleteSql(ModelDefinition md)
        {
            var ret = new StringBuilder();
            ret.Append(" DELETE ");
            ret.Append(" FROM " + md.TableName + " ");
            return ret.ToString();
        }

        public string GenUpdateSql(ModelDefinition md, Object obj)
        {
            var ret = new StringBuilder();

            var fields = new StringBuilder();
            var valueFields = new StringBuilder();
            fields.Append(GenInsertColFields(md));
            valueFields.Append(GenInsertValueFields(md));
            ret.Append(" UPDATE ");
            ret.Append(md.TableName + " SET ");
            var type = obj.GetType();
            Utility.IPropertyGetter pg;
            if (type.Equals(typeof(ExpandoObject)))
            {
                var expando = obj as ExpandoObject;
                ret.Append(GenExpandoUpdateParaString(expando));
            }
            else
            {
                ret.Append(GenNormalUpdateParaString(obj));
            }
            return ret.ToString();
        }

        private string GenExpandoUpdateParaString(ExpandoObject obj)
        {
            var ret = new StringBuilder();
            var i = 1;
            var pg = new Utility.ExpandoPorpertyGetter();
            var paraDic = pg.GetPropertyDic(obj);
            foreach (var key in paraDic.Keys)
            {
                ret.Append(" " + key + "=@" + key + " ");
                if (i < paraDic.Keys.Count)
                {
                    ret.Append(" , ");
                    i++;
                }
            }
            return ret.ToString();
        }

        private string GenNormalUpdateParaString(object obj)
        {
            var ret = new StringBuilder();
            var objMdf = new ModelDefinitionConverter().ConverClassToModelDefinition(obj.GetType());
            int i = 1;
            foreach (var df in objMdf.PropertyColumnDic.Values)
            {
                ret.Append(" " + df.ColumnName + "=@" + df.PropName + " ");
                if (i < objMdf.PropertyColumnDic.Values.Count)
                {
                    ret.Append(" , ");
                    i++;
                }
            }
            return ret.ToString();
        }

        private string GenInsertColFields(ModelDefinition md)
        {
            var ret = "";
            var i = 1;
            foreach (var mdf in md.PropertyColumnDic.Values)
            {
                ret += " " + mdf.ColumnName;
                if (i < md.PropertyColumnDic.Values.Count)
                {
                    ret += " ,";
                }
                i++;
            }
            return ret;
        }

        private string GenInsertValueFields(ModelDefinition md)
        {
            var ret = "";
            var i = 1;
            foreach (var mdf in md.PropertyColumnDic.Values)
            {
                ret += " @" + mdf.PropName;
                if (i < md.PropertyColumnDic.Values.Count)
                {
                    ret += " ,";
                }
                i++;
            }
            return ret;
        }

        private string GenColumnCreateTableSql(ColumnFieldDefinition cfd)
        {
            GenCreateSqlFuncDic();
            if (!createSqlFuncDic.ContainsKey(cfd.FieldType))
            {
                throw new NLORM.Core.Exceptions.NLORMException("SG","NOT SUPPORT DBTYPE");
            }
            return createSqlFuncDic[cfd.FieldType](cfd);
        }

        private void GenCreateSqlFuncDic()
        {
            if (createSqlFuncDic != null)
            {
                return;
            }
            createSqlFuncDic = new Dictionary<DbType, Func<ColumnFieldDefinition, string>>();
            createSqlFuncDic.Add(DbType.Byte, GenCreateTinyint);
            createSqlFuncDic.Add(DbType.Int16, GenCreateInteger);  //smallInt
            createSqlFuncDic.Add(DbType.Int32, GenCreateInteger);
            createSqlFuncDic.Add(DbType.Int64, GenCreateBigint);
            createSqlFuncDic.Add(DbType.Single, GenCreateReal);
            createSqlFuncDic.Add(DbType.Double, GenCreateFloat);
            createSqlFuncDic.Add(DbType.Decimal, GenCreateDecimal);
            createSqlFuncDic.Add(DbType.Boolean, GenCreateBit);
            createSqlFuncDic.Add(DbType.String, GenCreateString);
            createSqlFuncDic.Add(DbType.DateTime, GenCreateDateTime);
            createSqlFuncDic.Add(DbType.Time, GenCreateTime);
        }

    }
}
