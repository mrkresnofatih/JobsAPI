using System;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace JobsApi.JobsCore.Repositories
{
    public static class CacheContext
    {
        private static ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            var mtRedisHost = Environment.GetEnvironmentVariable("REDIS_HOST");
            var mtRedisPort = Environment.GetEnvironmentVariable("REDIS_PORT");
            var mtRedisPass = Environment.GetEnvironmentVariable("MT_REDIS_PASS");
            var connectionString = $"{mtRedisHost}:{mtRedisPort},abortConnect=false,password={mtRedisPass}";
            return ConnectionMultiplexer.Connect(connectionString);
        }

        public static void AddRedisCacheService(this IServiceCollection services)
        {
            var redis = CreateConnectionMultiplexer();
            services.AddSingleton(redis.GetDatabase());
        }
    }
}