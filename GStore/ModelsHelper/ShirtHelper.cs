using GStore.Models.ViewModels;
using GStore.Models;
using GStore.Utils.ImagesValues;
using System.Globalization;

namespace GStore.ModelsHelper
{
    public static class ShirtHelper
    {
        public static Shirt MapVmToEntityForCreate(ShirtVM shirtVM)
        {

            decimal tempDec = 0;

            string tempStr = shirtVM.Price;

            tempStr = tempStr.Replace(",", ".");


            try
            {
                tempDec = decimal.Parse(tempStr, CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
                tempDec = 0;
            }

            Shirt shirt = new Shirt()
            {
                ImageLinkOne = ImageValues.NoImage,
                ImageLinkTwo = ImageValues.NoImageBack,
                Name = shirtVM.Name,
                Description = shirtVM.Description,
                Price = tempDec,
                IsPromo = false,
                Discount = (decimal?)0.01,
                IsAvailable = true,
                IsActive = true
            };

            int tempInt = 0;

            bool resultFromParse = int.TryParse(shirtVM.SelectedCategoryValue, out tempInt);

            if (resultFromParse == true)

                shirt.CategoryId = tempInt;

            else

                shirt.CategoryId = 0;

            return shirt;
        }
    }
}
