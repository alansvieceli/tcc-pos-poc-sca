
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using SCA.Shared.CustomController;

namespace SCA.Web.Controllers.Filters
{
    public class PegarTokenActionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (ScaController)context.Controller;
            controller.SetToken(controller.HttpContext.Session.GetString("JWToken"));
            controller.ViewBag.UserRole = controller.HttpContext.Session.GetString("RoleAcces");
        }
    }
}
