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
            this._authService = authService;
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Generate([Bind("user,password")] LoginDto user)
        {
            return Ok(this._authService.Authenticate(user));
        }
    }
}
