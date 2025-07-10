using GStore.Utils.Constants;
using GStore.Utils.CustValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GStore.Models.ViewModels
{
    public class SizesPerShirtComboVM
    {
        public int? ProductId { get; set; }

        public string ErrorMessage { get; set; } = VarietyValues.ErrorMessageOnSizeSelection;

        public string SuccessOnUpdate { get; set; } = "";

        [ValidateNever]
        public ShirtShortWithCategoryNameVM ShirtShortById { get; set; }

        [GenericCheckBoxListValidation(ErrorMessage = VarietyValues.ErrorMessageOnSizeSelection)]  
        public List<SelectListItem> ListSizesPerShirt { get; set; }
    }
}
