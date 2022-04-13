using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace JobsApi.JobsCore.Templates
{
    public abstract class CacheTemplate<T> where T : class
    {
        private readonly IDatabase _redisDb;

        protected CacheTemplate(IDatabase redisDb)
        {
            _redisDb = redisDb;
        }

        protected abstract string GetPrefix();

        protected abstract TimeSpan GetLifeTime();

        private string GetKeyById(string id)
        {
            return $"{GetPrefix()}#{id}";
        }

        protected async Task SaveById(string id, T entity)
        {
            var key = GetKeyById(id);
            await SaveByKey(key, entity);
        }

        private async Task SaveByKey(string key, T entity)
        {
            var serializedEntity = JsonConvert.SerializeObject(entity);
            await _redisDb.StringSetAsync(key, serializedEntity, GetLifeTime());
        }

        protected async Task<T> GetById(string id)
        {
            var key = GetKeyById(id);
            return await GetByKey(key);
        }

        private async Task<T> GetByKey(string key)
        {
            var value = await _redisDb.StringGetAsync(key);
            return value.IsNull ? null : JsonConvert.DeserializeObject<T>(value);
        }

        protected async Task DeleteById(string id)
        {
            var key = GetKeyById(id);
            await DeleteByKey(key);
        }

        private async Task DeleteByKey(string key)
        {
            await _redisDb.KeyDeleteAsync(key);
        }
    }
}