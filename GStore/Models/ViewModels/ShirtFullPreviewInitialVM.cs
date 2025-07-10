

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GStore.Models.ViewModels
{
    public class ShirtFullPreviewInitialVM
    {
        public int Id { get; set; }

        public string ShirtFrontImage { get; set; }

        public string ShirtBackImage { get; set; }

        public string ShirtName { get; set; }

        public string Description { get; set; }

        public string? Price { get; set; }

        public bool IsPromo { get; set; }

        public decimal? Discount { get; set; }

        public int? Quantity { get; set; }

        public bool IsShirtAvailable { get; set; }

        public bool IsShirtActive { get; set; }

        public int ColorId { get; set; }

        public string? ColorName { get; set; }

        public string? ColorCode { get; set; }

        public string? ImageLinkOne { get; set; }

        public string? ImageLinkTwo { get; set; }

        //[ValidateNever]
        //public IEnumerable<ImagesPerColorFullPreviewVM> ImagesPerColorFullPreviewVM { get; set; }
    }
}
