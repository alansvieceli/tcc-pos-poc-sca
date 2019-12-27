using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Shared.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {

        public DbConcurrencyException(string message) : base(message)
        {

        }
    }
}
