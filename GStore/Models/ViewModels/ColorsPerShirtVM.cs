using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GStore.Models.ViewModels
{
    public class ColorsPerShirtVM
    {

        public int ColorId { get; set; }

        [ValidateNever]
        public string ColorName { get; set; }

        [ValidateNever]
        public string ColorCode { get; set; }

        public bool IsSelected { get; set; }
    }
}
