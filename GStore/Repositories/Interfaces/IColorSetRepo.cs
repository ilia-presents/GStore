using GStore.Models;
using GStore.Models.ViewModels;
using GStore.Repositories.Interfaces;

namespace GStore.Repositories.Interfaces
{
    public interface IColorSetRepo : IGenericRepository<ColorSet>
    {

        Task<IEnumerable<ColorSetVM>> GetAllColorsVmNoTracking();
        Task<bool> UpdateColorAndShirtAvailabilityColors(ColorSet colorSet);
        Task<bool> SetColorAndAllColorRelatedTables(ColorSet colorSet);
    }
}
