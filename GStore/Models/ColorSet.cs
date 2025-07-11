using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using GStore.Models.ViewModels;
using System.Linq.Expressions;

namespace GStore.Models
{
    public class ColorSet
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(82)")]
        public string ColorCode { get; set; } = "";

        [StringLength(42)]
        public string Name { get; set; } = "";

        public bool IsActive { get; set; } = true;

        public virtual List<Shirt> Products { get; set; }
    }
}
