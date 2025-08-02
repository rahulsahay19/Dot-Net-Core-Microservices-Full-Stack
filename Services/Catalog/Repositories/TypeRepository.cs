using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly IMongoCollection<ProductType> _types;
        public TypeRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _types = db.GetCollection<ProductType>(config["DatabaseSettings:TypeCollectionName"]);
        }
        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _types.Find(_ => true).ToListAsync();
        }

        public async Task<ProductType> GetByIdAsync(string id)
        {
            return await _types.Find(t=>t.Id == id).FirstOrDefaultAsync();
        }
    }
}
