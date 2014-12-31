using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLORM.Core.Exceptions;

namespace NLORM.Core.Exceptions
{
    public class FilterChainException : NLORMException
    {
        public FilterChainException(string message) : base("FCE",message)
        {
        }
    }
}
