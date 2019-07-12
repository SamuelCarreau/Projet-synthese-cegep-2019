using System;

namespace ParentEspoir.Domain.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreationDate { get; set; }
        DateTime? SuppressionDate { get; set; }
        bool IsDelete { get; set; }
    }
}
