using GStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GStore.Utils.CustValidators
{
    public class GenericCheckBoxListValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            List<SelectListItem> genericList = value as List<SelectListItem>;

            bool tempResult = false;

            foreach (SelectListItem item in genericList)
            {
                if (item.Selected == true)
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
    }
}
