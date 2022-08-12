using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QS.Shared.Constants;

namespace QS.BaseWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Index()
        {
            return new string[] { "UnAuthorize" };
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public IEnumerable<string> Index1()
        {
            return new string[] { "Authorize" };
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public IEnumerable<string> Index2()
        {
            return new string[] { "Admin" };
        }

        [HttpGet]
        [Authorize(Roles = Role.User)]
        public IEnumerable<string> Index3()
        {
            return new string[] { "User" };
        }
    }
}
