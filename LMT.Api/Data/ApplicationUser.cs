using Microsoft.AspNetCore.Identity;

namespace LMT.Api.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string UserFullName { get; set; } = string.Empty;
        public int District_Id { get; set; }
    }
}
