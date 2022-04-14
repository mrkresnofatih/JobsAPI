using System.Collections.Generic;
using System.Threading.Tasks;
using JobsApi.JobsCore.Models;
using JobsApi.JobsCore.Utils;

namespace JobsApi.JobsCore.Services
{
    public interface IJobService
    {
        Task<Job> SaveUserJob(TraceableQueuePayload<JobCreateDto> 
            jobCreateDtoTraceableQueuePayload);

        Task<Job> GetUserJobById(TraceableQueuePayload<JobGetDto> 
            jobGetDtoTraceableQueuePayload);

        Task<Dictionary<string, Job>> GetUserJobsList(TraceableQueuePayload<JobListGetDto> 
            jobListGetDtoTraceableQueuePayload);
    }
}