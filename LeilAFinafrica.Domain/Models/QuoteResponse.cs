using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeilAFinafrica.Domain.Models
{
    public class QuoteResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime QuoteDate { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
