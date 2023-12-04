using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmartAdSignage.Core.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CompanyName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public DateTime? RegistrationDate { get; set; } = DateTime.Now;

        public virtual ICollection<Advertisement> Advertisements { get; set; }

        public virtual ICollection<AdCampaign> AdCampaigns { get; set; }

        public virtual ICollection<Panel> Panels { get; set; }
    }
}
