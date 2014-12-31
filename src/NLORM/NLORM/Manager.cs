using NLORM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM
{
    public static class Manager
    {
        public static INLORMDb GetDb(string connectionString, SupportedDb dbType)
        {
             return NLORMFactory.Instance.GetDb(connectionString, dbType);
        }
    }
}
