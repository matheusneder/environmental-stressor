using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KubernetesValidation.Controllers
{
    [Route("[controller]")]
    public class SimulationsController : Controller
    {
        private readonly ILogger logger;

        public SimulationsController(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory?.CreateLogger<SimulationsController>() ??
                throw new ArgumentNullException(nameof(loggerFactory));
        }

        [HttpGet(nameof(LongRunning))]
        public IActionResult LongRunning()
        {
            var time = new Random().Next(5000, 25000);
            logger.LogInformation($"{nameof(LongRunning)}: Waiting for {time} ms", time);
            Thread.Sleep(time);
            logger.LogInformation($"{nameof(LongRunning)}: Succesfully waited for {time} ms, will now return Ok.");
            return Ok();
        }

        [HttpGet(nameof(OutOfMemory))]
        public void OutOfMemory()
        {
            var l = new List<long[]>();
            logger.LogInformation($"{nameof(OutOfMemory)}: Trying to cause an OutOfMemory Exception");
            for (; ; )
            {
                l.Add(new long[102400]);
            }
        }

        private static readonly List<byte[]> memoryLeakHelper = new List<byte[]>();

        [HttpGet(nameof(MemoryLeak))]
        public IActionResult MemoryLeak()
        {
            var arraySize = 10 * 1024 * 1024; // 10MB

            logger.LogInformation($"{nameof(MemoryLeak)}: Adding {arraySize / 1024 / 1024} MB");
            memoryLeakHelper.Add(new byte[arraySize]);

            return Ok();
        }

        [HttpGet(nameof(HighCpuUsage))]
        public IActionResult HighCpuUsage()
        {
            // will run for about 20 to 45 seconds

            int timeToRun = new Random().Next(20000, 45000);
            var runUpTo = DateTimeOffset.Now.AddMilliseconds(timeToRun);
            var max = Int32.MaxValue / 4;

            logger.LogInformation($"{nameof(HighCpuUsage)}: Simulating high cpu usage for about {timeToRun} ms");

            while (DateTimeOffset.Now < runUpTo)
            {
                for (int i = 0; i < max; i++) ;
            }

            logger.LogInformation($"{nameof(HighCpuUsage)}: Finished high cpu usage. Runned for about {timeToRun} ms");

            return Ok();
        }


        [HttpGet(nameof(HighThroughput) + "/{megabytes}")]
        public void HighThroughput(int megabytes)
        {
            logger.LogInformation($"{nameof(HighThroughput)}: Start streaming randon data with {megabytes} MB of lenght.");

            var random = new Random();
            HttpContext.Response.ContentType = "application/octet-stream";
            HttpContext.Response.ContentLength = 1024 * 1024 * megabytes;

            for (var i = 0; i < megabytes; i++)
            {
                var buffer = new Byte[1024 * 1024];
                random.NextBytes(buffer);
                HttpContext.Response.Body.Write(buffer, 0, buffer.Length);

                if (i % 100 == 0)
                {
                    logger.LogInformation($"{nameof(HighThroughput)}: Streaming... ({i} MB)");
                }
            }

            logger.LogInformation($"{nameof(HighThroughput)}: Successfully streamed {megabytes} MB of random data.");
        }
    }
}
