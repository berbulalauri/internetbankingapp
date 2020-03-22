using System;
using System.Runtime.Serialization;

namespace BBS.Core.CustomExceptions
{
    [Serializable]
    public class LoanTermOutOfBoundException : Exception
    {
        public LoanTermOutOfBoundException() { }

        public LoanTermOutOfBoundException(string message) : base(message) { }

        public LoanTermOutOfBoundException(string message, Exception innerException) : base(message, innerException) { }

        protected LoanTermOutOfBoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
