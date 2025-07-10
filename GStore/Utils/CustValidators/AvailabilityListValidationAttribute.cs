using GStore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace GStore.Utils.CustValidators
{
    public class AvailabilityListValidationAttribute : ValidationAttribute/*, IClientModelValidator*/
    {
        protected override ValidationResult IsValid(object value,
                                        ValidationContext validationContext)
        {
            List<AvailabilityPerShirtVM> ListAvailabilityPerShirt = value as List<AvailabilityPerShirtVM>;

            bool tempResult = false;

            foreach (AvailabilityPerShirtVM item in ListAvailabilityPerShirt)
            {
                if (item.IsSelected == true)
                {
                    tempResult = true;
                    break;
                }
            }

            if (tempResult == true)

                return ValidationResult.Success;

            else
            {
                string errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }
        }

        //public void AddValidation(ClientModelValidationContext context)
        //{
        //    context.Attributes.Add("data-val", "true"); 
        //    context.Attributes.Add("data-val-checkBoxListColorsValidation", "Моля изберете поне един цвят");
        //}
    }
}

