using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeilAFinafrica.Domain.Models
{
    public class User
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<ContractDetails> Contracts { get; set; } = new List<ContractDetails>();

        public string Response { get; set; }

        public string Status { get; set; }
    }
}
