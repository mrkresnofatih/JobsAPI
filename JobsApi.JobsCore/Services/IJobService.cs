using System.Collections.Generic;
using System.Threading.Tasks;
using JobsApi.JobsCore.Models;

namespace JobsApi.JobsCore.Services
{
    public interface IJobService
    {
        Task<Job> SaveUserJob(JobCreateDto jobCreateDto);

        Task<Job> GetUserJobById(string username, string jobId);

        Task<Dictionary<string, Job>> GetUserJobsList(string username);
    }
}