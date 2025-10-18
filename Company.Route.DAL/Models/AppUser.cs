using Microsoft.AspNetCore.Identity;

namespace Company.Route.DAL.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsAgree { get; set; }
    }
}
