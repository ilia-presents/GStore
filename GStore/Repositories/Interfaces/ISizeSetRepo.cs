using GStore.Models;

namespace GStore.Repositories.Interfaces
{
    public interface ISizeSetRepo : IGenericRepository<SizeSet>
    {
        Task<bool> UpdateSizeAndShirtAvailabilitySizes(SizeSet sizeSet);
        Task<bool> SetSizeAndAllSizeRelatedTables(SizeSet sizeSet);
    }
}
