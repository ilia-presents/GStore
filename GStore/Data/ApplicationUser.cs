using Microsoft.AspNetCore.Identity;

namespace GStore.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool? IsActive { get; set; }
        public DateTime? RegisterOn { get; set; }
    }
}
