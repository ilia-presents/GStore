

using Microsoft.EntityFrameworkCore;

namespace GStore.Models
{
    [Keyless]
    public class spShirtPreviewModel
    {
        public int Id { get; set; }

        public string ImageLinkOne { get; set; }

        public string ImageLinkTwo { get; set; }

        public string Name { get; set; }

        public string ColorCode { get; set; }

        public decimal Price { get; set; }

        public int? CategoryId { get; set; }

        public bool IsPromo { get; set; }

        public decimal? Discount { get; set; }
    }
}