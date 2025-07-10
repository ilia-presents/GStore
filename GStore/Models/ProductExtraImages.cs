using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GStore.Models
{
    public class ProductExtraImage
    {
        public int Id { get; set; }

        [Column("ImageLinkOne")]
        [StringLength(190)]
        public string ImageLink { get; set; }

        [ForeignKey("ProductId")]
        public virtual Shirt Shirt { get; set; }
    }
}
