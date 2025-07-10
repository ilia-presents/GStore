using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace GStore.Models.ViewModels
{
    public class SizeSetVM
    {
        public int Id { get; set; }

        [Display(Name = "Размер")]
        [Required(ErrorMessage = "Моля въведете валиден размер")]
        [StringLength(22)]
        public string Name { get; set; } = "";

        [Display(Name = "Активен")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
