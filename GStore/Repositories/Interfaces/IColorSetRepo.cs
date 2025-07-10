using GStore.Models;
using GStore.Repositories.Interfaces;

namespace GStore.Repositories.Interfaces
{
    public interface IColorSetRepo : IGenericRepository<ColorSet>
    {


        Task<bool> UpdateColorAndShirtAvailabilityColors(ColorSet colorSet);
        Task<bool> SetColorAndAllColorRelatedTables(ColorSet colorSet);
    }
}
