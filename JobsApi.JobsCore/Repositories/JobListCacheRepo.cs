using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobsApi.JobsCore.Models;
using JobsApi.JobsCore.Templates;
using StackExchange.Redis;

namespace JobsApi.JobsCore.Repositories
{
    public class JobListCacheRepo : CacheTemplate<Dictionary<string, Job>>, IJobListCacheRepo
    {
        public JobListCacheRepo(IDatabase redisDb) : base(redisDb)
        {
        }

        protected override string GetPrefix()
        {
            return "JOBLIST";
        }

        protected override TimeSpan GetLifeTime()
        {
            return TimeSpan.FromDays(1);
        }

        public async Task SaveByUsername(string username, Dictionary<string, Job> jobList)
        {
            await SaveById(username, jobList);
        }

        public async Task<Dictionary<string, Job>> GetByUsername(string username)
        {
            return await GetById(username);
        }

        public async Task DeleteByUsername(string username)
        {
            await DeleteById(username);
        }
    }
}