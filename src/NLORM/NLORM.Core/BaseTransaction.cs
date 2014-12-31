using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NLORM.Core
{
    public class BaseTransaction : ITransaction
    {
        private IDbTransaction trans;

        public IDbTransaction DbTransaction
        {
            get
            {
                return trans;
            }
        }

        public BaseTransaction(IDbConnection dbc)
        {
            if (dbc.State != ConnectionState.Open)
            {
                dbc.Open();
            }
            trans = dbc.BeginTransaction();
        }

        public void Commit()
        {
            trans.Commit();
        }

        public void Rollback()
        {
            trans.Rollback();
        }

        public void Dispose()
        {
            trans.Dispose();
        }
    }
}
