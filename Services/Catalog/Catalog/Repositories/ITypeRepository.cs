using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface ITypeRepository
    {
        Task<IEnumerable<ProductType>> GetAllTypes();
        Task<ProductType> GetByIdAsync(string id);
    }
}
