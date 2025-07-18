using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MarketplaceDataSync.Models
{
    public class ChangeLog
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public DateTime Timestamp { get; set; }
        public string Action { get; set; } = null!;
        public string DataJson { get; set; } = null!;
    }

}
