using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SCA.Shared.Model
{
    public abstract class ResultBase
    {

        public bool status { get;  }

        public ResultBase(bool status)
        {
            this.status = status;
        }
    }
}
