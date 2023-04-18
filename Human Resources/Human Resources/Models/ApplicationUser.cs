using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Models
{
    public class ApplicationUser:IdentityUser
    {
        
        public string Name { get; set; }
        public string pictureURL { get; set; }
        public string PositionName { get; set; }

    }
}
