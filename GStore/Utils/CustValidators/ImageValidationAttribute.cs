using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;


namespace GStore.Utils.CustValidators
{
    // This clas is no longer in use. I left it as a demo of my skills. Or the lack of them.
    // It makes Server side validation for file size and file (image) extensions. At once :)
    // When I started adding features for client side I decided to separate the two cases.
    public class ImageValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxFileSize = 1000;
        private readonly string[] _fileExtensions;
        private string errorOnFileSizeMessage = "";
        private string errorOnFileExtensionMessage = "";
        private bool IsErrorOnFileSize;
        private bool IsErrorOnFileExtension;
        public ImageValidationAttribute(int maxFileSize, string[] extensions)
        {
            _maxFileSize = maxFileSize;
            _fileExtensions = extensions;
        }

        protected override ValidationResult IsValid(object value,
             ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
                return new ValidationResult("Снимката не беше открита");

            if (file.Length > _maxFileSize)
            {
                IsErrorOnFileSize = true;
                GetErrorMessageOnFileSize();
            }

            var extension = Path.GetExtension(file.FileName);

            if (!_fileExtensions.Contains(extension.ToLower()))
            {
                IsErrorOnFileExtension = true;
                GetErrorMessageOnFileExtension();
            }

            if (IsErrorOnFileSize == true && IsErrorOnFileExtension == true)
            {
                string tempStr = string.Concat(errorOnFileExtensionMessage,
                    ". ", errorOnFileSizeMessage);
                return new ValidationResult(tempStr);
            }
            else if (IsErrorOnFileSize == true)

                return new ValidationResult(errorOnFileSizeMessage);

            else if (IsErrorOnFileExtension == true)

                return new ValidationResult(errorOnFileExtensionMessage);

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-imageValidation", "Image is either larger than the limit or file extension is incorrect.");
        }

        private void GetErrorMessageOnFileExtension()
        {
            errorOnFileExtensionMessage = $"Снимката трябва да е с разширение .jpg или .jpeg";
        }
        private void GetErrorMessageOnFileSize()
        {
            errorOnFileSizeMessage = $"Снимката надвишава позволеният лимит в Мегабайти";
        }
    }
}
