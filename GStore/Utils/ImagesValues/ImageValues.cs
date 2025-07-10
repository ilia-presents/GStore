namespace GStore.Utils.ImagesValues
{
    public static class ImageValues
    {
        public const string NoImage = "noimage.jpg";
        public const string NoImageBack = "noimageBack.jpg";

        public const string ErrorImageNotFound = "Снимката не беше открита";
        public const string ErrorImageNotAdded = "Имаше проблем и снимката не беше запазена";

        public const string ErrorOnImageSave = "Случи се непредвидена грешка при запазването на снимката. Моля свържете се с програмиста.";

        public const int ImageSizeInMBs = 8;
        public const int ImageMaxSize = ImageSizeInMBs * 1024 * 1024; // productimages/big/  ;

        public const string ProductImagesBig = @"productimages\big";
        public const string ProductImagesMidd = @"productimages\midd";
        public const string ProductImagesSmall = @"productimages\small";

        public const decimal SizeRatioLower = (decimal)0.732;
        public const decimal SizeRatioUpper = (decimal)0.768;

        public const int ImgWidthSmall = 150;
        public const int ImgHeightSmall = 200;

        public const int ImgWidthMidd = 450;
        public const int ImgHeightMidd = 600;

        public const int ImgWidthBig = 600;
        public const int ImgHeightBig = 800;

        public const int ImgWidthBiger = 768;
        public const int ImgHeightBiger = 1024;


    }
}
