using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core
{
    public interface IQuery
    {
        IQuery FilterBy(FilterType fType, dynamic param);
        IQuery And();
        IQuery Or();
        IEnumerable<T> Query<T>();
        int Delete<T>();
        int Update<T>(Object o);
    }
}
