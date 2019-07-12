using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class EntityAlreadyExistException : Exception
    {
        public EntityAlreadyExistException(string name)
        : base($"The Entity \"{name}\" already exist.")
        {
        }
    }
}
