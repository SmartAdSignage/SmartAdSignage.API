using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartAdSignage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        
        [HttpGet]
        [Authorize]
        [Route("getCustomers")]
        public IEnumerable<string> GetCustomers()
        {
            return new List<string>() { "Vasya Pupkin"};
        }
    }
}
