using System;
using System.Threading.Tasks;
using JobsApi.AuthedGateway.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobsApi.AuthedGateway.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionLoggingQueueFilterAttribute : ExceptionFilterAttribute
    {
        public ExceptionLoggingQueueFilterAttribute(LoggingQueueUtility loggingQueueUtility)
        {
            _loggingQueueUtility = loggingQueueUtility;
        }
        private readonly LoggingQueueUtility _loggingQueueUtility;

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            var spanId = ((BaseAppException) context.Exception).GetSpanId();
            var exceptionType = exception.GetType().FullName;
            var stackTrace = exception.StackTrace;

            await _loggingQueueUtility
                .QueueErrorLog(spanId, exceptionType, stackTrace);
        }
    }
}