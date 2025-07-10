using GStore.Utils.Constants;

namespace GStore.Models.ViewModels
{
    public class ShirtShortWithCategoryNameVM
    {

        public bool IsActive { get; set; }
        public bool IsAvailable { get; set; }
        public string CategoryName { get; set; }

        public string ShirtActive { get; set; } = "Активна:";
        public string ShirtSign { get; set; }
        public string ShirtLinkText { get; set; }

        public ShirtShortPreviewVM ShirtShortPreviewVM { get; set; }

        public List<ShirtShortWithCategoryNameVM> MapShirtShortToList(List<spShirtShortWithCategoryName> spShirtShort)
        {
            List<ShirtShortWithCategoryNameVM> ShirtShortList = new List<ShirtShortWithCategoryNameVM>();

            ShirtShortWithCategoryNameVM ShirtShort = null;

            int tempId = 0;

            foreach (spShirtShortWithCategoryName ShirtShortItem in spShirtShort)
            {
                if (tempId != ShirtShortItem.Id)
                {
                    ShirtShort = new ShirtShortWithCategoryNameVM();

                    ShirtShort.ShirtShortPreviewVM =
                        ShirtShortPreviewVM.MapToShirtShortPreview(ShirtShortItem);

                    ShirtShort.ShirtShortPreviewVM.ColorCodes = new List<string>();

                    ShirtShort.ShirtShortPreviewVM.ColorCodes
                        .Add(ShirtShortItem.ColorCode);

                    ShirtShort.CategoryName= ShirtShortItem.CategoryName;

                    ShirtShort.IsActive = ShirtShortItem.IsActive;

                    if (ShirtShort.IsActive)
                    {
                        ShirtShort.ShirtSign = ShirtActivityStatuses.ShirtActiveSign;
                        ShirtShort.ShirtLinkText = ShirtActivityStatuses.ShirtActiveText;
                    }
                    else
                    {
                        ShirtShort.ShirtSign = ShirtActivityStatuses.ShirtInactiveSign;
                        ShirtShort.ShirtLinkText = ShirtActivityStatuses.ShirtInactiveText;
                    }

                    ShirtShort.IsAvailable = ShirtShortItem.IsAvailable;

                    ShirtShortList.Add(ShirtShort);

                    tempId = ShirtShortItem.Id;
                }
                else
                {
                    ShirtShort.ShirtShortPreviewVM.ColorCodes
                        .Add(ShirtShortItem.ColorCode);
                }
            }

            return ShirtShortList;
        }
        public ShirtShortWithCategoryNameVM MapShirtShortById(List<spShirtShortWithCategoryNameById> spShirtShortById)
        {

            ShirtShortWithCategoryNameVM ShirtShortById = null;
            int tempIndex = 0;

            foreach (spShirtShortWithCategoryNameById ShirtShortByIdItem in spShirtShortById)
            {
                if (tempIndex == 0)
                {
                    ShirtShortById = new ShirtShortWithCategoryNameVM();

                    ShirtShortById.ShirtShortPreviewVM =
                        ShirtShortPreviewVM.MapToShirtShortPreview(ShirtShortByIdItem);

                    ShirtShortById.ShirtShortPreviewVM.ColorCodes = new List<string>();

                    ShirtShortById.ShirtShortPreviewVM.ColorCodes
                        .Add(ShirtShortByIdItem.ColorCode);

                    ShirtShortById.CategoryName = ShirtShortByIdItem.CategoryName;

                    tempIndex = tempIndex + 1;

                }
                else
                {
                    ShirtShortById.ShirtShortPreviewVM.ColorCodes
                         .Add(ShirtShortByIdItem.ColorCode);
                }
            }

            return ShirtShortById;
        }
    }
}
