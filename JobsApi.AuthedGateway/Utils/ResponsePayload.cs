namespace JobsApi.AuthedGateway.Utils
{
    public static class ResponseHandler
    {
        public static ResponsePayload<T> WrapSuccess<T>(T data)
        {
            return new ResponsePayload<T>
            {
                Data = data,
                ErrorCode = ""
            };
        }

        public static ResponsePayload<T> WrapFailure<T>(string errorCode)
        {
            return new ResponsePayload<T>
            {
                ErrorCode = errorCode
            };
        }

        public static ResponsePayload<string> AsynchronousReturn()
        {
            return new ResponsePayload<string>
            {
                Data = "Request Has Been Sent"
            };
        }
    }

    public class ResponsePayload<T>
    {
        public T Data { get; set; }
        
        public string ErrorCode { get; set; }
    }
}