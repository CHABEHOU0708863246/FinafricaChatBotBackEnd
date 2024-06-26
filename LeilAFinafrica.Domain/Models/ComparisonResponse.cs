using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeilAFinafrica.Domain.Models
{
    public class ComparisonResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public List<InsuranceOffer> Offers { get; set; } = new List<InsuranceOffer>();

        public DateTime ComparisonDate { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
