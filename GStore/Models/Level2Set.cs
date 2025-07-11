using GStore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GStore.Models
{
    public class Level2Set
    {
        public int Id { get; set; }

        [StringLength(42)]
        public string Name { get; set; } = "";
        public int? Level1SetId { get; set; }
        public bool IsActive { get; set; }
        public virtual Level1Set Level1Set { get; set; }

        public virtual IEnumerable<Shirt> Shirts { get; set; }

    }
}
