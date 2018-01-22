using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace EnvironmentalStressor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public const string ApplicationName = "Environmental Stressor";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = ApplicationName,
                        Version = "v1",
                        Description = "This is an ASP.NET Core web application for test environment (like a server or cluster)."
                    });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            ConfigureLog(app, env, loggerFactory);
            app.UseGracefullShutdown(applicationLifetime, loggerFactory);
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApplicationName);
            });

            // Simulate long running startup
            //var time = new Random().Next(5000, 25000);
            //Log.Logger.Information($"{nameof(Configure)}: Waiting for {time} ms", time);
            //Thread.Sleep(time);
            //Log.Logger.Information($"{nameof(Configure)}: Successfully waitted for {time} ms", time);
        }

        private void ConfigureLog(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Async(a => a.Console(new JsonFormatter()))
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(Configuration)
                .MinimumLevel.Is(Serilog.Events.LogEventLevel.Verbose)
                .CreateLogger();

            loggerFactory.AddSerilog();
        }
    }
}
