using GStore.Models;

namespace GStore.Repositories.Interfaces
{
    public interface IShirtColorSetRepo : IGenericRepository<ShirtColorSet>
    {
        ShirtColorSet GetEntityById(int ProductId, int ColorSetId);

        Task<List<ShirtColorSet>> GetShirtColorSetsByProductId(int ProductId);
    }


}
