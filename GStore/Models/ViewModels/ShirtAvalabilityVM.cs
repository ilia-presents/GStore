using System.ComponentModel.DataAnnotations;

namespace GStore.Models.ViewModels
{
    public class ShirtAvalabilityVM
    {
        public int ShirtId { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

        public string SomeString { get; set; }

        public bool IsAvailable { get; set; }
    }
}
