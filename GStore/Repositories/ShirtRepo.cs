using GStore.Data;
using GStore.Models;
using GStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using GStore.Models.ViewModels;
using GStore.Utils.Constants;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Drawing;
using GStore.Utils.ImageDataHelper;
using GStore.Utils.ImageDataHelper.Interface;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using GStore.Models.DTOs;

namespace GStore.Repositories
{
    public class ShirtRepo : GenericRepository<Shirt>, IShirtRepo
    {
        public ApplicationDbContext dbContext { get; }
        public ShirtRepo(ApplicationDbContext dbContext)
                : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CheckSetAvailabilityInDb(int ProductId
            , List<AvailabilityPerShirtVM> shirtAvailabilityIncomming  //List<ColorsPerShirtVM> listColorsPerShirtIcomming
            , IShirtAvailabalityRepo shirtAvailabalityRepo)
        {

            List<ShirtAvailability> listAvailabilitysPerShirtFromDb =
                await shirtAvailabalityRepo.GetShirtAvailabilitysByProductId(ProductId);

            foreach (AvailabilityPerShirtVM shirtAvailabilityItem in shirtAvailabilityIncomming)
            {
                ShirtAvailability shirtAvailabilityFromDb = listAvailabilitysPerShirtFromDb
                    .Where(sav => sav.ColorSetId == shirtAvailabilityItem.ColorId
                    && sav.SizeSetId == shirtAvailabilityItem.SizeId).FirstOrDefault();

                if ((shirtAvailabilityFromDb != null) && (shirtAvailabilityFromDb.IsAvalable != shirtAvailabilityItem.IsSelected))
                {
                    shirtAvailabilityFromDb.IsAvalable = shirtAvailabilityItem.IsSelected;
                }
            }

            bool boolResult = await shirtAvailabalityRepo.UpdateAsync();

            return boolResult;
        }

        public async Task<ShirtAvailabilityComboVM> GetShirtShortPrev_AvailabilityPerShirt(int ProductId)
        {
            ShirtAvailabilityComboVM shirtAvailabilityCombo = new ShirtAvailabilityComboVM();

            shirtAvailabilityCombo.ProductId = ProductId;

            shirtAvailabilityCombo.ShirtShortById = await GetShirtForPreviewById(ProductId);

            shirtAvailabilityCombo.ListShirtAvailability = await GetAvailabilityPerShirt(ProductId);

            return shirtAvailabilityCombo;
        }

        private async Task<List<AvailabilityPerShirtVM>> GetAvailabilityPerShirt(int ProductId)
        {

            List<AvailabilityPerShirtVM> listAvailabilityPerShirtVM = await
                dbContext.ShirtAvailabilitys.Join(dbContext.SizeSets,
                    shirtAvailability => shirtAvailability.SizeSetId,
                    sizeSets => sizeSets.Id,
                    (shirtAvailability, sizeSets) => new { shirtAvailability, sizeSets }
            ).Join(dbContext.ColorSets,
                    shirtAvailability => shirtAvailability.shirtAvailability.ColorSetId,
                    colorSets => colorSets.Id,
                    (shirtAvailability, colorSets) => new { shirtAvailability, colorSets })
                .Where(tp => tp.shirtAvailability.shirtAvailability.ProductId == ProductId 
                && tp.shirtAvailability.sizeSets.IsActive == true
                && tp.colorSets.IsActive == true)
                .Select(anon => new AvailabilityPerShirtVM
                {
                    ColorId = anon.colorSets.Id,
                    ColorName = anon.colorSets.Name,
                    ColorCode = anon.colorSets.ColorCode,
                    SizeId = anon.shirtAvailability.sizeSets.Id,
                    SizeName = anon.shirtAvailability.sizeSets.Name,
                    IsSelected = anon.shirtAvailability.shirtAvailability.IsAvalable
                }).OrderBy(la => la.ColorId).AsNoTracking().ToListAsync();

            //listAvailabilityPerShirtVM.OrderBy(la => la.ColorId);//.ThenBy(la => la.ColorName);

            return listAvailabilityPerShirtVM;
        }

