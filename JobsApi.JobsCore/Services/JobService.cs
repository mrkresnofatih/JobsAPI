using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobsApi.JobsCore.Models;
using JobsApi.JobsCore.Repositories;
using JobsApi.JobsCore.Utils;

namespace JobsApi.JobsCore.Services
{
    public class JobService : IJobService
    {
        public JobService(JobListCacheRepo jobListCacheRepo, IMapper mapper)
        {
            _mapper = mapper;
            _jobListCacheRepo = jobListCacheRepo;
        }

        private readonly IMapper _mapper;
        private readonly JobListCacheRepo _jobListCacheRepo;

        public async Task<Job> SaveUserJob(JobCreateDto jobCreateDto)
        {
            var newJob = _mapper.Map<Job>(jobCreateDto);
            newJob.JobId = ShortIdGenerator.GenerateId();
            newJob.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            newJob.Completed = false;

            var foundJobList = await _jobListCacheRepo
                .GetByUsername(jobCreateDto.Username);

            if (foundJobList == null)
            {
                var jobList = new Dictionary<string, Job>
                {
                    {newJob.JobId, newJob}
                };
                await _jobListCacheRepo
                    .SaveByUsername(jobCreateDto.Username, jobList);
                return newJob;
            }
            
            foundJobList.Add(newJob.JobId, newJob);
            await _jobListCacheRepo
                .SaveByUsername(jobCreateDto.Username, foundJobList);
            return newJob;
        }

        public async Task<Job> GetUserJobById(string username, string jobId)
        {
            var foundJobList = await _jobListCacheRepo
                .GetByUsername(username);
            if (foundJobList == null)
            {
                throw new RecordNotFoundException();
            }
            var foundJob = foundJobList[jobId];
            if (foundJob == null)
            {
                throw new RecordNotFoundException();
            }
            return foundJob;
        }

        public async Task<Dictionary<string, Job>> GetUserJobsList(string username)
        {
            var foundJobList = await _jobListCacheRepo
                .GetByUsername(username);
            
            if (foundJobList == null)
            {
                return new Dictionary<string, Job>();
            }

            return foundJobList;
        }
    }
}