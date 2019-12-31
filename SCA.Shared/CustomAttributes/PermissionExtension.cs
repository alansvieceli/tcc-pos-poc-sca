using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SCA.Shared.CustomAttributes
{
    public static class PermissionExtension
    {
        public static bool HavePermission(this Controller c, Role claimValue)
        {
            var user = c.HttpContext.User as ClaimsPrincipal;
            bool havePer = user.HasClaim(claimValue.ToString(), claimValue.ToString());
            return havePer;
        }
        public static bool HavePermission(this IIdentity claims, string claimValue)
        {
            var userClaims = claims as ClaimsIdentity;
            bool havePer = userClaims.HasClaim(claimValue, claimValue);
            return havePer;
        }
    }
}
