using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeilAFinafrica.Domain.Models
{
    public class ContactRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime PreferredContactTime { get; set; }

        public string RequestType { get; set; } // e.g., "phone", "email", "callback"
    }
}
