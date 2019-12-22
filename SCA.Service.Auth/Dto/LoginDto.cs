using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Service.Auth.Dto
{
    public class LoginDto
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
