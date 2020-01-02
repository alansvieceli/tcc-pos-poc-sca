using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCA.Shared.CustomAttributes.Enums;
using SCA.Shared.Entities;
using SCA.Shared.Results;
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
            Arguments = new object[] { TipoRetornoAcesso.API };
        }

        public UnAuthorizedAttribute(TipoRetornoAcesso tipoRetorno) : base(typeof(UnauthorizedFilter))
        {
            Arguments = new object[] { tipoRetorno };
        }
    }
    public class UnauthorizedFilter : IAuthorizationFilter
    {
        private readonly TipoRetornoAcesso _tipoRetorno;

        public UnauthorizedFilter(TipoRetornoAcesso tipoRetorno)
        {
            this._tipoRetorno = tipoRetorno;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (!IsAuthenticated)
            {
                if (this._tipoRetorno.Equals(TipoRetornoAcesso.WEB))
                {
                    context.Result = new RedirectResult("~/Home/NoPermission");
                } 
                else
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Result = new JsonResult(new ResultApi(false, "Forbidden Access"));
                }
            }
        }

    }

}
