using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeilAFinafrica.Domain.Models
{
    public class FleetInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public decimal TotalAmount { get; set; }
    }
}
