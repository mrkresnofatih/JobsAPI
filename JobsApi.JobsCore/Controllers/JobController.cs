using System.Collections.Generic;
using System.Threading.Tasks;
using JobsApi.JobsCore.Models;
using JobsApi.JobsCore.Services;
using JobsApi.JobsCore.Utils;
using Microsoft.AspNetCore.Mvc;

namespace JobsApi.JobsCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TypeFilter(typeof(ExceptionLoggingQueueFilterAttribute))]
    public class JobController : IJobController
    {
        public JobController(JobService jobService)
        {
            _jobService = jobService;
        }

        private readonly JobService _jobService;

        [HttpPost("save")]
        public async Task<ResponsePayload<Job>> SaveUserJob([FromBody] TraceableQueuePayload<JobCreateDto> jobCreateDtoTraceableQueuePayload)
        {
            var res = await _jobService.SaveUserJob(jobCreateDtoTraceableQueuePayload);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpPost("get")]
        public async Task<ResponsePayload<Job>> GetUserJobByJobId([FromBody] TraceableQueuePayload<JobGetDto> 
            jobGetDtoTraceableQueuePayload)
        {
            var res = await _jobService.GetUserJobById(jobGetDtoTraceableQueuePayload);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpPost("getAll")]
        public async Task<ResponsePayload<Dictionary<string, Job>>> GetUserJobList([FromBody] TraceableQueuePayload<JobListGetDto> 
            jobListGetDtoTraceableQueuePayload)
        {
            var res = await _jobService.GetUserJobsList(jobListGetDtoTraceableQueuePayload);
            return ResponseHandler.WrapSuccess(res);
        }
    }
}