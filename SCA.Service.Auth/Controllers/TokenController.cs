using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA.Service.Auth.Dto;
using SCA.Service.Auth.Services;
using System.Net;

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
        public IActionResult Get()
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
