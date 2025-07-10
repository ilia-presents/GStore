using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GStore.Models.ViewModels
{
    public class L2SetVM
    {
        public int Id { get; set; }

        [Display(Name = "Име на категорията Ниво 2")]
        [Required(ErrorMessage = "Моля въведете име на категорията")]
        [StringLength(42)]
        public string Name { get; set; } = "";

        [Display(Name = "Име на горна категория")]
        [StringLength(42)]
        public string L1Name { get; set; } = "";

        public int? Level1SetId { get; set; }

        [Display(Name = "Активна")]
        public bool IsActive { get; set; }

        [Display(Name = "Избор на категория от Ниво 1, към която да принадлежи")]
        [Required(ErrorMessage = "Моля изберете Ниво 1 категория към която тази да принадлежи ")]
        public string SelectedRadioValue { get; set; } = "";

        [ValidateNever]
        public virtual IEnumerable<SelectListItem> L1Sets { get; set; }
    }

    //public class RoleVM
    //{
    //    public int RoleId { get; set; }
    //    public string RoleName { get; set; }
    //    public bool IsSelected { get; set; }
    //}
}
