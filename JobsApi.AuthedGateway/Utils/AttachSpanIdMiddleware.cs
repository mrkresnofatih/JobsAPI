using System.Threading.Tasks;
using JobsApi.AuthedGateway.Constants;
using Microsoft.AspNetCore.Http;

namespace JobsApi.AuthedGateway.Utils
{
    public class AttachSpanIdMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var newSpanId = ShortIdGenerator.GenerateId();
            context.Request.Headers.Add(CustomHeaders.SpanIdHeader, newSpanId);
            await next(context);
        }
    }
}