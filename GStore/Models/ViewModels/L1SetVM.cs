using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GStore.Models.ViewModels
{
    public class L1SetVM
    {
        public int Id { get; set; }

        [Display(Name = "Име на категорията")]
        [Required(ErrorMessage = "Моля въведете име на категорията")]
        [StringLength(42)]
        public string Name { get; set; } = "";

        [Display(Name = "Активна")]
        public bool IsActive { get; set; }
    }
}
