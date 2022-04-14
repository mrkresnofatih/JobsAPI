using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobsApi.AuthedGateway.Exceptions;
using JobsApi.AuthedGateway.Repositories;
using JobsApi.AuthedGateway.Services;
using JobsApi.AuthedGateway.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace JobsApi.AuthedGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var logger = context.HttpContext.RequestServices
                            .GetRequiredService<ILogger<Startup>>();
                        logger.LogInformation("Invalid ModelState");
                        return new OkObjectResult(ResponseHandler.WrapFailure<object>(ErrorCodes.BadRequest));
                    };
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobsApi.AuthedGateway", Version = "v1" });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutomapperConfig();
            services.AddScoped<AuthService>();
            services.AddSingleton<PlayerCacheRepo>();
            services.AddRedisCacheService();
            services.AddSingleton<PlayerAccessTokenUtility>();
            services.AddSingleton<AccessTokenCache>();
            services.AddMessageQueueClient();
            services.AddScoped<LoggingQueueUtility>();
            services.AddTransient<AttachSpanIdMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobsApi.AuthedGateway v1"));
            }

            app.UseMiddleware<AttachSpanIdMiddleware>();
            
            app.UseAppExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
