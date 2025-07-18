using MarketplaceDataSync.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MarketplaceDataSync.Mongo
{
    public class MongoLogService : IMongoLogService
    {
        private readonly IMongoCollection<ChangeLog> _collection;

        public MongoLogService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("Mongo"));
            var db = client.GetDatabase("SyncLogs");
            _collection = db.GetCollection<ChangeLog>("Logs");
        }

        public async Task LogAsync(ChangeLog log) =>
            await _collection.InsertOneAsync(log);
    }
}
