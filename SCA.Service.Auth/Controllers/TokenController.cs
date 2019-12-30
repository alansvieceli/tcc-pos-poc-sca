using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Dto;
using SCA.Service.Auth.Services;

namespace SCA.Service.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {

        private readonly AuthService _authService;

        public TokenController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Content("*** SCA.Service.Auth ***");
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Generate([Bind("user,password")] LoginDto user)
        {
            return Ok(_authService.Authenticate(user));
        }
    }
}
