using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<ProductBrand>> GetAllBrands();
        Task<ProductBrand> GetByIdAsync(string id);
    }
}
