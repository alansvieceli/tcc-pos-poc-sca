using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Shared.Model
{
    public class ResultError : ResultBase
    {

        public string error { get;  }

        public ResultError(string error) : base(false)
        {
            this.error = error;
        }
    }
}
