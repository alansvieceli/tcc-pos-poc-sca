using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Shared.Dto
{
    public class LoginDto
    {
        public string User { get; set; }
        public string Password { get; set; }

        public LoginDto()
        {

        }

        public LoginDto(string User, string Password)
        {
            this.User = User;
            this.Password = Password;
        }
    }
}