        public async Task<bool> CheckSetSizesInDb(int ProductId
            , List<SelectListItem> listSizesPerShirtIcomming
            , [FromServices] IShirtSizeSetRepo shirtSizeSetRepo)
        {

            List<ShirtSizeSet> listSizesPerShirtFromDb = 
                await shirtSizeSetRepo.GetShirtSizeSetsByProductId(ProductId);;

            bool boolResult = false;

            foreach (SelectListItem sizePerShirtItem in listSizesPerShirtIcomming)
            {
                int tempInt = 0;

                bool tempBool = Int32.TryParse(sizePerShirtItem.Value, out tempInt);

                if (tempBool == false)
                {
                    return false;
                }

                ShirtSizeSet sizePerShirtFromDb = listSizesPerShirtFromDb
                    .Where(ocs => ocs.SizeSetId == tempInt).FirstOrDefault();

                if ((sizePerShirtFromDb != null) && (sizePerShirtFromDb.IsAvalable != sizePerShirtItem.Selected))
                {
                    sizePerShirtFromDb.IsAvalable = sizePerShirtItem.Selected;
                }
            }

            boolResult = await shirtSizeSetRepo.UpdateAsync();

            return boolResult;
        }


        public async Task<bool> CheckSetColorsInDb(int ProductId
            , List<ColorsPerShirtVM> listColorsPerShirtIcomming
            , [FromServices] IShirtColorSetRepo shirtColorSetRepo)
        {

            List<ShirtColorSet> listColorsPerShirtFromDb =
                await shirtColorSetRepo.GetShirtColorSetsByProductId(ProductId);

            foreach (ColorsPerShirtVM colorPerShirtItem in listColorsPerShirtIcomming)
            {
                ShirtColorSet colorPerShirtFromDb = listColorsPerShirtFromDb
                    .Where(ocs => ocs.ColorSetId == colorPerShirtItem.ColorId).FirstOrDefault();

                if ((colorPerShirtFromDb != null) && (colorPerShirtFromDb.IsAvalable != colorPerShirtItem.IsSelected))
                {
                    colorPerShirtFromDb.IsAvalable = colorPerShirtItem.IsSelected;
                }
            }

            bool boolResult = await shirtColorSetRepo.UpdateAsync();

            return boolResult;  
        }

        public async Task<SizesPerShirtComboVM> GetShirtShortPrev_SizesPerShirt(int ProductId)
        {
            SizesPerShirtComboVM sizesPerShirtComboVM = new SizesPerShirtComboVM();

            sizesPerShirtComboVM.ProductId = ProductId;

            sizesPerShirtComboVM.ShirtShortById = await GetShirtForPreviewById(ProductId);

            sizesPerShirtComboVM.ListSizesPerShirt = await GetSizesPerShirt(ProductId);

            return sizesPerShirtComboVM;
        }

        private async Task<List<SelectListItem>> GetSizesPerShirt(int ProductId)
        {

            List<SelectListItem> listSizesPerShirtVM = await
            dbContext.ShirtSizeSets.Join(
            dbContext.SizeSets,
            shirtsSizeSets => shirtsSizeSets.SizeSetId,
            sizeSets => sizeSets.Id,
            (shirtsSizeSets, sizeSets) => new { shirtsSizeSets, sizeSets })
                .Where(x => x.shirtsSizeSets.ProductId == ProductId && x.sizeSets.IsActive == true)
                .Select(anon => new SelectListItem
                {
                    Text = anon.sizeSets.Name,
                    Value = anon.sizeSets.Id.ToString(),
                    Selected = anon.shirtsSizeSets.IsAvalable
                }).AsNoTracking().ToListAsync();

            return listSizesPerShirtVM;
        }

        public async Task<ColorsPerShirtComboVM> GetShirtShortPrev_ColorsPerShirt(int ProductId)
        {
            ColorsPerShirtComboVM colorsPerShirtComboVM = new ColorsPerShirtComboVM();

            colorsPerShirtComboVM.ProductId = ProductId;

            colorsPerShirtComboVM.ShirtShortById = await GetShirtForPreviewById(ProductId);

            colorsPerShirtComboVM.ListColorsPerShirt = await GetColorsPerShirt(ProductId);

            return colorsPerShirtComboVM;
        }


