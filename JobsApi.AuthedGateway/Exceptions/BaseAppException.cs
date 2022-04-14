using System;

namespace JobsApi.AuthedGateway.Exceptions
{
    public class BaseAppException : Exception
    {
        public BaseAppException(string errorCode, string spanId) : base(errorCode)
        {
            _spanId = spanId;
        }

        private readonly  string _spanId;

        public string GetSpanId()
        {
            return _spanId;
        }
    }
}