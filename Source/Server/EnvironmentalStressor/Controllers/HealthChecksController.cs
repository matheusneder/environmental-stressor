using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace EnvironmentalStressor.Controllers
{
    public class HealthChecksController : Controller
    {
        private readonly ILogger logger;

        public HealthChecksController(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory?.CreateLogger<HealthChecksController>() ?? 
                throw new ArgumentNullException(nameof(loggerFactory));
        }

        [HttpGet("/healthz")]
        public IActionResult Healthz()
        {
            logger.LogInformation($"{nameof(Healthz)}: Just called!");

            return Ok();
        }
    }
}
