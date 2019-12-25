using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Shared.Entities
{
    public class User
    {
        public string USERID { get; set; }
        public string PASSWORD { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string EMAILID { get; set; }
        public string ACCESS_LEVEL { get; set; }
    }
}
