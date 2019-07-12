using System;

namespace ParentEspoir.Application
{
    public class InvalideNameException : Exception
    {
        public InvalideNameException(string message) : base(message)
        {
            
        }
    }
}
