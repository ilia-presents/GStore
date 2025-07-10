using GStore.Models.OtherModels;
using GStore.Models.ViewModels;
using GStore.Repositories.Interfaces;
using System.Drawing;

namespace GStore.Utils.ImageDataHelper.Interface
{
    public interface IImageManager
    {


        void CheckSetColorImagesForPreview(List<ImagePerColorUploadVM> listImagesPerColorUploadVM);
        bool DeleteOldImage(ImageAssist imageAssist
                , string imageDbName
                , IWebHostEnvironment hostEnvironment);

        public bool ImageResizeAndSave(Bitmap theBaseBmp
        , ImageAssist imageAssist
        , string imageDbName
        , IWebHostEnvironment hostEnvironment);

        bool CheckForTableAndUpdateDb(int productId, int imageType, int? colorId, string ImageDbName
            , IShirtColorSetRepo shirtColorSetRepo);

        ImageAssistMain SetAllForFolderAndDbSave(int ProductId, int ImageType, int? colorId, string imageExtension);

        InitialImgAssist GetInitialBmpValidate(IFormFile uploadedFile);

        UploadImageVM GetSetImagePath(int productId, int imageType, int? colorId);
    }
}
