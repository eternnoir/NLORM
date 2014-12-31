using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NLORM.Core.Exceptions
{
    [Serializable]
    public class NLORMException : Exception,ISerializable
    {
        public string ErrorCode 
        {
            get 
            {
                return errorCode;
            }
        }
        private string errorCode;

        public NLORMException(string errorCode,string message) :base(message)
        {
            this.errorCode = errorCode;
        }
    }
}
