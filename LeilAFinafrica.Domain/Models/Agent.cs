using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeilAFinafrica.Domain.Models
{
    public class Agent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
    }
}
