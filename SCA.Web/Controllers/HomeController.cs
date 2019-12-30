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

namespace SCA.Web.Controllers
{
    public class HomeController : Controller
    {
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
                                objLoggedInUser.USERID = cValue;
                                break;
                            case "EMAILID":
                                objLoggedInUser.EMAILID = cValue;
                                break;
                            case "DIRECTOR":
                                objLoggedInUser.ACCESS_LEVEL = cValue;
                                break;
                            case "SUPERVISOR":
                                objLoggedInUser.ACCESS_LEVEL = cValue;
                                break;
                            case "ANALYST":
                                objLoggedInUser.ACCESS_LEVEL = cValue;
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
            var result = client.PostAsync("http://localhost:7000/auth/api/token/authenticate", content).Result;

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
            if (this.HavePermission(Roles.DIRECTOR))
                return " - DIRECTOR";
            if (this.HavePermission(Roles.SUPERVISOR))
                return " - SUPERVISOR";
            if (this.HavePermission(Roles.ANALYST))
                return " - ANALYST";
            return "NOTHING";
        }

        private LoginDto ConverteUserToLoginDto(User user)
        {
            return new LoginDto(user.USERID, user.PASSWORD);
        }


    }
}
