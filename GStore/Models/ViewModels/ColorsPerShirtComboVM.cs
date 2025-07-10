
using GStore.Utils.Constants;
using GStore.Utils.CustValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace GStore.Models.ViewModels
{
    public class ColorsPerShirtComboVM
    {
        public int? ProductId { get; set; }

        public string ErrorMessage { get; set; } = VarietyTexts.ErrorMessageOnColoreSelection;

        public string SuccessOnUpdate { get; set; } = "";

        [ValidateNever]
        public ShirtShortWithCategoryNameVM ShirtShortById { get; set; }

        //[Required]
        [ColorsListValidation(ErrorMessage = VarietyTexts.ErrorMessageOnColoreSelection)]
        public List<ColorsPerShirtVM> ListColorsPerShirt { get; set; }
    }
}
