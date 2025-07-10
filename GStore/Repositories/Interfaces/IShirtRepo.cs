using GStore.Models;
using GStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace GStore.Repositories.Interfaces
{
    public interface IShirtRepo : IGenericRepository<Shirt>
    {

        Task<bool> CheckSetAvailabilityInDb(int ProductId
            , List<AvailabilityPerShirtVM> shirtAvailabilityIncomming  //List<ColorsPerShirtVM> listColorsPerShirtIcomming
            , IShirtAvailabalityRepo shirtAvailabalityRepo);

        Task<ShirtAvailabilityComboVM> GetShirtShortPrev_AvailabilityPerShirt(int ProductId);

        Task<bool> CheckSetSizesInDb(int ProductId
            , List<SelectListItem> listSizesPerShirtIcomming
            , [FromServices] IShirtSizeSetRepo shirtSizeSetRepo);

        Task<SizesPerShirtComboVM> GetShirtShortPrev_SizesPerShirt(int ProductId);

        Task<bool> CheckSetColorsInDb(int ProductId
            , List<ColorsPerShirtVM> listColorsPerShirtIncomming
            , [FromServices] IShirtColorSetRepo shirtColorSetRepo);

        Task<ColorsPerShirtComboVM> GetShirtShortPrev_ColorsPerShirt(int ProductId);

        //List<SetColorsPerShirtVM> GetColorsPerShirt(int ProductId);

        string GetColorImagePathById(Expression<Func<ShirtColorSet, string>> selector
            , int productId, int colorId);

        Task<List<ImagePerColorUploadVM>> GetListShirtsColorsAndImagesForUpload(int ProductId);
        string GetShirtPropertyById(Expression<Func<Shirt, string>> condition, int ProductId);

        string GetMainImagePathById(Expression<Func<Shirt, string>> selector, int ProductId);

        Task<ShirtShortWithCategoryNameVM> GetShirtForPreviewById(int id);
        Task<List<ShirtShortWithCategoryNameVM>> GetAllShirtShortVersion(int rowsToSkip, int rowsToTake);
        Task<bool> SetShirtAndAllShirtRelatedTables(Shirt shirt);
        Task<IEnumerable<SelectListItem>> GetAllLevel2Categories();
    }
}
