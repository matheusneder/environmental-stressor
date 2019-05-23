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

        [HttpGet("Damd")]
        public IActionResult HelloDamd()
        {
            return Ok("Ole Damd!");
        }

        [HttpGet("LongRunning/{sleepForMiliseconds}")]
        public IActionResult LongRunning(int sleepForMiliseconds)
        {
            logger.LogInformation($"{nameof(LongRunning)}: Waiting for {sleepForMiliseconds} ms", sleepForMiliseconds);
            Thread.Sleep(sleepForMiliseconds);
            logger.LogInformation($"{nameof(LongRunning)}: Succesfully waited for {sleepForMiliseconds} ms, will now return Ok.");

            return Ok();
        }

        [HttpGet("OutOfMemory")]
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

        [HttpGet("MemoryLeak")]
        public IActionResult MemoryLeak()
        {
            var arraySize = 10 * 1024 * 1024; // 10MB

            logger.LogInformation($"{nameof(MemoryLeak)}: Adding {arraySize / 1024 / 1024} MB");
            memoryLeakHelper.Add(new byte[arraySize]);

            return Ok();
        }

        [HttpGet("HighCpuUsage/{runForMilliseconds}")]
        public IActionResult HighCpuUsage(int runForMilliseconds)
        {
            var runUpTo = DateTimeOffset.Now.AddMilliseconds(runForMilliseconds);
            var max = Int32.MaxValue / 4;

            logger.LogInformation($"{nameof(HighCpuUsage)}: Simulating high cpu usage for about {runForMilliseconds} ms");

            while (DateTimeOffset.Now < runUpTo)
            {
                for (int i = 0; i < max; i++) ;
            }

            logger.LogInformation($"{nameof(HighCpuUsage)}: Finished high cpu usage. Runned for about {runForMilliseconds} ms");

            return Ok();
        }


        [HttpGet("HighThroughput/{megabytes}/{sleepForMilliseconds}")]
        public void HighThroughput(int megabytes, int sleepForMilliseconds)
        {
            logger.LogInformation($"{nameof(HighThroughput)}: Start streaming randon data with {megabytes} MB of lenght.");
            HttpContext.Response.ContentType = "application/octet-stream";
            HttpContext.Response.ContentLength = 1024 * 1024 * megabytes;
            Thread.Sleep(sleepForMilliseconds);
            var rand = new Random();

            for (var i = 0; i < megabytes; i++)
            {
                var buffer = new Byte[1024 * 1024];
                rand.NextBytes(buffer);
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
