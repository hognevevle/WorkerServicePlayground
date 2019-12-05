using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerServicePlayground
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Throw();

                _logger.LogInformation("We made it!");
            }
        }

        private async Task Throw()
        {
            await Task.Delay(1000);

            _logger.LogInformation("Now throwing...");

            throw new Exception("Ooops"); // And we have a hang... 
        }
    }
}
