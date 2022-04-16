using System;
using System.Threading.Tasks;
using Amazon.SQS;

namespace JobsApi.AuthedGateway.Utils
{
    public class JobsCoreQueueUtility : MessageQueueUtilityTemplate
    {
        public JobsCoreQueueUtility(AmazonSQSClient amazonSqsClient) : base(amazonSqsClient)
        {
        }

        protected override string GetQueueUrl()
        {
            var sqsSecret = Environment.GetEnvironmentVariable("SQS_JOBSCORE_URL");
            return sqsSecret;
        }

        public async Task QueueJobsCoreRequest(TraceableQueuePayload<string> jobsCoreRequest)
        {
            await PushMessageToQueue(jobsCoreRequest);
        }
    }
}