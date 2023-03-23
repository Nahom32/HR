using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}
