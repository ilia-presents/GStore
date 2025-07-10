using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GStore.Models
{
    public class ShirtSizeSet
    {
        public int ProductId { get; set; }

        public int SizeSetId { get; set; }
        public virtual Shirt Product { get; set; } = null!;
        public virtual SizeSet SizeSet { get; set; } = null!;

        [Display(Name = "Наличен")]
        [DefaultValue(true)]
        public bool IsAvalable { get; set; }
    }
}
