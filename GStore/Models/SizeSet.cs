using GStore.Models.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using static NuGet.Packaging.PackagingConstants;

namespace GStore.Models
{
    public class SizeSet
    {
        public int Id { get; set; }

        [Required]
        [StringLength(22)]
        public string Name { get; set; } = "";

        public bool IsActive { get; set; }

        public virtual List<Shirt> Products { get; set; }

    }
}
