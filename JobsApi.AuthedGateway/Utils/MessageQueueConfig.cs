using System;
using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;

namespace JobsApi.AuthedGateway.Utils
{
    public static class MessageQueueConfig
    {
        public static void AddMessageQueueClient(this IServiceCollection services)
        {
            services.AddSingleton(CreateAmazonSqsClient());
        }

        private static AmazonSQSClient CreateAmazonSqsClient()
        {
            var sqsAccessKey = Environment.GetEnvironmentVariable("SQS_ACCESSKEY");
            var sqsSecret = Environment.GetEnvironmentVariable("SQS_SECRET");
            return new AmazonSQSClient(sqsAccessKey, sqsSecret);
        }
    }
}