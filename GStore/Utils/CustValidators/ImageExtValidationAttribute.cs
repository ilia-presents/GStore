using GStore.Utils.Constants;
using GStore.Utils.ImagesValues;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GStore.Utils.CustValidators
{
    public class ImageExtValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _fileExtensions;
        public ImageExtValidationAttribute(string[] extensions)
        {
            _fileExtensions = extensions;
        }

        protected override ValidationResult IsValid(object value,
             ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
                return new ValidationResult(ImageValues.ErrorImageNotFound);

            var extension = Path.GetExtension(file.FileName);

            if (!_fileExtensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(GetErrorMessageOnFileExtension());
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            // This attribute is already addded inside the other image validator .
            //context.Attributes.Add("data-val", "true"); 
            context.Attributes.Add("data-val-imageExtValidation", GetErrorMessageOnFileExtension());
        }

        private string GetErrorMessageOnFileExtension()
        {
            return $"Снимката трябва да е с разширение .jpg или .jpeg";
        }
    }
}
