using GStore.Utils.Constants;
using GStore.Utils.CustValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GStore.Models.ViewModels
{
    public class ShirtAvailabilityComboVM
    {
        public int? ProductId { get; set; }

        public string ErrorMessage { get; set; } = VarietyTexts.ErrorMessageOnAvailabilitySelection;

        public string SuccessOnUpdate { get; set; } = "";

        [ValidateNever]
        public ShirtShortWithCategoryNameVM ShirtShortById { get; set; }

        [AvailabilityListValidation(ErrorMessage = VarietyTexts.ErrorMessageOnAvailabilitySelection)]
        public List<AvailabilityPerShirtVM> ListShirtAvailability { get; set; }
    }
}
