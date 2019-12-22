using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Auth.Models
{
    public class ResultApi
    {
        public bool Result { get; private set; }
        public string Message { get; private set; }
        public string Token { get; private set; }
        public DateTime Expire { get; private set; }

        public ResultApi(bool result)
        {
            Result = result;
        }

        public ResultApi(bool result, string message)
        {
            Result = result;
            Message = message;
        }

        public ResultApi(bool result, string message, string token, DateTime expire)
        {
            Result = result;
            Message = message;
            Token = token;
            Expire = expire;
        }
    }
}
