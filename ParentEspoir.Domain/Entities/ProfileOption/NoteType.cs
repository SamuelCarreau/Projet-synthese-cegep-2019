using System.Collections;
using System.Collections.Generic;

namespace ParentEspoir.Domain.Entities
{
    public class NoteType : IProfileOption
    {
        public NoteType()
        {
            Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }


        public ICollection<Note> Notes { get; private set; }

    }
}