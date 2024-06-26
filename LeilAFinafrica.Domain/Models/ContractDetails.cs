using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeilAFinafrica.Domain.Models
{
    public class ContractDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ContractNumber { get; set; }

        public string InsuranceType { get; set; }

        public decimal AnnualPremium { get; set; }

        public DateTime CoverageStartDate { get; set; }

        public DateTime CoverageEndDate { get; set; }

        public List<string> IncludedCoverages { get; set; } = new List<string>();

        public string UserId { get; set; }

        public string VehicleId { get; set; }
    }
}
