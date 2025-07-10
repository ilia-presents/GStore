using System.ComponentModel.DataAnnotations.Schema;

namespace GStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public string CartId { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public decimal Price { get; set; }

        [NotMapped]
        public bool IsPromo { get; set; }

        [NotMapped]
        public decimal PromoPrice { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int ProductId { get; set; }

        public virtual Shirt Product { get; set; }
    }
}
