using MarketplaceDataSync.Models;

namespace MarketplaceDataSync.Mongo
{
    public interface IMongoLogService
    {
        Task LogAsync(ChangeLog log);
    }
}
