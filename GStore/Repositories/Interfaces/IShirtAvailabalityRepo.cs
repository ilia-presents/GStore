using GStore.Models;

namespace GStore.Repositories.Interfaces
{
    public interface IShirtAvailabalityRepo : IGenericRepository<ShirtAvailability>
    {
        Task<List<ShirtAvailability>> GetShirtAvailabilitysByProductId(int ProductId);
    }
}
