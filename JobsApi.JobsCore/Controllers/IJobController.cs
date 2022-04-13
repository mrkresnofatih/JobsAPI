using System.Collections.Generic;
using System.Threading.Tasks;
using JobsApi.JobsCore.Models;
using JobsApi.JobsCore.Utils;

namespace JobsApi.JobsCore.Controllers
{
    public interface IJobController
    {
        Task<ResponsePayload<Job>> SaveUserJob(JobCreateDto jobCreateDto);

        Task<ResponsePayload<Job>> GetUserJobByJobId(string username, string jobId);

        Task<ResponsePayload<Dictionary<string, Job>>> GetUserJobList(string username);
    }
}