using System;

namespace JobsApi.JobsCore.Utils
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() : base(ErrorCodes.RecordNotFound)
        {
        }
    }
    
    public class UsernameTakenException : Exception
    {
        public UsernameTakenException() : base(ErrorCodes.UsernameTaken)
        {
        }
    }
    
    public class InvalidCredException : Exception
    {
        public InvalidCredException() : base(ErrorCodes.InvalidCredentials)
        {
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