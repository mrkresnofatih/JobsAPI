using System.Collections.Generic;
using System.Threading.Tasks;
using JobsApi.JobsCore.Models;
using JobsApi.JobsCore.Utils;

namespace JobsApi.JobsCore.Controllers
{
    public interface IJobController
    {
        Task<ResponsePayload<Job>> SaveUserJob(TraceableQueuePayload<JobCreateDto> jobCreateDtoTraceableQueuePayload);

        Task<ResponsePayload<Job>> GetUserJobByJobId(TraceableQueuePayload<JobGetDto> 
            jobGetDtoTraceableQueuePayload);

        Task<ResponsePayload<Dictionary<string, Job>>> GetUserJobList(TraceableQueuePayload<JobListGetDto> 
            jobListGetDtoTraceableQueuePayload);
    }
}