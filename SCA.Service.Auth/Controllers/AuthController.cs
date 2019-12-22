using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA.Service.Auth.Dto;
using SCA.Service.Auth.Services;
using System.Net;

namespace SCA.Service.Auth.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {

        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Content("*** SCA.Service.Auth ***");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([Bind("user,password")] LoginDto user)
        {
            return Ok(_authService.Login(user));
        }
    }
}
