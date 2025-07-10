namespace GStore.Models.ViewModels
{
    public class ShirtShortPreviewVM : spShirtPreviewModel
    {
        public List<string> ColorCodes { get; set; }

        public static ShirtShortPreviewVM MapToShirtShortPreview(spShirtShortWithCategoryNameById spShirtShort)
        {
            ShirtShortPreviewVM ShirtShortPreview = new ShirtShortPreviewVM();

            ShirtShortPreview.Id = spShirtShort.Id;
            ShirtShortPreview.ImageLinkOne = spShirtShort.ImageLinkOne;
            ShirtShortPreview.ImageLinkTwo = spShirtShort.ImageLinkTwo;
            ShirtShortPreview.Name = spShirtShort.Name;
            ShirtShortPreview.Price = spShirtShort.Price;
            ShirtShortPreview.IsPromo = spShirtShort.IsPromo;
            ShirtShortPreview.Discount = spShirtShort.Discount;

            return ShirtShortPreview;
        }
    }
}
