using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;

namespace JobsApi.AuthedGateway.Utils
{
    public abstract class MessageQueueUtilityTemplate : IMessageQueueUtility
    {
        protected MessageQueueUtilityTemplate(AmazonSQSClient amazonSqsClient)
        {
            _amazonSqsClient = amazonSqsClient;
        }

        private readonly AmazonSQSClient _amazonSqsClient;

        public async Task PushMessageToQueue(object data)
        {
            var request = new SendMessageRequest
            {
                MessageBody = JsonConvert.SerializeObject(data),
                QueueUrl = GetQueueUrl()
            };
            await _amazonSqsClient.SendMessageAsync(request);
        }

        protected abstract string GetQueueUrl();

        protected abstract string GetApplicationName();
    }
}