        public async Task<List<ImagePerColorUploadVM>> GetListShirtsColorsAndImagesForUpload(int ProductId)
        {

            List<ImagePerColorUploadVM> listImagePerColorUploadVM =
            await dbContext.ShirtColorSets.Join(
            dbContext.ColorSets,
            shirtColorSets => shirtColorSets.ColorSetId,
            colorSets => colorSets.Id,
            (shirtColorSets, colorSets) => new { shirtColorSets, colorSets })
                .Where(x => x.shirtColorSets.ProductId == ProductId
                && x.shirtColorSets.IsAvalable == true && x.colorSets.IsActive == true)
                .Select(anon => new ImagePerColorUploadVM
                {
                    ColorName = anon.colorSets.Name,
                    ColorCode = anon.colorSets.ColorCode,
                    ColorId = anon.colorSets.Id,
                    ImageLinkOne = anon.shirtColorSets.ImageLinkOne,
                    ImageLinkTwo = anon.shirtColorSets.ImageLinkTwo
            }).AsNoTracking().ToListAsync();

            return listImagePerColorUploadVM;
        }

        public string GetShirtPropertyById(Expression<Func<Shirt, string>> selector, int ProductId)
        {
            string imageName = dbContext
                .Shirts.Where(shirt => shirt.Id == ProductId).Select(selector)
                .AsNoTracking().FirstOrDefault();

            return imageName;
        }

        public string GetColorImagePathById(Expression<Func<ShirtColorSet, string>> selector
            , int productId, int colorId)
        {

            string imageName = dbContext.ShirtColorSets
                .Where(shirtColor => shirtColor.ProductId == productId && shirtColor.ColorSetId == colorId)
                .Select(selector).AsNoTracking().FirstOrDefault();

            return imageName;
        }

        public string GetMainImagePathById(Expression<Func<Shirt, string>> selector, int ProductId)
        {

            string imageName = dbContext.Shirts
                .Where(shirt => shirt.Id == ProductId).Select(selector)
                .AsNoTracking().FirstOrDefault();

            return imageName;
        }

        public async Task<ShirtShortWithCategoryNameVM> GetShirtForPreviewById(int id)
        {
            SqlParameter shirtIdParam = new SqlParameter("@ShirtId", SqlDbType.Int);
            shirtIdParam.Value = id;

            List<spShirtShortWithCategoryNameById> spShirtSqlShortById =
                await dbContext.spShirtPrevWithCatNameById
                .FromSqlInterpolated($"spShirtPrevWithCatNameById {shirtIdParam}")
                .AsNoTracking().ToListAsync();

            ShirtShortWithCategoryNameVM shirtShortById =
                new ShirtShortWithCategoryNameVM().MapShirtShortById(spShirtSqlShortById);

            return shirtShortById;
        }

        public async Task<List<ShirtShortWithCategoryNameVM>> GetAllShirtShortVersion(int rowsToSkip = 0, int rowsToTake = 22)
        {
            SqlParameter rowsToSkipParam = new SqlParameter("@RowsToSkip", SqlDbType.Int);
            rowsToSkipParam.Value = rowsToSkip;

            SqlParameter rowsToTakeParam = new SqlParameter("@RowsToTake", SqlDbType.Int);
            rowsToTakeParam.Value = rowsToTake;

            List<spShirtShortWithCategoryName> spShirtSqlShort =
                await dbContext.spShirtPrevWithCatName
                .FromSqlInterpolated($"spShirtPrevWithCatName {rowsToSkipParam}, {rowsToTakeParam}")
                .AsNoTracking().ToListAsync();

            ShirtShortWithCategoryNameVM ShirtShortWithCategoryNameVM = new ShirtShortWithCategoryNameVM();

            ShirtActivityDTO shirtActivityVM = new ShirtActivityDTO();


            List<ShirtShortWithCategoryNameVM> shirtShort =
                ShirtShortWithCategoryNameVM.MapShirtShortToList(spShirtSqlShort);


            return shirtShort;
        }

