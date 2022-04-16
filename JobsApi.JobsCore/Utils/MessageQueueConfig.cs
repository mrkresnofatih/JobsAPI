using System;
using Amazon;
using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;

namespace JobsApi.JobsCore.Utils
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
            var sqsRegion = Environment.GetEnvironmentVariable("SQS_REGION");
            var awsRegionEndpoint = RegionEndpoint.GetBySystemName(sqsRegion);
            return new AmazonSQSClient(sqsAccessKey, sqsSecret, awsRegionEndpoint);
        }
    }
}