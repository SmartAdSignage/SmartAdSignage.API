using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.User.Responses
{
    public class UserResponse
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
