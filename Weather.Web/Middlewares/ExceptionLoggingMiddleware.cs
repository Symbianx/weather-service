using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Web.Middlewares
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionLoggingMiddleware> logger;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "An exception occurred handling a request.");
                if (!httpContext.Response.HasStarted) {
                    httpContext.Response.StatusCode = 500;
                    await httpContext.Response.WriteAsync("Sorry, this one is our fault :(");
                }
            }
        }
    }
}
