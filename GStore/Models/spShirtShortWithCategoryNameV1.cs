using GStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GStore.Models
{
    
    public class spShirtShortWithCategoryName : spShirtShortWithCategoryNameById
    {
        public bool IsActive { get; set; }
        public bool IsAvailable { get; set; }
    }
}
