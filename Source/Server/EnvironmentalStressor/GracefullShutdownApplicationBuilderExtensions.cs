using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EnvironmentalStressor
{
    //public static class GracefullShutdownApplicationBuilderExtensions
    //{
    //    private static int requestCount = 0;
    //    private static object lockPad = new object();
    //    private static ILogger logger;

    //    public static IApplicationBuilder UseGracefullShutdown(this IApplicationBuilder applicationBuilder,
    //        IApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory)
    //    {
    //        logger = loggerFactory.CreateLogger("GracefullShutdown");

    //        applicationBuilder.Use(async (HttpContext context, Func<Task> next) =>
    //        {
    //            lock (lockPad)
    //            {
    //                requestCount++;
    //            }

    //            await next.Invoke();

    //            lock (lockPad)
    //            {
    //                requestCount--;
    //            }
    //        });

    //        applicationLifetime.ApplicationStopping.Register(() =>
    //        {
    //            logger.LogInformation("Application stopping, waiting for pending requests to complete...");

    //            do
    //            {
    //                Task.Delay(1000).Wait();
    //                logger.LogInformation($"Current request count: {requestCount}");
    //            }
    //            while (requestCount > 0);

    //            logger.LogInformation("Done! Application will now stop.");
    //        });

    //        return applicationBuilder;
    //    }
    //}
}
