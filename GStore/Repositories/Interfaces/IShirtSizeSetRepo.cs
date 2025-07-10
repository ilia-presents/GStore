using GStore.Models;

namespace GStore.Repositories.Interfaces
{
    public interface IShirtSizeSetRepo : IGenericRepository<ShirtSizeSet>
    {
        Task<List<ShirtSizeSet>> GetShirtSizeSetsByProductId(int ProductId);
    }
}
