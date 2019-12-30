using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Dto;
using SCA.Service.Auth.Providers;
using System.IdentityModel.Tokens.Jwt;
using SCA.Shared.Results;

namespace SCA.Service.Auth.Services
{
    public class AuthService
    {

        public ResultToken Authenticate(LoginDto user)
        {
            if (user == null)
            {
                return new ResultToken(false, "Usuário ou senha inválidos");
            }

            TokenProvider _tokenProvider = new TokenProvider();
            var _token = _tokenProvider.AuthenticateUser(user.User.Trim(), user.Password.Trim());

            if (_token == null)
            {
                return new ResultToken(false, "Login inválido / Token não pode ser gerado");
            }

            string userToken = new JwtSecurityTokenHandler().WriteToken(_token);                
            return new ResultToken(true, "Token gerado com sucesso", userToken, _token.ValidTo);
        }
    }
}
