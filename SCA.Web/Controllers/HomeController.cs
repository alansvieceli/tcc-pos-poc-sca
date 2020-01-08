using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Entities.Auth;
using SCA.Shared.Entities.Enums;
using SCA.Shared.CustomAttributes;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SCA.Shared.Dto;
using Microsoft.AspNetCore.Http;
using SCA.Shared.Results;
using Microsoft.Extensions.Configuration;
using SCA.Shared.CustomController;

namespace SCA.Web.Controllers
{
    public class HomeController : ScaController
    {
        private readonly IConfiguration _configuration;
        private string _host;
        private int _port;

        public HomeController(IConfiguration config)
        {
            this._configuration = config;

            Prepare();
        }

        protected override void Prepare()
        {
            this._host = this._configuration.GetSection("ConfigApp").GetSection("host").Value;
            this._port = ConfigurationBinder.GetValue<int>(this._configuration.GetSection("ConfigApp"), "port", 80);
        }

        public override void SetToken(string token)
        {

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

        public IActionResult NoPermission()
        {
            ViewBag.UserRole = GetRole();
            return View("NoPermission");
        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public IActionResult Unauthorized()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            ViewBag.UserRole = GetRole();
            return View("Unauthorized");
        }

        private string GetRole()
        {
            string valor = "";
            if (this.HavePermission(Role.ADMIN))
                valor = " - ADMIN";
            else
            if (this.HavePermission(Role.MONITOR))
                valor = " - MONITOR";
            else
            if (this.HavePermission(Role.MAINTENANCE))
                valor = " - MAINTENANCE";
            else
            if (this.HavePermission(Role.USER_COMMON))
                valor = " - USER_COMMON";
            else
                valor = "NOTHING";

            HttpContext.Session.SetString("RoleAcces", valor);
            return valor;
        }

        private LoginDto ConverteUserToLoginDto(User user)
        {
            return new LoginDto(user.UserId, user.Password);
        }
    }
}
