using GStore.Data;
using GStore.Models;
using GStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GStore.Repositories
{
    public class ShirtSizeSetRepo : GenericRepository<ShirtSizeSet>, IShirtSizeSetRepo
    {
        public ShirtSizeSetRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public ApplicationDbContext dbContext { get; }

        public async Task<List<ShirtSizeSet>> GetShirtSizeSetsByProductId(int ProductId)
        {
            List<ShirtSizeSet> listSizesPerShirtFromDb = dbContext.ShirtSizeSets
                    .Where(scs => scs.ProductId == ProductId).ToList();

            return listSizesPerShirtFromDb;
        }
    }
}
