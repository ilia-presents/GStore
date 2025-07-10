using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GStore.Models.ViewModels
{
    public class ColorSetVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля въведете код на цвета:")]
        [Column(TypeName = "varchar(82)")]
        [Display(Name = "Код на цвета")]
        public string ColorCode { get; set; } = "";

        [Required(ErrorMessage = "Моля въведете име на цвета")]
        [StringLength(42)]
        [Display(Name = "Име")]
        public string Name { get; set; } = "";

        [Display(Name = "Активен")]
        public bool IsActive { get; set; } = true;
    }
}
