using GStore.Utils.Constants;
using GStore.Utils.ImagesValues;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GStore.Utils.CustValidators
{
    public class ImageSizeValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxFileSize = 1000;
        public ImageSizeValidationAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value,
             ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
                return new ValidationResult(ImageValues.ErrorImageNotFound);

            if (file.Length > _maxFileSize)
            {
                return new ValidationResult(GetErrorMessageOnFileSize());
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-imagSiz", ImageValues.ImageMaxSize.ToString());
            context.Attributes.Add("data-val-imageSizeValidation", GetErrorMessageOnFileSize());

        }
        private string GetErrorMessageOnFileSize()
        {
            return $"Снимката надвишава лимитa в Мегабайти - {ImageValues.ImageSizeInMBs.ToString()} MB";
        }
    }
}
