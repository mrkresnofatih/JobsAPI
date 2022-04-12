using System;
using System.Threading.Tasks;
using JobsApi.AuthedGateway.Templates;
using StackExchange.Redis;

namespace JobsApi.AuthedGateway.Repositories
{
    public class AccessTokenCache : CacheTemplate<string>, IAccessTokenCache
    {
        public AccessTokenCache(IDatabase redisDb) : base(redisDb)
        {
        }

        protected override string GetPrefix()
        {
            return "ACCESSTOKEN";
        }

        protected override TimeSpan GetLifeTime()
        {
            return TimeSpan.FromDays(1);
        }

        public async Task SaveByUsername(string username, string token)
        {
            await SaveById(username, token);
        }

        public async Task<string> GetByUsername(string username)
        {
            return await GetById(username);
        }

        public async Task DeleteByUsername(string username)
        {
            await DeleteById(username);
        }
    }
}