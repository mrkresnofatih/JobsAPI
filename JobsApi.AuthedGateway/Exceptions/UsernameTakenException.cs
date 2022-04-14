using System;

namespace JobsApi.AuthedGateway.Exceptions
{
    public class UsernameTakenException : BaseAppException
    {
        public UsernameTakenException(string spanId) : base(ErrorCodes.UsernameTaken, spanId)
        {
        }
    }
}