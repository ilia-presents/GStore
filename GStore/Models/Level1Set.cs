using GStore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace GStore.Models
{
    public class Level1Set
    {
        // Those are my product categories. Level1 means top level category
        public int Id { get; set; }
        [StringLength(42)]
        public string Name { get; set; } = "";
        public bool IsActive { get; set; }
        public virtual IEnumerable<Level2Set> Level2Sets { get; set; }
    }
}
