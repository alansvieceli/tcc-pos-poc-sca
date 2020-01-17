using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Shared.Results
{
    public class ResultApi : ResultBase
    {
        public string message { get; set; }
        public int Id { get; set; }

        public ResultApi() : base(false)
        {

        }

        public ResultApi(bool status) : base(status)
        {

        }

        public ResultApi(bool status, string message) : base(status)
        {
            this.message = message;
        }

        public ResultApi(bool status, int Id) : base(status)
        {
            this.Id = Id;
        }
    }
}
