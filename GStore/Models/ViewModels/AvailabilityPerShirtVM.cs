using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GStore.Models.ViewModels
{
    public class AvailabilityPerShirtVM
    {
        public int ColorId { get; set; }

        public int SizeId { get; set; }

        [ValidateNever]
        public string ColorName { get; set; }

        [ValidateNever]
        public string ColorCode { get; set; }


        [ValidateNever]
        public string SizeName { get; set; }

        public bool IsSelected { get; set; }
    }
}