        public async Task<bool> SetShirtAndAllShirtRelatedTables(Shirt shirt)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            {
                try
                {
                    int shirtIdFromCreate = await AddAsyncReturnId(shirt);

                    if (shirtIdFromCreate == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    bool resultFromCreate = false, ShouldSetAvailabilty = true;

                    // If there are colorss in the table AddAllColorsForShirt
                    if (dbContext.ColorSets.Any() == true)
                    { 
                        resultFromCreate = await AddAllColorsForShirt(shirtIdFromCreate); 
                    }

                    else
                    { 
                        ShouldSetAvailabilty = false; 
                    }


                    if (resultFromCreate == false)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    // If there are Sizes in the table AddAllSizesForShirt
                    if (dbContext.SizeSets.Any() == true)
                    { 
                        resultFromCreate = await AddAllSizesForShirt(shirtIdFromCreate); 
                    }

                    else
                    { 
                        ShouldSetAvailabilty = false; 
                    }

                    if (resultFromCreate == false)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    if (ShouldSetAvailabilty == true)
                    { 
                        resultFromCreate = await AddAvailabilityForShirt(shirtIdFromCreate); 
                    }

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

        private async Task<bool> AddAvailabilityForShirt(int ShirtId)
        {
            ShirtAvailability shirtAvailabilitys;

            List<GenericIntBoolVM> allColorsSetIds = await dbContext.ColorSets.AsNoTracking()               
                .Select(c => new GenericIntBoolVM() { Id= c.Id, IsActive = c.IsActive })
                .ToListAsync();

            List<GenericIntBoolVM> allSizesSetIds = await dbContext.SizeSets.AsNoTracking()
                .Select(s => new GenericIntBoolVM() { Id = s.Id, IsActive = s.IsActive })
                .ToListAsync();

            foreach (GenericIntBoolVM ColorSet in allColorsSetIds)
            {
                foreach (GenericIntBoolVM SizeSet in allSizesSetIds)
                {
                    shirtAvailabilitys = new ShirtAvailability();

                    shirtAvailabilitys.ProductId = ShirtId;

                    shirtAvailabilitys.ColorSetId = ColorSet.Id;

                    shirtAvailabilitys.SizeSetId = SizeSet.Id;

                    shirtAvailabilitys.IsAvalable = true;

                    if ((ColorSet.IsActive == false ) || (SizeSet.IsActive == false))
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

        private async Task<bool> AddAllSizesForShirt(int ShirtId)
        {

            ShirtSizeSet productSizeSet;

            List<int> allSizesSetIds = await dbContext
                .SizeSets.AsNoTracking().Select(s => s.Id).ToListAsync();

            foreach (int Id in allSizesSetIds)
            {
                productSizeSet = new ShirtSizeSet();

                //productSizeSet.ImageLink = "";

                productSizeSet.ProductId = ShirtId;

                productSizeSet.SizeSetId = Id;

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

        private async Task<bool> AddAllColorsForShirt(int ShirtId)
        {
            ShirtColorSet productColorSet;

            List<int> allColorsSetIds = await dbContext
                .ColorSets.AsNoTracking().Select(c => c.Id).ToListAsync();

            foreach (int ColorsSetId in allColorsSetIds)
            {
                productColorSet = new ShirtColorSet();

                productColorSet.ImageLinkOne = "";

                productColorSet.ProductId = ShirtId;

                productColorSet.ColorSetId = ColorsSetId;

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

        private async Task<int> AddAsyncReturnId(Shirt shirt)
        {
            await dbContext.AddAsync(shirt);

            try
            {
                await dbContext.SaveChangesAsync();

                return shirt.Id;
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

            //int tempId = shirt.Id;

            //return shirt.Id;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllLevel2Categories()
        {
            var AllLevel1Categories = await dbContext.Level2Sets
                .Where(l2 => l2.Level1SetId == 7)
                .Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).AsNoTracking().ToListAsync();

            return AllLevel1Categories;
        }

        private async Task<bool> AddAsyncReturnResult(Shirt shirt)
        {
            await dbContext.AddAsync(shirt);

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
        private async Task<List<ColorsPerShirtVM>> GetColorsPerShirt(int ProductId)
        {

            List<ColorsPerShirtVM> listImagePerColorUploadVM = await
            dbContext.ShirtColorSets.Join(
            dbContext.ColorSets,
            shirtColorSets => shirtColorSets.ColorSetId,
            colorSets => colorSets.Id,
            (shirtColorSets, colorSets) => new { shirtColorSets, colorSets })
                .Where(x => x.shirtColorSets.ProductId == ProductId && x.colorSets.IsActive == true)
                .Select(anon => new ColorsPerShirtVM
                {
                    ColorName = anon.colorSets.Name,
                    ColorCode = anon.colorSets.ColorCode,
                    ColorId = anon.colorSets.Id,
                    IsSelected = anon.shirtColorSets.IsAvalable
                }).AsNoTracking().ToListAsync();

            return listImagePerColorUploadVM;
        }


    }
}
