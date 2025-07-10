using System.ComponentModel.DataAnnotations;

namespace GStore.Models.ViewModels
{
    public class L2SetVMForFullDisplay
    {
        public int Id { get; set; }

        [StringLength(42)]
        public string Name { get; set; } = "";

        public bool IsActive { get; set; }
    }
}
