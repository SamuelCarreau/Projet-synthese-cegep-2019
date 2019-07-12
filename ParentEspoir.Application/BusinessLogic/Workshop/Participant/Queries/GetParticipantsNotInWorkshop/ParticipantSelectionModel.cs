using System;
using System.Collections.Generic;

namespace ParentEspoir.Application
{
    public class ParticipantSelectionModel: IComparable<ParticipantSelectionModel>
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public int CompareTo(ParticipantSelectionModel other)
        {
            return Name.CompareTo(other.Name);
        }

        public override bool Equals(object obj)
        {
            return obj is ParticipantSelectionModel model &&
                   CustomerId == model.CustomerId &&
                   Name == model.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerId, Name);
        }
    }
}
