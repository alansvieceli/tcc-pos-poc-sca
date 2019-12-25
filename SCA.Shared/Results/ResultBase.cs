using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SCA.Shared.Results
{
    public abstract class ResultBase
    {

        public bool status { get; set; }

        public ResultBase(bool status)
        {
            this.status = status;
        }
    }
}
