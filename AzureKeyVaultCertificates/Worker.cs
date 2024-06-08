using System.Net.Http;

namespace AzureKeyVaultCertificates
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var httpClient  = _httpClientFactory.CreateClient("GatewayAPI");

                await httpClient.GetAsync("/health");

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}