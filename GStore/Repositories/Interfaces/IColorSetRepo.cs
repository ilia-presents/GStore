using GStore.Models;
using GStore.Models.ViewModels;
using GStore.Repositories.Interfaces;
using System.Linq.Expressions;

namespace GStore.Repositories.Interfaces
{
    public interface IColorSetRepo : IGenericRepository<ColorSet>
    {
        Task<IEnumerable<ColorSetVM>> GetAllColorsVmNoTracking();
        Task<bool> UpdateColorAndShirtAvailabilityColors(ColorSet colorSet);
        Task<bool> SetColorAndAllColorRelatedTables(ColorSetVM colorSetVM);
    }
}
