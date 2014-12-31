using NLORM.Core;
using NLORM.Core.Exceptions;
using NLORM.MSSQL;
using NLORM.MySql;
using NLORM.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM
{
    public class NLORMFactory
    {
        private static NLORMFactory instance;

        private NLORMFactory() { }

        public static NLORMFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NLORMFactory();
                }
                return instance;
            }
        }

        public INLORMDb GetDb(string ConnectString, SupportedDb dbType)
        {
            switch (dbType)
            {
                case SupportedDb.MSSQL:
                    return new NLORMMSSQLDb(ConnectString);
                case SupportedDb.SQLITE:
                    return new NLORMSQLiteDb(ConnectString);
                case SupportedDb.MYSQL:
                    return new NLORMMySqlDb(ConnectString);
                default:
                    throw new NLORMException("N","Not SupportDB");
            }
        }
    }
}
