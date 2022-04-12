using System;

namespace JobsApi.AuthedGateway.Exceptions
{
    public class UsernameTakenException : Exception
    {
        public UsernameTakenException() : base(ErrorCodes.UsernameTaken)
        {
        }
    }
}