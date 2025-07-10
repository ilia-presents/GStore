using GStore.Data;
using GStore.Models;
using GStore.Models.ViewModels;
using GStore.Repositories.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace GStore.Repositories
{
    public class ColorSetRepo : GenericRepository<ColorSet>, IColorSetRepo
    {
        public ApplicationDbContext dbContext { get; }
        public ColorSetRepo(ApplicationDbContext dbContext)
                : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> UpdateColorAndShirtAvailabilityColors(ColorSet colorSet)
        {

            bool resultFromCreate;

            using var transaction = dbContext.Database.BeginTransaction();
            {
                try
                {
                    resultFromCreate = await UpdateAsync(colorSet);

                    // If there are no shirts in the table there is no point to continue
                    if (dbContext.Shirts.Any() == false)
                    {
                        transaction.Commit();
                        return true;
                    }

                    resultFromCreate = await UpdateShirtAvailability(colorSet.Id, colorSet.IsActive);

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

        private async Task<bool> UpdateShirtAvailability(int ColorId, bool ColorIsActive)
        {

            List<ShirtAvailability> shirtAvailability = await dbContext.ShirtAvailabilitys
                .Where(sa => sa.ColorSetId == ColorId).ToListAsync();

            shirtAvailability.ForEach(sa => { sa.IsActive = ColorIsActive; });

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


        public async Task<bool> SetColorAndAllColorRelatedTables(ColorSet colorSet)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            {
                try
                {
                    int colorIdFromCreate = await AddAsyncReturnId(colorSet);

                    if (colorIdFromCreate == 0)
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

                    bool resultFromCreate = await AddThisColorForAllShirts(colorIdFromCreate);

                    if (resultFromCreate == false)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    resultFromCreate = await AddColorAvailabilityForShirt(colorIdFromCreate);

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
        private async Task<bool> AddThisColorForAllShirts(int colorId)
        {
            ShirtColorSet productColorSet;

            List<int> allShirtsIds = await dbContext
                .Shirts.AsNoTracking().Select(c => c.Id).ToListAsync();

            foreach (int shirtId in allShirtsIds)
            {
                productColorSet = new ShirtColorSet();

                productColorSet.ImageLinkOne = "";
                productColorSet.ImageLinkOne = "";

                productColorSet.ProductId = shirtId;

                productColorSet.ColorSetId = colorId;

                productColorSet.IsAvalable = true;

                await dbContext.AddAsync(productColorSet);
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

        private async Task<bool> AddColorAvailabilityForShirt(int ColorId)
        {
            ShirtAvailability shirtAvailabilitys;

            List<int> allShirtsIds = await dbContext
                .Shirts.AsNoTracking().Select(c => c.Id).ToListAsync();

            List<GenericIntBoolVM> allSizeSets = await dbContext.SizeSets.AsNoTracking()
                .Select(s => new GenericIntBoolVM() { Id = s.Id, IsActive = s.IsActive })
                .ToListAsync();

            foreach (int shirtsIds in allShirtsIds)
            {
                foreach (GenericIntBoolVM SizeSet in allSizeSets)
                {
                    shirtAvailabilitys = new ShirtAvailability();

                    shirtAvailabilitys.ProductId = shirtsIds;

                    shirtAvailabilitys.ColorSetId = ColorId;

                    shirtAvailabilitys.SizeSetId = SizeSet.Id;

                    shirtAvailabilitys.IsAvalable = true;

                    if (SizeSet.IsActive == false)
                    {
                        shirtAvailabilitys.IsActive = false;
                    }
                    else
                    {
                        shirtAvailabilitys.IsActive = true;
                    }

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

        private async Task<int> AddAsyncReturnId(ColorSet colorSet)
        {
            await dbContext.AddAsync(colorSet);

            try
            {
                await dbContext.SaveChangesAsync();

                return colorSet.Id;
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
    }
}
