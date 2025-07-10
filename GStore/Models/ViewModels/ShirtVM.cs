using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GStore.Models.ViewModels
{
    public class ShirtVM
    {
        public int Id { get; set; }

        [StringLength(190)]
        public string ImageLinkOne { get; set; }

        [StringLength(190)]
        public string ImageLinkTwo { get; set; }

        [Display(Name = "Име на продукта")]
        [Required(ErrorMessage = "Моля, въведете Име на продукта")]
        [StringLength(120)]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Моля, въведете Описание")]
        [StringLength(1600)]
        public string Description { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Моля, въведете Цена")]
        public string? Price { get; set; }

        [Display(Name = "Текуща промоция ?")]
        public bool IsPromo { get; set; }

        [Display(Name = "Промоционална цена")]
        [Range(0.01, 20000, ErrorMessage = "Please enter a positive Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Ако продуктът е с ограничена бройка, моля въведете наличнита бройка")]
        [Range(1, 20000, ErrorMessage = "Моля, въведете положителна налична бройка")]
        public int? Quantity { get; set; }

        [Display(Name = "Текуща наличност")]
        public bool IsAvailable { get; set; }

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }

        public virtual string? L2Name { get; set; }


        [Display(Name = "Избор на категория от Ниво 2, към която да принадлежи")]
        [Required(ErrorMessage = "Моля изберете Ниво 2 категория към която продуктът да принадлежи")]
        public string? SelectedCategoryValue { get; set; } = "";

        [ValidateNever]
        public virtual IEnumerable<SelectListItem> L2Sets { get; set; }

        [ValidateNever]
        public virtual List<Level2Set> Level2Sets { get; set; }

        [ValidateNever]
        public virtual List<ColorSet> ColorSets { get; set; }

        [ValidateNever]
        public virtual List<SizeSet> SizeSets { get; set; }
    }
}
