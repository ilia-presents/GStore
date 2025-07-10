using GStore.Data;
using GStore.Models;
using GStore.Models.OtherModels;
using GStore.Models.ViewModels;
using GStore.Repositories.Interfaces;
using GStore.Utils.Constants;
using GStore.Utils.Enums;
using GStore.Utils.Helpers;
using GStore.Utils.ImageDataHelper.Interface;
using GStore.Utils.ImagesValues;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging.Signing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace GStore.Utils.ImageDataHelper
{
    public class ImageManager : IImageManager
    {
        public IShirtRepo shirtRepo { get; }

        //public ImageManager()
        //{
        //}
        public ImageManager(IShirtRepo shirtRepo)
        {
            this.shirtRepo = shirtRepo;
        }

        public void CheckSetColorImagesForPreview(List<ImagePerColorUploadVM> listImagesPerColorUploadVM)
        {

            foreach (ImagePerColorUploadVM ImageColorSet in listImagesPerColorUploadVM)
            {
                string imageNameFront = ImageColorSet.ImageLinkOne;

                string imageNameBack = ImageColorSet.ImageLinkTwo;

                if (IsImageNameNullOrEmpty(imageNameFront))

                    ImageColorSet.ImageLinkOne = ImageValues.NoImage;

                if (IsImageNameNullOrEmpty(imageNameBack))

                    ImageColorSet.ImageLinkTwo = ImageValues.NoImageBack;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="imageAssist"></param>
        /// <param name="imageDbName"></param>
        /// <param name="hostEnvironment"></param>
        /// <returns></returns>

        public bool CheckForTableAndUpdateDb(int productId, int imageType, int? colorId
            , string imageDbName, IShirtColorSetRepo shirtColorSetRepo)
        {

            bool boolResult = false;

            if ((imageType == (int)EnumImagesUpload.MainImageFront) 
                || (imageType == (int)EnumImagesUpload.MainImageBack))
            {

                Shirt shirt = shirtRepo.GetEntityById(productId);

                if (shirt == null)
                {
                    return false;
                }

                if (imageType == (int)EnumImagesUpload.MainImageFront)
                {
                    shirt.ImageLinkOne = imageDbName;
                }
                else if (imageType == (int)EnumImagesUpload.MainImageBack)
                {
                    shirt.ImageLinkTwo = imageDbName;
                }

                boolResult = shirtRepo.UpdateEntity(shirt);
            }
            else if ((imageType == (int)EnumImagesUpload.ColorImageFront)
                || (imageType == (int)EnumImagesUpload.ColorImageBack))
            {
                ShirtColorSet shirtColorSet = shirtColorSetRepo.GetEntityById(productId, colorId.Value);

                if (shirtColorSet == null)
                {
                    return false;
                }

                if (imageType == (int)EnumImagesUpload.ColorImageFront)
                {
                    shirtColorSet.ImageLinkOne = imageDbName;
                }
                else if (imageType == (int)EnumImagesUpload.ColorImageBack)
                {
                    shirtColorSet.ImageLinkTwo = imageDbName;
                }

                boolResult = shirtColorSetRepo.UpdateEntity(shirtColorSet);
            }

            return boolResult;
        }

        public ImageAssistMain SetAllForFolderAndDbSave(int productId, int imageType, int? colorId, string imageExtension)
        {
            string imageDbName = "";
            bool isNewImage = false;

            ImageAssistMain imageAssistMain = new ImageAssistMain();

            List<ImageAssist> imageAssistList = null;

            if (imageType == (int)EnumImagesUpload.MainImageFront)
            {
                imageDbName = shirtRepo
                    .GetShirtPropertyById(shirt => shirt.ImageLinkOne, productId);

                if ((IsImageNameNullOrEmpty(imageDbName)) || (imageDbName == ImageValues.NoImage))
                {
                    imageAssistMain.ImageDbName = GetDbNameForNewImage(productId, "Main", imageExtension);

                    imageAssistMain.IsNewImage = true;
                }
                else
                {
                    imageAssistMain.ImageDbName = imageDbName;
                }

                imageAssistList =
                    SetImageAssistForMiddImages(imageAssistMain.ImageDbName);
            }
            else if (imageType == (int)EnumImagesUpload.MainImageBack)
            {
                imageDbName = shirtRepo
                    .GetShirtPropertyById(shirt => shirt.ImageLinkTwo, productId);

                if ((IsImageNameNullOrEmpty(imageDbName)) || (imageDbName == ImageValues.NoImageBack))
                {
                    imageAssistMain.ImageDbName = GetDbNameForNewImage(productId, "Main_Back", imageExtension);

                    imageAssistMain.IsNewImage = true;
                }
                else
                {
                    imageAssistMain.ImageDbName = imageDbName;
                }

                imageAssistList =
                    SetImageAssistForMiddImages(imageAssistMain.ImageDbName);
            }
            else if (imageType == (int)EnumImagesUpload.ColorImageFront)
            {
                imageDbName = shirtRepo.GetColorImagePathById(shirtColor => shirtColor.ImageLinkOne
                , productId, colorId.Value);

                if (IsImageNameNullOrEmpty(imageDbName))
                {
                    imageAssistMain.ImageDbName = GetDbNameForNewImage(productId, "Color_" + colorId.Value.ToString(), imageExtension);

                    imageAssistMain.IsNewImage = true;
                }
                else
                {
                    imageAssistMain.ImageDbName = imageDbName;
                }

                imageAssistList = new List<ImageAssist>();
            }
            else if (imageType == (int)EnumImagesUpload.ColorImageBack)
            {
                imageDbName = shirtRepo.GetColorImagePathById(shirtColor => shirtColor.ImageLinkTwo
                , productId, colorId.Value);

                if (IsImageNameNullOrEmpty(imageDbName))
                {
                    imageAssistMain.ImageDbName = GetDbNameForNewImage(productId, "Color_" + colorId.Value.ToString() + "_Back", imageExtension);

                    imageAssistMain.IsNewImage = true;
                }
                else
                {
                    imageAssistMain.ImageDbName = imageDbName;
                }

                imageAssistList = new List<ImageAssist>();
            }

            imageAssistList =
                SetImageAssistForBigAndSmallImages(imageAssistMain.ImageDbName, imageAssistList);

            imageAssistMain.ImageAssistList = imageAssistList;

            return imageAssistMain;
        }

        private List<ImageAssist> SetImageAssistForMiddImages(string ImageDbName)
        {
            List<ImageAssist> imageAssistList = new List<ImageAssist>();

            ImageAssist imageAssist = new ImageAssist();

            imageAssist.ImageMainPath = ImageValues.ProductImagesMidd;
            imageAssist.ImgWidth = ImageValues.ImgWidthMidd;
            imageAssist.ImgHeight = ImageValues.ImgHeightMidd;

            imageAssistList.Add(imageAssist);

            return imageAssistList;
        }


        private List<ImageAssist> SetImageAssistForBigAndSmallImages(string ImageDbName
            , List<ImageAssist> imageAssistList)
        {

            ImageAssist imageAssist = new ImageAssist();

            imageAssist.ImageMainPath = ImageValues.ProductImagesBig;
            imageAssist.ImgWidth = ImageValues.ImgWidthBig;
            imageAssist.ImgHeight = ImageValues.ImgHeightBig;

            imageAssistList.Add(imageAssist);

            imageAssist = null;
            imageAssist = new ImageAssist();

            imageAssist.ImageMainPath = ImageValues.ProductImagesSmall;
            imageAssist.ImgWidth = ImageValues.ImgWidthSmall;
            imageAssist.ImgHeight = ImageValues.ImgHeightSmall;

            imageAssistList.Add(imageAssist);

            return imageAssistList;
        }

        private string GetDbNameForNewImage(int ProductId, string NameAdditions, string imageExtension)
        {
            string shirtNameBg = "", shirtNameLatin = "";

            shirtNameBg = shirtRepo
                .GetShirtPropertyById(shirt => shirt.Name, ProductId);

            shirtNameLatin = LetterConvertor.ConvertBgToLatin(shirtNameBg);

            shirtNameLatin = String.Concat(shirtNameLatin
                , "_", NameAdditions, "_", ProductId.ToString(), imageExtension);

            return shirtNameLatin;
        }

        public UploadImageVM GetSetImagePath(int productId
            , int imageType, int? colorId)
        {
            //string imageName = await shirtRepo.GetMainImagePathById(ProductId);

            UploadImageVM UploadImageObj = new UploadImageVM();

            UploadImageObj.ProductId = productId;
            UploadImageObj.ImageType = imageType;
            UploadImageObj.ColorId = colorId;

            string imageDbName = "";

            if (colorId == null) colorId = 0;

            imageDbName = GetImageName(imageType, productId, colorId.Value);

            string imagePath = Path.Combine(ImageValues.ProductImagesMidd, imageDbName);

            UploadImageObj.ImagePath = imagePath;

            return UploadImageObj;
        }

        private string GetImageName(int imageType, int productId, int? colorId)
        {
            string imageName = "";

            switch (imageType)
            {
                case (int)EnumImagesUpload.MainImageFront:

                    imageName = shirtRepo
                        .GetMainImagePathById(shirt => shirt.ImageLinkOne, productId);
                    break;

                case (int)EnumImagesUpload.MainImageBack:

                    imageName = shirtRepo
                        .GetMainImagePathById(shirt => shirt.ImageLinkTwo, productId);
                    break;

                case (int)EnumImagesUpload.ColorImageFront:

                    if (colorId == null) break;

                    imageName = shirtRepo
                        .GetColorImagePathById(shirtColor => shirtColor.ImageLinkOne
                        , productId, colorId.Value);

                    if (IsImageNameNullOrEmpty(imageName)) imageName = ImageValues.NoImage;

                    break;

                case (int)EnumImagesUpload.ColorImageBack:

                    if (colorId == null) break;

                    imageName = shirtRepo.GetColorImagePathById(shirtColor => shirtColor.ImageLinkTwo
                    , productId, colorId.Value);

                    if (IsImageNameNullOrEmpty(imageName)) imageName = ImageValues.NoImageBack;

                    break;

                default:
                    imageName = "";
                    break;
            }

            return imageName;
        }

        private bool IsImageNameNullOrEmpty(string imageName)
        {
            bool IsImageNameOK = ((string.IsNullOrEmpty(imageName))
                        || (imageName.Length < 4));

            return IsImageNameOK;
        }


        public bool DeleteOldImage(ImageAssist imageAssist
                , string imageDbName
                , IWebHostEnvironment hostEnvironment)
        {

            string wwwRootPath = hostEnvironment.WebRootPath;
            string path = Path.Combine(wwwRootPath, imageAssist.ImageMainPath, imageDbName);

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public bool ImageResizeAndSave(Bitmap theBaseBmp
                , ImageAssist imageAssist
                , string imageDbName
                , IWebHostEnvironment hostEnvironment)
        {

            Bitmap nextBtm;

            nextBtm = new Bitmap(theBaseBmp, imageAssist.ImgWidth, imageAssist.ImgHeight);

            using (var g = Graphics.FromImage(nextBtm))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                g.DrawImage(nextBtm, 0, 0, nextBtm.Width, nextBtm.Height);
            }

            ImageAssist ImageAssistant = new ImageAssist();

            string wwwRootPath = hostEnvironment.WebRootPath;

            string path = Path.Combine(wwwRootPath, imageAssist.ImageMainPath, imageDbName);

            try
            {
                nextBtm.Save(path, ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public InitialImgAssist GetInitialBmpValidate(IFormFile uploadedFile)
        {
            Bitmap initialBmp = null;
            string imageExtension = "";

            using (System.Drawing.Image uploadedImage = System.Drawing.Image.FromStream(uploadedFile.OpenReadStream(), true, true))
            {
                initialBmp = new Bitmap(uploadedImage);

                imageExtension = Path.GetExtension(uploadedFile.FileName);
            }

            InitialImgAssist initialImgAssist = new InitialImgAssist();

            initialImgAssist.IsImageOk = true;

            if (initialBmp.Height < ImageValues.ImgHeightBiger)
            {
                initialImgAssist.IsImageOk = false;

                initialImgAssist.ErrorMessage = $"Височината на снимката беше по-малка от {ImageValues.ImgHeightBiger.ToString()} пиксела";
            }

            decimal imgRatio = ((decimal)initialBmp.Width / (decimal)initialBmp.Height);

            if ((imgRatio < ImageValues.SizeRatioLower) || (ImageValues.SizeRatioUpper < imgRatio))
            {
                initialImgAssist.IsImageOk = false;

                initialImgAssist.ErrorMessage = initialImgAssist.ErrorMessage + " Снимката не беше вертикална с пропорции 3 към 4";
            }

            if (initialImgAssist.IsImageOk == false) return initialImgAssist;

            initialImgAssist.ImageExtension = imageExtension.ToLower();

            initialImgAssist.InitialBmp = new Bitmap(initialBmp, ImageValues.ImgWidthBiger, ImageValues.ImgHeightBiger); ;

            return initialImgAssist;
        }


    }
}

