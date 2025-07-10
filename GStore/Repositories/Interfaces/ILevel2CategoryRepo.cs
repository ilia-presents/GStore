using GStore.Models;
using GStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace GStore.Repositories.Interfaces
{
    public interface ILevel2CategoryRepo : IGenericRepository<Level2Set>
    {




        Task<Level2Set> GetL1SetAndL2SetNoTracking(int id);
        Task<IEnumerable<SelectListItem>> GetAllLevel1Categories();

        Task<List<L1SetVMForFullDisplay>> GetAllCategoriesCombined();
    }
}
