﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobsApi.JobsCore.Utils
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

            await _loggingQueueUtility.QueueErrorLog("jobsCore", DateTime.Now.ToString(),
                exception.GetType().FullName, exception.StackTrace);
        }
    }
}