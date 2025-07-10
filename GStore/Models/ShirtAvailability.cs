using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace GStore.Models
{
    public class ShirtAvailability
    {
        public int ProductId { get; set; }
        public int SizeSetId { get; set; }
        public int ColorSetId { get; set; }

        [Display(Name = "Налична")]
        public bool IsAvalable { get; set; }

        [Display(Name = "Активна")]
        public bool IsActive { get; set; }

        [ForeignKey("ProductId")]
        public virtual Shirt Shirt { get; set; } = null!;

        [ForeignKey("SizeSetId")]
        public virtual SizeSet SizeSet { get; set; } = null!;

        [ForeignKey("ColorSetId")]
        public virtual ColorSet ColorSet { get; set; }

    }
}
