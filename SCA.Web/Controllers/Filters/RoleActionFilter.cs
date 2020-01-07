using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using SCA.Shared.CustomController;

namespace SCA.Web.Controllers.Filters
{
   public class RoleActionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (ScaController)context.Controller;
            controller.ViewBag.UserRole = controller.HttpContext.Session.GetString("RoleAcces");
        }
    }
}
