using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Dapper;
using NLORM.Core.BasicDefinitions;
using System.Reflection;
using NLORM.Core.Exceptions;
using System.Diagnostics;
using System.Dynamic;

namespace NLORM.Core
{
    public class NLORMBaseDb : INLORMDb,IQuery
    {
        protected ISqlBuilder SqlBuilder;
        protected IDbConnection DbConnection;
        protected Type QueryType;
        protected List<IFilterObject> FliterObjects;
        protected ITransaction trans=null;

        public IDbConnection GetDbConnection()
        {
            Debug.Assert(DbConnection != null);
            return DbConnection;
        }

        virtual public void Open()
        {
            DbConnection.Open();
        }

        virtual public void Close()
        {
            DbConnection.Close();
        }

        virtual public void Dispose()
        {
            Close();
            DbConnection.Dispose();
        }

        virtual public void CreateTable<T>() where T : new()
        {
            var sql = SqlBuilder.GenCreateTableSql<T>();
            DbConnection.Execute(sql);
        }

        virtual public void DropTable<T>() where T : new()
        {
            var sql = SqlBuilder.GenDropTableSql<T>();
            DbConnection.Execute(sql);
        }

        virtual public IEnumerable<T> Query<T>(string sql, dynamic param = null, ITransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var ret = SqlMapper.Query<T>(DbConnection, sql, param, tryToCastIDbConnection(transaction), buffered, commandTimeout, commandType);
            return (IEnumerable<T>)ret;
        }

        private int Execute(string sql, dynamic param = null, ITransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {

            var ret = SqlMapper.Execute(DbConnection, sql, param, tryToCastIDbConnection(transaction), commandTimeout, commandType);
            return ret;
        }

        private IDbTransaction tryToCastIDbConnection(ITransaction transaction)
        {
            return transaction == null ? null : ((BaseTransaction)transaction).DbTransaction;
        }


        virtual public IEnumerable<T> Query<T>()
        {
            GenSelectSql(typeof(T));
            GenWhereSql();
            var selectStr = SqlBuilder.SQLString;
            var whereStr = SqlBuilder.GetWhereSQLString();
            var sql = selectStr;
            if (!string.IsNullOrEmpty(whereStr))
            {
                sql += " WHERE "+whereStr;
            }
            var consObject = SqlBuilder.GetWhereParas();
            ResetFliterCache();
            return (IEnumerable<T>)Query<T>(sql, consObject,trans);
        }


        virtual public int Delete<T>()
        {
            GenDeleteSql(typeof(T));
            GenWhereSql();
            var deleteStr = SqlBuilder.SQLString;
            var whereStr = SqlBuilder.GetWhereSQLString();
            var sql = deleteStr;
            if (!string.IsNullOrEmpty(whereStr))
            {
                sql += " WHERE "+whereStr;
            }
            var consObject = SqlBuilder.GetWhereParas();
            ResetFliterCache();
            return Execute(sql, consObject, trans);
        }

        virtual public int Insert<T>(Object o)
        {
            var sql = SqlBuilder.GenInsertSql<T>();
            return Execute(sql, o,trans);
        }


        virtual public int Update<T>(Object o)
        {
            GenUpdateSql(typeof(T),o);
            GenWhereSql();
            var updateStr = SqlBuilder.SQLString;
            var whereStr = SqlBuilder.GetWhereSQLString();
            var sql = updateStr;
            if (!string.IsNullOrEmpty(whereStr))
            {
                sql += " WHERE "+whereStr;
            }
            var consObject = SqlBuilder.GetWhereParas();
            setProValueToDynPara(consObject, o);
            ResetFliterCache();
            return SqlMapper.Execute(DbConnection, sql, consObject);
        }

        private void setProValueToDynPara(DynamicParameters dp, Object o)
        {
            Type type = o.GetType();
            if (type.Equals(typeof(ExpandoObject)))
            {
                var pg = new Utility.ExpandoPorpertyGetter();
                var paraDic = pg.GetPropertyDic(o);
                foreach (var key in paraDic.Keys)
                {
                    dp.Add("@" + key, paraDic[key]);
                }
            }
            else
            {
                IList<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());
                foreach (PropertyInfo prop in props)
                {
                    object propValue = prop.GetValue(o, null);
                    dp.Add("@" + prop.Name, propValue);
                }
            }
        }

        public IQuery FilterBy(FilterType fType, dynamic param)
        {
            if (param == null)
            {
                throw new ParaErrorException("FilterBy Para can not be null");
            }
            FliterObjects = FliterObjects?? new List<IFilterObject>();
            var f = new FilterObject();
            f.ClassType = QueryType;
            f.Cons = param;
            f.Filter = fType;
            FliterObjects.Add(f);
            return this;
        }

        public IQuery And()
        {
            if (FliterObjects == null)
            {
                throw new FilterChainException("No filter brefore AND.");
            }
            var f = new OpFilterObject();
            f.type = OpFilterType.AND;
            FliterObjects.Add(f);
            return this;
        }

        public IQuery Or()
        {
            if (FliterObjects == null)
            {
                throw new FilterChainException("No filter brefore OR.");
            }
            var f = new OpFilterObject();
            f.type = OpFilterType.OR;
            FliterObjects.Add(f);
            return this;
        }

        virtual public ITransaction BeginTransaction(string transactonName = "")
        {
            trans = new BaseTransaction(DbConnection);
            return trans;
        }

        private void GenWhereSql()
        {
            if (FliterObjects == null)
            {
                return;
            }
            foreach (var f in FliterObjects)
            {
                SqlBuilder.GenWhereCons(f);
            }
        }

        private void GenSelectSql(Type t)
        {
            QueryType = t;
            SqlBuilder.GenSelect(t);
        }

        private void GenUpdateSql(Type t,Object obj)
        {
            QueryType = t;
            SqlBuilder.GenUpdate(t,obj);
        }

        private void ResetSqlBuilder()
        {
            SqlBuilder = SqlBuilder.CreateOne();
        }

        private void ResetFliterCache()
        {
            ResetSqlBuilder();
            FliterObjects = null;
        }

        private void GenDeleteSql(Type t)
        {
            QueryType = t;
            SqlBuilder.GenDelete(t);
        }

    }
}
