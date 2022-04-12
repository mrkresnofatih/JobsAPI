using System;
using System.Threading.Tasks;
using JobsApi.AuthedGateway.Models;
using JobsApi.AuthedGateway.Templates;
using StackExchange.Redis;

namespace JobsApi.AuthedGateway.Repositories
{
    public class PlayerCacheRepo : CacheTemplate<Player>, IPlayerCacheRepo
    {
        public PlayerCacheRepo(IDatabase redisDb) : base(redisDb)
        {
        }

        protected override string GetPrefix()
        {
            return "PLAYER";
        }

        protected override TimeSpan GetLifeTime()
        {
            return TimeSpan.FromDays(1);
        }

        public async Task SaveByUsername(string username, Player player)
        {
            await SaveById(username, player);
        }

        public async Task<Player> GetByUsername(string username)
        {
            return await GetById(username);
        }

        public async Task DeleteByUsername(string username)
        {
            await DeleteById(username);
        }
    }
}