using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCA.Shared.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SCA.Shared.CustomAttributes
{
    public class UnAuthorizedAttribute : TypeFilterAttribute
    {
        public UnAuthorizedAttribute() : base(typeof(UnauthorizedFilter))
        {
            //Empty constructor
        }
    }
    public class UnauthorizedFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (!IsAuthenticated)
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                context.Result = new JsonResult(new ResultError("Forbidden Access"));

            }
        }

    }

}
