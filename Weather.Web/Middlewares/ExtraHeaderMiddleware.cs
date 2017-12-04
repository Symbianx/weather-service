using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Weather.Web.Middlewares
{
    public class ExtraHeaderMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExtraHeaderMiddleware> logger;

        public ExtraHeaderMiddleware(RequestDelegate next, ILogger<ExtraHeaderMiddleware> logger)
        {
            this.next = next ?? throw new System.ArgumentNullException(nameof(next));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public Task Invoke(HttpContext httpContext)
        {
            var watch = new Stopwatch();
            watch.Start();

            httpContext.Response.OnStarting(state =>
            {
                var context = state as HttpContext;
                if (context != null)
                    context.Response.Headers.Add("X-Time-Taken", new[] { watch.ElapsedMilliseconds.ToString() });

                return Task.CompletedTask;
            }, httpContext);

            return next(httpContext);
        }
    }
}