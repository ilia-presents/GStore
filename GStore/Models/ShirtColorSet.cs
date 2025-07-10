using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace GStore.Models
{
    public class ShirtColorSet
    {
        public int ProductId { get; set; }

        public int ColorSetId { get; set; }
        public virtual Shirt Product { get; set; }
        public virtual ColorSet ColorSet { get; set; }

        [Column("ImageLinkOne")]
        [StringLength(200)]
        public string? ImageLinkOne { get; set; }

        [StringLength(200)]
        public string? ImageLinkTwo { get; set; }

        [Display(Name = "Available")]
        [DefaultValue(true)]
        public bool IsAvalable { get; set; }
    }
}
