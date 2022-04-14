using System;

namespace JobsApi.AuthedGateway.Exceptions
{
    public class InvalidCredException : BaseAppException
    {
        public InvalidCredException(string spanId) : base(ErrorCodes.InvalidCredentials, spanId)
        {
        }
    }
}