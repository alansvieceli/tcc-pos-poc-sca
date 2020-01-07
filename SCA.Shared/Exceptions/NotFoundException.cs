using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Shared.Exceptions
{
    public class NotFoundException : ApplicationException
    {

        public NotFoundException(string message) : base(message)
        {

        }
    }
}
