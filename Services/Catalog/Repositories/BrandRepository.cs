using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IMongoCollection<ProductBrand> _brands;
        public BrandRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _brands = db.GetCollection<ProductBrand>(config["DatabaseSettings:BrandCollectionName"]);
        }
        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _brands.Find(_ => true).ToListAsync();
        }

        public async Task<ProductBrand> GetByIdAsync(string id)
        {
            return await _brands.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
