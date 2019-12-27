using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Shared.Exceptions
{
    public class IntegrityException : ApplicationException
    {

        public IntegrityException(string message) : base(message)
        {

        }
    }
}
