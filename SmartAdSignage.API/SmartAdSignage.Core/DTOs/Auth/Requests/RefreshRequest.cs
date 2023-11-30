using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.DTOs.User.Requests
{
    public class RefreshRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
