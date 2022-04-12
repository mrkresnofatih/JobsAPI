using System;
using Amazon;
using Amazon.CloudWatchLogs;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.AwsCloudWatch;

namespace JobsAPI.LoggerApp.Utils
{
    public static class LoggingUtility
    {
        private static Logger CreateLogger()
        {
            var cloudWatchAccessKey = Environment.GetEnvironmentVariable("CLOUDWATCH_ACCESSKEY");
            var cloudWatchSecret = Environment.GetEnvironmentVariable("CLOUDWATCH_SECRET");
            var cloudWatchRegion = Environment.GetEnvironmentVariable("CLOUDWATCH_AWSREGION");
            var cloudWatchLogGroup = Environment.GetEnvironmentVariable("CLOUDWATCH_LOGGROUP");
            var awsRegionEndpoint = RegionEndpoint.GetBySystemName(cloudWatchRegion);

            var client = new AmazonCloudWatchLogsClient(
                cloudWatchAccessKey, 
                cloudWatchSecret, 
                awsRegionEndpoint);

            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.AmazonCloudWatch(
                    logGroup: cloudWatchLogGroup,
                    logStreamPrefix: DateTime.UtcNow.ToString("yyyy-M-d dddd"),
                    cloudWatchClient: client)
                .WriteTo.Console()
                .CreateLogger();
        }

        public static void AddLoggingUtility(this IServiceCollection services)
        {
            services.AddSingleton(CreateLogger());
        }
    }
}