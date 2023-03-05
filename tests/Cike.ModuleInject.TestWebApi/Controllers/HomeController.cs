using Cike.ModuleInject.TestService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cike.ModuleInject.TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetUserNameAsync("Hello,User"));
        }
    }
}
