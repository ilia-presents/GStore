using GStore.Data;
using GStore.Models;
using GStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GStore.Repositories
{
    public class ShirtColorSetRepo : GenericRepository<ShirtColorSet>, IShirtColorSetRepo
    {
        private readonly DbSet<ShirtColorSet> _entities;
        private ApplicationDbContext dbContext;

        public ShirtColorSetRepo(ApplicationDbContext dbContext)
                : base(dbContext)
        {
            this.dbContext = dbContext;
            _entities = dbContext.Set<ShirtColorSet>();
        }

        public ShirtColorSet GetEntityById(int ProductId, int ColorSetId)
        {
            var entity = _entities.Find(ProductId, ColorSetId);

            return entity;
        }

        public async Task<List<ShirtColorSet>> GetShirtColorSetsByProductId(int ProductId)
        {
            List<ShirtColorSet> listSizesPerShirtFromDb = dbContext.ShirtColorSets
                    .Where(scs => scs.ProductId == ProductId).ToList();

            return listSizesPerShirtFromDb;
        }
    }
}
