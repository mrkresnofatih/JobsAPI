using System;
using System.Threading.Tasks;
using JobsApi.AuthedGateway.Attributes;
using JobsApi.AuthedGateway.Constants;
using JobsApi.AuthedGateway.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobsApi.AuthedGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController
    {
        public AppController(JobsCoreQueueUtility jobsCoreQueueUtility)
        {
            _jobsCoreQueueUtility = jobsCoreQueueUtility;
        }

        private readonly JobsCoreQueueUtility _jobsCoreQueueUtility;

        [HttpPost("jobs")]
        [TypeFilter(typeof(RequireAuthFilterAttribute))]
        [TypeFilter(typeof(ExceptionLoggingQueueFilterAttribute))]
        public async Task<ResponsePayload<string>> ForwardAsyncJobsCoreApi(
            [FromBody] TraceableQueuePayload<object> data, [FromHeader(Name = CustomHeaders.SpanIdHeader)] string spanId)
        {
            var traceable = TraceableQueueBuilder.Build(data.Data.ToString(), spanId, data.RequestAddress);
            await _jobsCoreQueueUtility.QueueJobsCoreRequest(traceable);

            return ResponseHandler.AsynchronousReturn();
        }
    }
}