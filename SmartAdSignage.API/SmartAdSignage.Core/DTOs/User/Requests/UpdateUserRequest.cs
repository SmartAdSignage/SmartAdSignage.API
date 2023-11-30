using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.User.Requests
{
    public class UpdateUserRequest
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
