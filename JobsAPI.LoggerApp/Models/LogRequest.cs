namespace JobsAPI.LoggerApp.Models
{
    public class LogRequest
    {
        public string ApplicationName { get; set; }
        public string SpanId { get; set; }
        public string LogType { get; set; }
        public string Body { get; set; }
    }

    public static class LogRequestBuilder
    {
        public static LogRequest BuildErrorLogRequest(string applicationName, string spanId, 
            string exceptionClass, string stackTrace)
        {
            return new LogRequest
            {
                ApplicationName = applicationName,
                LogType = LogTypes.Error,
                SpanId = spanId,
                Body = $"{exceptionClass}\n\t{stackTrace}"
            };
        }
        
        public static LogRequest BuildInfoLogRequest(string applicationName, string spanId, 
            string message)
        {
            return new LogRequest
            {
                ApplicationName = applicationName,
                LogType = LogTypes.Information,
                SpanId = spanId,
                Body = message
            };
        }
    }

    public static class LogTypes
    {
        public const string Error = "ERROR";
        public const string Information = "INFORMATION";
    }
}