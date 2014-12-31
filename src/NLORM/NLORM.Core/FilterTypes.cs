using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core
{
    public enum FilterType
    {
        EQUAL_AND,
        EQUAL_OR,
        LESS_AND,
        LESS_OR,
        GREATER_AND,
        GREATER_OR
    };
    public enum OpFilterType
    {
        AND,
        OR
    };
}
