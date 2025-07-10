using GStore.Utils.CustValidators;
using GStore.Utils.Constants;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using GStore.Utils.ImagesValues;

namespace GStore.Models.ViewModels
{
    public class UploadImageVM
    {
        public int ProductId { get; set; } = 0;

        public int ImageType { get; set; } = 0;

        public int? ColorId { get; set; }

        public string? ImagePath { get; set; }

        [Display(Name = "Изберете снимка за качване")]
        [Required]
        [ImageSizeValidation(ImageValues.ImageMaxSize)]
        [ImageExtValidation(new string[] { ".jpg", ".jpeg" })]
        //[ImageHeightValidation]
        public IFormFile ImageToUpload { get; set; }
    }
}
