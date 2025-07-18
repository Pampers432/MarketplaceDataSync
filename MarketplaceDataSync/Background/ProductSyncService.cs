using System.Text.Json;
using MarketplaceDataSync.Data;
using MarketplaceDataSync.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceDataSync.Background
{
    public class ProductSyncService : BackgroundService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ProductSyncService> _logger;

        public ProductSyncService(
            IHttpClientFactory httpFactory,
            IServiceScopeFactory scopeFactory,
            ILogger<ProductSyncService> logger)
        {
            _httpFactory = httpFactory;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var client = _httpFactory.CreateClient("Marketplace");

                try
                {
                    var response = await client.GetAsync("/api/products", stoppingToken);
                    var json = await response.Content.ReadAsStringAsync();
                    var products = JsonSerializer.Deserialize<List<Product>>(json);

                    if (products != null)
                    {
                        db.Products.RemoveRange(db.Products);
                        await db.Products.AddRangeAsync(products, stoppingToken);
                        await db.SaveChangesAsync(stoppingToken);
                    }

                    _logger.LogInformation("Синхронизация завершена успешно");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при синхронизации");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}
