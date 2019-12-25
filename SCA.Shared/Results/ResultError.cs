using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Shared.Results
{
    public class ResultError : ResultBase
    {

        public string error { get; set; }

        public ResultError(string error) : base(false)
        {
            this.error = error;
        }
    }
}
