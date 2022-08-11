using Microsoft.AspNetCore.Mvc;

namespace QS.BaseWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Index()
        {
            return new string[] { "Index 1" };
        }

        [HttpGet]
        public IEnumerable<string> Index2()
        {
            return new string[] { "Index 2" };
        }
    }
}
