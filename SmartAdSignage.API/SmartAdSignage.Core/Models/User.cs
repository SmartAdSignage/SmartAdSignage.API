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

        public ICollection<Advertisement> Advertisements { get; set; }

        public ICollection<AdCampaign> AdCampaigns { get; set; }

        public ICollection<Panel> Panels { get; set; }
    }
}
