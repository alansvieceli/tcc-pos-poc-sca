using Microsoft.IdentityModel.Tokens;
using SCA.Shared.Entities.Auth;
using SCA.Shared.Domain.Properties;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SCA.Service.Auth.Services;

namespace SCA.Service.Auth.Providers
{
    public class TokenProvider
    {
        private readonly UserService _userService;

        public TokenProvider(UserService userService)
        {
            this._userService = userService;
        }

        public JwtSecurityToken AuthenticateUser(string UserID, string Password)
        {

            List<User> UserList = this._userService.FindAll().ToList();
            var user = UserList.SingleOrDefault(x => x.UserId == UserID);

            if (user == null)
                return null;

            if (Password == user.Password)
            {
                var key = Encoding.ASCII.GetBytes(Token.Key);
                
                var JWToken = new JwtSecurityToken(
                    issuer: Token.Issuer,
                    audience: Token.Audience,
                    claims: GetUserClaims(user),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    //HS256 Algorithm to encrypt Token  
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                //var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return JWToken;
            } else
            {
                return null;
            }
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            Claim _claim;
            _claim = new Claim(ClaimTypes.Name, user.FirtName + " " + user.LastName);
            claims.Add(_claim);
            _claim = new Claim("USERID", user.UserId);
            claims.Add(_claim);
            _claim = new Claim("EMAILID", user.Email);
            claims.Add(_claim);
            _claim = new Claim(user.AcessLevel.ToString(), user.AcessLevel.ToString());
            claims.Add(_claim);
            return claims.AsEnumerable<Claim>();
        }
    }
}
