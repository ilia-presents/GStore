
using GStore.Utils.Constants;
using GStore.Utils.CustValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace GStore.Models.ViewModels
{
    public class ColorsPerShirtComboVM
    {
        public int? ProductId { get; set; }

        public string ErrorMessage { get; set; } = VarietyValues.ErrorMessageOnColoreSelection;

        public string SuccessOnUpdate { get; set; } = "";

        [ValidateNever]
        public ShirtShortWithCategoryNameVM ShirtShortById { get; set; }

        //[Required]
        [ColorsListValidation(ErrorMessage = VarietyValues.ErrorMessageOnColoreSelection)]
        public List<ColorsPerShirtVM> ListColorsPerShirt { get; set; }
    }
}
