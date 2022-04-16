using System;
using Microsoft.Extensions.DependencyInjection;

namespace JobsApi.AuthedGateway.Utils
{
    public static class AppCorsUtility
    {
        public static void AddAppCors(this IServiceCollection services)
        {
            var feAppOrigin = Environment.GetEnvironmentVariable("FE_APP_ORIGIN");
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy => 
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }
    }
}