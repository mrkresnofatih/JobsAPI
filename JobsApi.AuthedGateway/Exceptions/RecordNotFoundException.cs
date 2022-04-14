using System;

namespace JobsApi.AuthedGateway.Exceptions
{
    public class RecordNotFoundException : BaseAppException
    {
        public RecordNotFoundException(string spanId) : base(ErrorCodes.RecordNotFound, spanId)
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