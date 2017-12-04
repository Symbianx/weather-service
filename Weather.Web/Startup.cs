using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Weather.Web.Middlewares;
using WeatherLib;

namespace Weather.Web
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
            services.Configure<OpenWeatherMapOptions>(Configuration.GetSection("OpenWeatherMap"));
            services.AddMvc();
            services.AddLogging();

            services.AddTransient<IWeatherService, OpenWeatherMapService>(serviceCollection =>
            {
                var options = serviceCollection.GetRequiredService<IOptionsSnapshot<OpenWeatherMapOptions>>();
                return new OpenWeatherMapService(options.Value.ApiKey);
            }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionLoggingMiddleware>();
            app.UseMiddleware<ExtraHeaderMiddleware>();

            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            app.UseMvc();
        }
    }
}
