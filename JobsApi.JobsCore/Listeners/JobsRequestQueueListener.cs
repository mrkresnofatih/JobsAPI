using System;
using System.Threading.Tasks;
using Amazon.SQS;
using JobsApi.JobsCore.Models;
using JobsApi.JobsCore.Services;
using JobsApi.JobsCore.Templates;
using JobsApi.JobsCore.Utils;
using Newtonsoft.Json;

namespace JobsApi.JobsCore.Listeners
{
    public class JobsRequestQueueListener : QueueListenerTemplate
    {
        public JobsRequestQueueListener(JobService jobService, AmazonSQSClient amazonSqsClient,
            SocketUtility socketUtility) : base(amazonSqsClient)
        {
            _jobService = jobService;
            _socketUtility = socketUtility;
        }

        private readonly SocketUtility _socketUtility;
        private readonly JobService _jobService;

        protected override void ProcessQueueMessage(string stringObject)
        {
            var traceableObject = JsonConvert
                .DeserializeObject<TraceableQueuePayload<string>>(stringObject);

            if (traceableObject == null)
            {
                return;
            }

            switch (traceableObject.RequestAddress)
            {
                case JobsCoreRequestAddress.SaveOneJob:
                    var traceableSaveOneJobReq = ParseTraceableQueuePayloadString<JobCreateDto>(traceableObject);
                    var traceableSaveOneJobRes = Task.Run(() => _jobService.SaveUserJob(traceableSaveOneJobReq)).Result;
                    Task.Run(() => _socketUtility
                        .DischargeAsync(
                            ChannelGetterUtility.GetHomeChannel(traceableSaveOneJobRes.Username),
                            ChannelEventNames.HomeReceiveOneJob,
                            traceableSaveOneJobRes)
                    );
                    break;
                
                case JobsCoreRequestAddress.GetOneJob:
                    var traceableGetOneJobReq = ParseTraceableQueuePayloadString<JobGetDto>(traceableObject);
                    var traceableGetOneJobRes = Task.Run(() => _jobService.GetUserJobById(traceableGetOneJobReq)).Result;
                    Task.Run(() => _socketUtility
                        .DischargeAsync(
                            ChannelGetterUtility.GetHomeChannel(traceableGetOneJobRes.Username),
                            ChannelEventNames.HomeReceiveOneJob,
                            traceableGetOneJobRes)
                    );
                    break;
                
                case JobsCoreRequestAddress.GetJobList:
                    var traceableGetJobListReq = ParseTraceableQueuePayloadString<JobListGetDto>(traceableObject);
                    var parsedGetJobListRes = Task.Run(() => _jobService.GetUserJobsList(traceableGetJobListReq)).Result;
                    Task.Run(() => _socketUtility
                        .DischargeAsync(
                            ChannelGetterUtility.GetHomeChannel(traceableGetJobListReq.Data.Username),
                            ChannelEventNames.HomeReceiveJobList,
                            parsedGetJobListRes)
                    );
                    break;
                
                case JobsCoreRequestAddress.DeleteOneJob:
                    var traceableDeleteJobReq = ParseTraceableQueuePayloadString<JobGetDto>(traceableObject);
                    Task.Run(() => _jobService.DeleteUserJobById(traceableDeleteJobReq));
                    break;
            }
        }

        private static TraceableQueuePayload<T> ParseTraceableQueuePayloadString<T>(TraceableQueuePayload<string> traceableObject)
        {
            var parsed = JsonConvert
                .DeserializeObject<T>(traceableObject.Data);
            return TraceableQueueBuilder
                .Build(parsed, traceableObject.SpanId, traceableObject.RequestAddress);
        }

        protected override string GetQueueUrl()
        {
            var sqsSecret = Environment.GetEnvironmentVariable("SQS_JOBSCORE_URL");
            return sqsSecret;
        }
    }
}