using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        // Default constructor
        public BusinessException()
        {
        }

        // Constructor with exception message
        public BusinessException(string message)
            : base(message)
        {

        }

        // Constructor with message and inner exception
        public BusinessException(string message, Exception inner)
            : base(message, inner)
        {

        }

        // Protected constructor to de-serialize data
        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
