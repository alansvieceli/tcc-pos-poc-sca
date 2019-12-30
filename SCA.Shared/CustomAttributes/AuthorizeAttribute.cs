using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCA.Shared.Entities;
using SCA.Shared.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SCA.Shared.CustomAttributes
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _claim;

        public AuthorizeFilter(params string[] claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (IsAuthenticated)
            {
                bool flagClaim = false;
                foreach (var item in _claim)
                {
                    if (context.HttpContext.User.HasClaim(item, item))
                        flagClaim = true;
                }
                if (!flagClaim)
                {
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new ResultApi(false, "Unauthorized"));
                }
            } else
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                context.Result = new JsonResult(new ResultApi(false, "Forbidden Access"));
            }
            return;
        }
    }
}
