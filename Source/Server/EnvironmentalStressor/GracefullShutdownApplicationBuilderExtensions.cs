using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EnvironmentalStressor
{
    public static class GracefullShutdownApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGracefullShutdown(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<GracefullShutdownMiddleware>();

            return applicationBuilder;
        }
    }
}
