using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application.Exceptions
{
    public class ErrorSavingChangesException : Exception
    {
        public ErrorSavingChangesException(string message) : base(message) { }
    }
}
