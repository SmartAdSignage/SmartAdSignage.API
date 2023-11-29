using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.Responses
{
    public class RegisteredUserResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
