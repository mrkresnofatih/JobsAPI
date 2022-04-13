using System.Collections.Generic;
using System.Threading.Tasks;
using JobsApi.JobsCore.Models;

namespace JobsApi.JobsCore.Repositories
{
    public interface IJobListCacheRepo
    {
        Task SaveByUsername(string username, Dictionary<string, Job> jobList);
        
        Task<Dictionary<string, Job>> GetByUsername(string username);
        
        Task DeleteByUsername(string username);
    }
}