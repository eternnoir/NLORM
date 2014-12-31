using NLORM.Core.BasicDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.Utility
{
    class NormalPorpertyGetter:IPropertyGetter
    {
        public IDictionary<string, object> GetPropertyDic(object Fo)
        {
            var ret = new Dictionary<string, object>();
            var fliterInfo = Fo.GetType().GetProperties();
            var i = 1;
            foreach (var info in fliterInfo)
            {
                ret.Add(info.Name, info.GetValue(Fo, null));
            }
            return ret;
        }
    }
}
