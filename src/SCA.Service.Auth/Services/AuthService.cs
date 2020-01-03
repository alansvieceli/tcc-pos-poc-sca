using Microsoft.AspNetCore.Mvc;
using SCA.Shared.Dto;
using SCA.Service.Auth.Providers;
using System.IdentityModel.Tokens.Jwt;
using SCA.Shared.Results;

namespace SCA.Service.Auth.Services
{
    public class AuthService
    {
        private readonly TokenProvider _tokenService;

        public AuthService(TokenProvider tokenService)
        {
            this._tokenService = tokenService;
        }

        public ResultToken Authenticate(LoginDto user)
        {
            if (user == null)
            {
                return new ResultToken(false, "Usuário ou senha inválidos");
            }

            var _token = this._tokenService.AuthenticateUser(user.User.Trim(), user.Password.Trim());

            if (_token == null)
            {
                return new ResultToken(false, "Login inválido / Token não pode ser gerado");
            }

            string userToken = new JwtSecurityTokenHandler().WriteToken(_token);                
            return new ResultToken(true, "Token gerado com sucesso", userToken, _token.ValidTo);
        }
    }
}
