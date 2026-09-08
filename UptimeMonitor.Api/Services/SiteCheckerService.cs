using Microsoft.EntityFrameworkCore;
using UptimeMonitor.Api.Models;

namespace UptimeMonitor.Api.Services
{
    public class SiteCheckerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SiteCheckerService> _logger;

        public SiteCheckerService(
            IServiceScopeFactory scopeFactory,
            IHttpClientFactory httpClientFactory,
            ILogger<SiteCheckerService> logger)
        {
            _scopeFactory = scopeFactory;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("background monitoring started");

            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckSitesAsync();

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        private async Task CheckSitesAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var httpClient = _httpClientFactory.CreateClient();

            var sites = await dbContext.Sites.ToListAsync();
            if (!sites.Any()) return;

            var pingTasks = sites.Select(site => PingSiteAsync(site, httpClient));

            await Task.WhenAll(pingTasks);

            await dbContext.SaveChangesAsync();
            _logger.LogInformation($"Checked {sites.Count} sites at {DateTime.Now}");
        }

        private async Task PingSiteAsync(MonitoredSite site, HttpClient client)
        {
            try
            {
                var responce = await client.GetAsync(site.Url);

                site.IsOnline = responce.IsSuccessStatusCode;
            }
            catch
            {
                site.IsOnline = false;
            }
            finally
            {
                site.LastChecked = DateTime.UtcNow;
            }
        }
    }
}
