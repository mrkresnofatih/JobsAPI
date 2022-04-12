using System;

namespace JobsApi.AuthedGateway.Exceptions
{
    public class InvalidCredException : Exception
    {
        public InvalidCredException() : base(ErrorCodes.InvalidCredentials)
        {
        }
    }
}