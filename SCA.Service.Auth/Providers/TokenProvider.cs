using Microsoft.IdentityModel.Tokens;
using SCA.Domain.Model;
using SCA.Domain.Repository;
using SCA.Shared.Domain.Properties;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Service.Auth.Providers
{
    public class TokenProvider
    {

        public JwtSecurityToken LoginUser(string UserID, string Password)
        {

            List<User> UserList = UserRepository.GetUsers().ToList();
            var user = UserList.SingleOrDefault(x => x.USERID == UserID);

            if (user == null)
                return null;

            if (Password == user.PASSWORD)
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
            _claim = new Claim(ClaimTypes.Name, user.FIRST_NAME + " " + user.LAST_NAME);
            claims.Add(_claim);
            _claim = new Claim("USERID", user.USERID);
            claims.Add(_claim);
            _claim = new Claim("EMAILID", user.EMAILID);
            claims.Add(_claim);
            _claim = new Claim(user.ACCESS_LEVEL, user.ACCESS_LEVEL);
            claims.Add(_claim);
            return claims.AsEnumerable<Claim>();
        }
    }
}
