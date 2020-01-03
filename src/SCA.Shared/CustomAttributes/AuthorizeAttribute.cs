using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCA.Shared.CustomAttributes.Enums;
using SCA.Shared.Entities;
using SCA.Shared.Entities.Enums;
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
        public AuthorizeAttribute(params Role[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { TipoRetornoAcesso.API, claim };
        }

        public AuthorizeAttribute(TipoRetornoAcesso tipoRetorno, params Role[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { tipoRetorno, claim };
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        private readonly Role[] _claim;
        private readonly TipoRetornoAcesso _tipoRetorno;

        public AuthorizeFilter(TipoRetornoAcesso tipoRetorno, params Role[] claim)
        {
            this._tipoRetorno = tipoRetorno;
            this._claim = claim;
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
                    if (context.HttpContext.User.HasClaim(item.ToString(), item.ToString()))
                        flagClaim = true;
                }
                if (!flagClaim)
                {
                    if (this._tipoRetorno.Equals(TipoRetornoAcesso.WEB))
                    {
                        context.Result = new RedirectResult("~/Home/Unauthorized");
                    } else
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Result = new JsonResult(new ResultApi(false, "Unauthorized"));
                    }
                }
            } else
            {
                if (this._tipoRetorno.Equals(TipoRetornoAcesso.WEB))
                {
                    context.Result = new RedirectResult("~/Home/NoPermission");
                } else
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Result = new JsonResult(new ResultApi(false, "Forbidden Access"));
                }
            }
            return;
        }
    }
}
