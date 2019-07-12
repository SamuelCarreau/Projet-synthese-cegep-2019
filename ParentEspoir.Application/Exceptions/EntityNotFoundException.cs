using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string name)
        : base($"Entity \"{name}\" was not found.")
        {
        }
    }
}
