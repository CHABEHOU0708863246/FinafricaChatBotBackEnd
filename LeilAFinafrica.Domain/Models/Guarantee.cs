using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeilAFinafrica.Domain.Models
{
    public class Guarantee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }
    }
}
