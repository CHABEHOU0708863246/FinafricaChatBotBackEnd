using LeilAFinafrica.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LeilAFinafrica.DataAccessLayer.DbContext
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Vehicle> Vehicles => _database.GetCollection<Vehicle>("Vehicles");
        public IMongoCollection<QuoteResponse> QuoteResponses => _database.GetCollection<QuoteResponse>("QuoteResponses");
        public IMongoCollection<FleetInfo> FleetInfos => _database.GetCollection<FleetInfo>("FleetInfos");
        public IMongoCollection<InsuranceOffer> InsuranceOffers => _database.GetCollection<InsuranceOffer>("InsuranceOffers");
        public IMongoCollection<ContractDetails> ContractDetails => _database.GetCollection<ContractDetails>("ContractDetails");
        public IMongoCollection<ContactRequest> ContactRequests => _database.GetCollection<ContactRequest>("ContactRequests");
    }
}
