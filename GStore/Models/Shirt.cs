using GStore.Models.ViewModels;
using GStore.Utils.Constants;
using GStore.Utils.ImagesValues;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Xml.Linq;

namespace GStore.Models
{
    public class Shirt
    {
        public int Id { get; set; }

        [Column("ImageLinkOne")]
        [StringLength(190)]
        public string ImageLinkOne { get; set; }

        [StringLength(190)]
        public string ImageLinkTwo { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1600)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 20000)]
        public decimal Price { get; set; }

        [DefaultValue(false)]
        public bool IsPromo { get; set; }

        [Range(0.01, 20000)]
        public decimal? Discount { get; set; }

        public int? Quantity { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Level2Set Level2Set { get; set; }

        public virtual List<ShirtColorSet> ShirtColorSets { get; set; }

        public virtual List<ColorSet> ColorSets { get; set; }

        public virtual List<SizeSet> SizeSets { get; set; }

        public virtual List<ShirtAvailability> ShirtAvailabilitys { get; set; }

        public virtual List<ProductExtraImage> ProductExtraImages { get; set; }
    }
}
