using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace LeilAFinafrica.Domain.Models
{
    public class Vehicle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Category { get; set; }

        public string Usage { get; set; }

        public string Genre { get; set; }

        public int FiscalPower { get; set; }

        public string EnergyType { get; set; }

        public int SeatNumber { get; set; }

        public decimal NewValue { get; set; }

        public decimal MarketValue { get; set; }

        public bool HasTrailer { get; set; }

        [JsonPropertyName("firstRegistrationDate")]
        public DateTime FirstRegistrationDate { get; set; }

        [BsonIgnoreIfNull] // Ne pas inclure dans MongoDB si null
        public decimal? PayloadCapacity { get; set; }

        public int ContractDuration { get; set; }


    }
}
