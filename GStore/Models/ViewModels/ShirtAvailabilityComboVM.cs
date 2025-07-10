using GStore.Utils.Constants;
using GStore.Utils.CustValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GStore.Models.ViewModels
{
    public class ShirtAvailabilityComboVM
    {
        public int? ProductId { get; set; }

        public string ErrorMessage { get; set; } = VarietyValues.ErrorMessageOnAvailabilitySelection;

        public string SuccessOnUpdate { get; set; } = "";

        [ValidateNever]
        public ShirtShortWithCategoryNameVM ShirtShortById { get; set; }

        [AvailabilityListValidation(ErrorMessage = VarietyValues.ErrorMessageOnAvailabilitySelection)]
        public List<AvailabilityPerShirtVM> ListShirtAvailability { get; set; }
    }
}
