using GStore.Utils.Constants;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GStore.Utils.CustValidators
{
    public class ImageHeightValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        public ImageHeightValidationAttribute()
        {
           
        }

        protected override ValidationResult IsValid(object value,
             ValidationContext validationContext)
        {

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            // This attribute is already addded in the other image validator .
            //context.Attributes.Add("data-val", "true"); 
            context.Attributes.Add("data-val-imageHeightValidation", GetErrorMessageOnFileExtension());
        }

        private string GetErrorMessageOnFileExtension()
        {
            return $"Снимката трябва да е с височина поне 1024 пиксела.";
        }
    }
}
