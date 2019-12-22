using Microsoft.AspNetCore.Mvc;
using SCA.Service.Auth.Dto;
using SCA.Service.Auth.Models;
using SCA.Service.Auth.Providers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Auth.Services
{
    public class AuthService
    {

        public ResultApi Authenticate(LoginDto user)
        {
            if (user == null)
            {
                return new ResultApi(false, "Usuário ou senha inválidos");
            }

            TokenProvider _tokenProvider = new TokenProvider();
            var _token = _tokenProvider.AuthenticateUser(user.User.Trim(), user.Password.Trim());

            if (_token == null)
            {
                return new ResultApi(false, "Login inválido / Token não pode ser gerado");
            }

            string userToken = new JwtSecurityTokenHandler().WriteToken(_token);                
            return new ResultApi(true, "Token gerado com sucesso", userToken, _token.ValidTo);
        }
    }
}
