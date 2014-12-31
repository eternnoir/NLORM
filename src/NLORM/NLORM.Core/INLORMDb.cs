using System;
using System.Collections.Generic;
using System.Data;

namespace NLORM.Core
{
    public interface INLORMDb
    {
        void Open();
        void Close();
        void Dispose();

        IDbConnection GetDbConnection();

        /// <summary>
        ///     Use this creating Table.
        /// </summary>
        /// <typeparam name="T"> model</typeparam>
        void CreateTable<T>() where T : new();

        /// <summary>
        ///     Use this dropping Table.
        /// </summary>
        /// <typeparam name="T"> model</typeparam>
        void DropTable<T>() where T : new();

        IEnumerable<T> Query<T>(string sql, dynamic param = null, ITransaction transaction = null, bool buffered = true,
            int? commandTimeout = null, CommandType? commandType = null);

        IQuery FilterBy(FilterType fType, dynamic param);
        IEnumerable<T> Query<T>();
        int Insert<T>(Object o);
        int Delete<T>();
        int Update<T>(Object o);

        ITransaction BeginTransaction(string transactonName);
    }
}