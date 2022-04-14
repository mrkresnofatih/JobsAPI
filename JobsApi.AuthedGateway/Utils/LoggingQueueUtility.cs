using System;
using System.Threading.Tasks;
using Amazon.SQS;

namespace JobsApi.AuthedGateway.Utils
{
    public class LoggingQueueUtility : MessageQueueUtilityTemplate
    {
        public LoggingQueueUtility(AmazonSQSClient amazonSqsClient) : base(amazonSqsClient)
        {
        }

        protected override string GetQueueUrl()
        {
            var sqsSecret = Environment.GetEnvironmentVariable("SQS_LOGGER_URL");
            return sqsSecret;
        }

        protected override string GetApplicationName()
        {
            return "AuthedGateway";
        }

        public async Task QueueErrorLog(string spanId, 
            string exceptionClass, string stackTrace)
        {
            var packet = LogRequestBuilder
                .BuildErrorLogRequest(GetApplicationName(), spanId, exceptionClass, stackTrace);
            await PushMessageToQueue(packet);
        }
        
        public async Task QueueInfoLog(string spanId, 
            string message)
        {
            var packet = LogRequestBuilder
                .BuildInfoLogRequest(GetApplicationName(), spanId, message);
            await PushMessageToQueue(packet);
        }
    }
    
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