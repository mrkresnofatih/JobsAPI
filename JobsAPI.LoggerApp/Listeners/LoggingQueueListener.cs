using System;
using Amazon.SQS;
using JobsAPI.LoggerApp.Models;
using JobsAPI.LoggerApp.Templates;
using Newtonsoft.Json;
using Serilog.Core;

namespace JobsAPI.LoggerApp.Listeners
{
    public class LoggingQueueListener : QueueListenerTemplate
    {
        public LoggingQueueListener(Logger logger, AmazonSQSClient amazonSqsClient) : base(amazonSqsClient)
        {
            _logger = logger;
        }

        private readonly Logger _logger;

        protected override void ProcessQueueMessage(string stringObject)
        {
            var logRequest = JsonConvert.DeserializeObject<LogRequest>(stringObject);

            if (logRequest == null)
            {
                return;
            }

            var logMessage = logRequest.SpanId + "\n\t"
                                               + logRequest.ApplicationName + "\n\t"
                                               + logRequest.Body;

            switch (logRequest.LogType)
            {
                case LogTypes.Information:
                    _logger
                        .Information(logMessage);
                    break;
                case LogTypes.Error:
                    _logger
                        .Error(logMessage);
                    break;
            }
        }

        protected override string GetQueueUrl()
        {
            var sqsSecret = Environment.GetEnvironmentVariable("SQS_LOGGER_URL");
            return sqsSecret;
        }
    }
}