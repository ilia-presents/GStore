using GStore.Data;
using GStore.Models;
using GStore.Models.ViewModels;
using GStore.Repositories.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace GStore.Repositories
{
    public class SizeSetRepo : GenericRepository<SizeSet>, ISizeSetRepo
    {
        public ApplicationDbContext dbContext { get; }
        public SizeSetRepo(ApplicationDbContext dbContext)
                : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> UpdateSizeAndShirtAvailabilitySizes(SizeSet sizeSet)
        {
            bool resultFromCreate;

            using var transaction = dbContext.Database.BeginTransaction();
            {
                try
                {
                    resultFromCreate = await UpdateAsync(sizeSet);

                    // If there are no shirts in the table there is no point to continue
                    if (dbContext.Shirts.Any() == false)
                    {
                        transaction.Commit();
                        return true;
                    }

                    resultFromCreate = await UpdateShirtAvailability(sizeSet.Id, sizeSet.IsActive);

                    if (resultFromCreate == false)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        private async Task<bool> UpdateShirtAvailability(int SizeId, bool SizeIsActive)
        {
            List<ShirtAvailability> shirtAvailability = await dbContext.ShirtAvailabilitys
                .Where(sa => sa.SizeSetId == SizeId).ToListAsync();

            shirtAvailability.ForEach(sa => { sa.IsActive = SizeIsActive; });

            try
            {
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SetSizeAndAllSizeRelatedTables(SizeSet sizeSet)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            {
                try
                {
                    int sizeIdFromCreate = await AddAsyncReturnId(sizeSet);

                    if (sizeIdFromCreate == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    // If there are no shirts in the table there is no point to continue
                    if (dbContext.Shirts.Any() == false)
                    {
                        transaction.Commit();
                        return true;
                    }

                    bool resultFromCreate = await AddThisSizeForAllShirts(sizeIdFromCreate);

                    if (resultFromCreate == false)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    resultFromCreate = await AddAvailabilityForShirt(sizeIdFromCreate);

                    if (resultFromCreate == false)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    transaction.Commit();

                    return true;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        private async Task<int> AddAsyncReturnId(SizeSet sizeSet)
        {
            await dbContext.AddAsync(sizeSet);

            try
            {
                await dbContext.SaveChangesAsync();

                return sizeSet.Id;
            }
            catch (DbUpdateException ex)
            {
                return 0;
            }
            catch (SqlException ex)
            {
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private async Task<bool> AddAvailabilityForShirt(int sizeSetId)
        {
            ShirtAvailability shirtAvailabilitys;

            List<int> allShirtsIds = await dbContext
                .Shirts.AsNoTracking().Select(c => c.Id).ToListAsync();

            List<int> allColorSetIds = await dbContext
                .ColorSets.AsNoTracking().Select(s => s.Id).ToListAsync();

            foreach (int shirtId in allShirtsIds)
            {
                foreach (int colorSetId in allColorSetIds)
                {
                    shirtAvailabilitys = new ShirtAvailability();

                    shirtAvailabilitys.ProductId = shirtId;

                    shirtAvailabilitys.ColorSetId = colorSetId;

                    shirtAvailabilitys.SizeSetId = sizeSetId;

                    shirtAvailabilitys.IsAvalable = true;

                    shirtAvailabilitys.IsActive = true;

                    await dbContext.AddAsync(shirtAvailabilitys);
                }
            }

            try
            {
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<bool> AddThisSizeForAllShirts(int sizeSetId)
        {

            ShirtSizeSet productSizeSet;

            List<int> allShirtsIds = await dbContext
                .Shirts.AsNoTracking().Select(s => s.Id).ToListAsync();

            foreach (int shirtId in allShirtsIds)
            {
                productSizeSet = new ShirtSizeSet();

                productSizeSet.ProductId = shirtId;

                productSizeSet.SizeSetId = sizeSetId;

                productSizeSet.IsAvalable = true;

                await dbContext.AddAsync(productSizeSet);
            }

            try
            {
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
