using System;
using System.Runtime.Serialization;

namespace BBS.Core.CustomExceptions
{
    [Serializable]
    public class LoanSumOutOfBoundException : Exception
    {
        public LoanSumOutOfBoundException() { }

        public LoanSumOutOfBoundException(string message) : base(message) { }

        public LoanSumOutOfBoundException(string message, Exception innerException) : base(message, innerException) { }

        protected LoanSumOutOfBoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}