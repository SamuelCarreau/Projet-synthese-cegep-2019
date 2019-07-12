using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application.Exceptions
{
    public class UserOperationException : Exception
    {
        public UserOperationException(string message) : base(message) { }
    }
}
