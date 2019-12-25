using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Shared.Results
{
    public class ResultApi : ResultBase
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime Expire { get; set; }

        public ResultApi() : base(false)
        {

        }

        public ResultApi(bool status) : base(status)
        {
        }

        public ResultApi(bool status, string message) : base(status)
        {
            this.Message = message;
        }

        public ResultApi(bool status, string message, string token, DateTime expire) : base(status)
        {
            this.Message = message;
            this.Token = token;
            this.Expire = expire;
        }
    }
}
