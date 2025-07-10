using GStore.Data;
using GStore.Models;
using GStore.Models.ViewModels;
using GStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GStore.Repositories
{
    public class Level2CategoryRepo : GenericRepository<Level2Set>, ILevel2CategoryRepo
    {
        public ApplicationDbContext dbContext { get; }
        public Level2CategoryRepo(ApplicationDbContext dbContext)
                : base(dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Level2Set> GetL1SetAndL2SetNoTracking(int id)
        {
            Level2Set l1SetAndl2Set =
                await dbContext.Level2Sets
                .Include(x => x.Level1Set.Name)
                .Where(l2 => l2.Id == id).AsNoTracking()
                .FirstOrDefaultAsync();

            return l1SetAndl2Set;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllLevel1Categories()
        {
            var AllLevel1Categories = await dbContext.Level1Sets.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToListAsync();

            return AllLevel1Categories;
        }

        public async Task<List<L1SetVMForFullDisplay>> GetAllCategoriesCombined()
        {


            List<L1SetVMForFullDisplay> allCategoriesCombined =
                await dbContext.Level1Sets.Select(l1 =>
                    new L1SetVMForFullDisplay
                    {
                        Name = l1.Name,
                        IsActive = l1.IsActive,
                        L2SetsVMForFullDisplay = l1.Level2Sets
                        .Select(l2 => new L2SetVMForFullDisplay
                        {
                            Id = l2.Id,
                            Name = l2.Name,
                            IsActive = l2.IsActive
                        })
                    })
                .ToListAsync();

            return allCategoriesCombined;
        }
    }
}
