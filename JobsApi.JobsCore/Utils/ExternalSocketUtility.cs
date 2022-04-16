using System;
using Microsoft.Extensions.DependencyInjection;
using PusherServer;

namespace JobsApi.JobsCore.Utils
{
    public static class ExternalSocketUtility
    {
        private static Pusher CreatePusherInstance()
        {
            var pusherAppId = Environment.GetEnvironmentVariable("PUSHER_APP_ID");
            var pusherKey = Environment.GetEnvironmentVariable("PUSHER_KEY");
            var pusherSecret = Environment.GetEnvironmentVariable("PUSHER_SECRET");
            var pusherCluster = Environment.GetEnvironmentVariable("PUSHER_CLUSTER");
            var pusherOptions = new PusherOptions
            {
                Cluster = pusherCluster,
                Encrypted = true
            };
            return new Pusher(pusherAppId, pusherKey, pusherSecret, pusherOptions);
        }

        public static void AddPusherAsExternalSockets(this IServiceCollection services)
        {
            services.AddSingleton(CreatePusherInstance());
        }
    }
}