using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Hosting;

namespace JobsApi.JobsCore.Templates
{
    public abstract class QueueListenerTemplate : BackgroundService
    {
        protected QueueListenerTemplate(AmazonSQSClient amazonSqsClient)
        {
            _amazonSqsClient = amazonSqsClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = new ReceiveMessageRequest()
                {
                    QueueUrl = GetQueueUrl()
                };

                var response = await _amazonSqsClient.ReceiveMessageAsync(request);
                foreach (var message in response.Messages)
                {
                    var queueMessage = message.Body;
                    ProcessQueueMessage(queueMessage);
                    await _amazonSqsClient.DeleteMessageAsync(GetQueueUrl(), message.ReceiptHandle);
                }
            }
        }

        protected abstract void ProcessQueueMessage(string stringObject);

        private readonly AmazonSQSClient _amazonSqsClient;

        protected abstract string GetQueueUrl();
    }
}