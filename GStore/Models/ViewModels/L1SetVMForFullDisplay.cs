using System.ComponentModel.DataAnnotations;

namespace GStore.Models.ViewModels
{
    public class L1SetVMForFullDisplay
    {
        public int Id { get; set; }

        [StringLength(42)]
        public string Name { get; set; } = "";

        public bool IsActive { get; set; }

        public IEnumerable<L2SetVMForFullDisplay> L2SetsVMForFullDisplay { get; set; }
    }
}
