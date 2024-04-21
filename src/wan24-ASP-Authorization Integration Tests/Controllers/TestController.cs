using Microsoft.AspNetCore.Mvc;

namespace wan24_ASP_Authorization_Integration_Tests.Controllers
{
    [ApiController, Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("unauthorized"), TestAuth(false)]
        public IActionResult UnauthorizedPath() => Ok();

        [HttpGet("authorized"), TestAuth(true)]
        public IActionResult AuthorizedPath() => Ok("Authorized");
    }
}
