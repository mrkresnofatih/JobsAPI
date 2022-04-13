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
    public class JobController : IJobController
    {
        public JobController(JobService jobService)
        {
            _jobService = jobService;
        }

        private readonly JobService _jobService;

        [HttpPost("save")]
        public async Task<ResponsePayload<Job>> SaveUserJob([FromBody] JobCreateDto jobCreateDto)
        {
            var res = await _jobService.SaveUserJob(jobCreateDto);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("get/{username}/{jobId}")]
        [TypeFilter(typeof(ExceptionLoggingQueueFilterAttribute))]
        public async Task<ResponsePayload<Job>> GetUserJobByJobId(string username, string jobId)
        {
            var res = await _jobService.GetUserJobById(username, jobId);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("getAll/{username}")]
        public async Task<ResponsePayload<Dictionary<string, Job>>> GetUserJobList(string username)
        {
            var res = await _jobService.GetUserJobsList(username);
            return ResponseHandler.WrapSuccess(res);
        }
    }
}