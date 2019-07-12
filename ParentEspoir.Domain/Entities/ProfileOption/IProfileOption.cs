using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public interface IProfileOption
    {
        int Id { get; set; }
        string Name { get; set; }
        bool IsDelete { get; set; }
    }
}
