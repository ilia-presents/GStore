using GStore.Data;
using GStore.Models;
using GStore.Repositories.Interfaces;

namespace GStore.Repositories
{
    public class ShirtAvailabalityRepo : GenericRepository<ShirtAvailability>, IShirtAvailabalityRepo
    {

        public ApplicationDbContext dbContext { get; }
        public ShirtAvailabalityRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            dbContext = dbContext;
        }

        public async Task<List<ShirtAvailability>> GetShirtAvailabilitysByProductId(int ProductId)
        {
            List<ShirtAvailability> listAvailabilitysPerShirtFromDb = dbContext.ShirtAvailabilitys
                    .Where(scs => scs.ProductId == ProductId).ToList();

            return listAvailabilitysPerShirtFromDb;
        }

    }
}
