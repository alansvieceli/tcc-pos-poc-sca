using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Entities;
using SCA.Shared.Entities.Enums;
using SCA.Shared.CustomAttributes;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SCA.Shared.Dto;
using System.Net;
using Microsoft.AspNetCore.Http;
using SCA.Shared.Results;
using Microsoft.Extensions.Configuration;

namespace SCA.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private string _host;
        private int _port;

        public HomeController(IConfiguration config)
        {
            this._configuration = config;

            Prepare();
        }

        private void Prepare()
        {
            this._host = this._configuration.GetSection("ConfigApp").GetSection("host").Value;
            this._port = ConfigurationBinder.GetValue<int>(this._configuration.GetSection("ConfigApp"), "port", 80);
        }

        public IActionResult Index()
        {
            User objLoggedInUser = new User();
            if (User.Identity.IsAuthenticated)
            {
                var claimsIndentity = HttpContext.User.Identity as ClaimsIdentity;
                var userClaims = claimsIndentity.Claims;

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    foreach (var claim in userClaims)
                    {
                        var cType = claim.Type;
                        var cValue = claim.Value;
                        switch (cType)
                        {
                            case "USERID":
                                objLoggedInUser.UserId = cValue;
                                break;
                            case "EMAILID":
                                objLoggedInUser.Email = cValue;
                                break;
                            case "ADMIN":
                                objLoggedInUser.AcessLevel = Role.ADMIN;
                                break;
                            case "MONITOR":
                                objLoggedInUser.AcessLevel = Role.MONITOR;
                                break;
                            case "USER_COMMON":
                                objLoggedInUser.AcessLevel = Role.USER_COMMON;
                                break;
                        }
                    }
                    
                }
            }
            ViewBag.UserRole = GetRole();
            return View("Index", objLoggedInUser);
        }

        public async Task<IActionResult> LoginUser(User user)
        {
            LoginDto loginDto = ConverteUserToLoginDto(user);
            var jsonContent = JsonConvert.SerializeObject(loginDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            var result = client.PostAsync($"http://{this._host}:{this._port}/auth/api/token/authenticate", content).Result;

            string userToken = null;
            if (result.IsSuccessStatusCode)
            {
                if (result.Content != null)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    ResultToken resultToken = JsonConvert.DeserializeObject<ResultToken>(responseContent);
                    userToken = resultToken.Token;
                }
            }

            if (userToken != null)
            {
                HttpContext.Session.SetString("JWToken", userToken);   //Save token in session object
            }
            return Redirect("~/Home/Index");
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Home/Index");
        }

        private string GetRole()
        {
            if (this.HavePermission(Role.ADMIN))
                return " - ADMIN";
            if (this.HavePermission(Role.MONITOR))
                return " - MONITOR";
            if (this.HavePermission(Role.USER_COMMON))
                return " - USER_COMMON";
            return "NOTHING";
        }

        private LoginDto ConverteUserToLoginDto(User user)
        {
            return new LoginDto(user.UserId, user.Password);
        }


    }
}
