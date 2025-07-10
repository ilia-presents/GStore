using GStore.Models.ViewModels;
using GStore.Utils.ImagesValues;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GStore.Utils.CustValidators
{
    public class ColorsListValidationAttribute : ValidationAttribute/*, IClientModelValidator*/
    {
        protected override ValidationResult IsValid(object value,
                                        ValidationContext validationContext)
        {
            List<ColorsPerShirtVM> ListColorsPerShirt = value as List<ColorsPerShirtVM>;

            bool tempResult = false;    

            foreach (ColorsPerShirtVM item in ListColorsPerShirt)
            {
                if (item.IsSelected == true)
                {
                    tempResult=true; 
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
