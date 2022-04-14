using System;

namespace JobsApi.JobsCore.Utils
{
    public class RecordNotFoundException : BaseAppException
    {
        public RecordNotFoundException(string spanId) : base(ErrorCodes.RecordNotFound, spanId)
        {
        }
    }
    
    public class UsernameTakenException : BaseAppException
    {
        public UsernameTakenException(string spanId) : base(ErrorCodes.UsernameTaken, spanId)
        {
        }
    }
    
    public class InvalidCredException : BaseAppException
    {
        public InvalidCredException(string spanId) : base(ErrorCodes.InvalidCredentials, spanId)
        {
        }
    }
    
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

    public static class ErrorCodes
    {
        public const string InvalidCredentials = "INVALID_CREDENTIALS";
        public const string RecordNotFound = "RECORD_NOT_FOUND";
        public const string UsernameTaken = "USERNAME_TAKEN";
        public const string Unhandled = "UNHANDLED";
        public const string BadRequest = "BAD_REQUEST";
    }
}