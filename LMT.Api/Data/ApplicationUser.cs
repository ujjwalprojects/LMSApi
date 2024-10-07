using Microsoft.AspNetCore.Identity;

namespace LMT.Api.Data
{
    public class ApplicationUser: IdentityUser
    {
        public int District_Id { get; set; }
    }
}
