using Microsoft.AspNetCore.Identity;

namespace DA_webRestaurant.Models
{
    public class Employee : IdentityUser
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
