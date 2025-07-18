using MarketplaceDataSync.Data;
using MarketplaceDataSync.Mongo;
using MarketplaceDataSync.Background;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceDataSync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Подключение EF Core
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=marketplace.db"));

            // Подключение MongoDB логера
            builder.Services.AddSingleton<IMongoLogService, MongoLogService>();

            // Подключение фоновой службы
            builder.Services.AddHostedService<ProductSyncService>();

            builder.Services.AddHttpClient("Marketplace", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7203/api/");
            });

            // Добавление Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Marketplace API",
                    Description = "Сервис синхронизации с маркетплейсом"
                });
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Marketplace API v1");
                });

            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
