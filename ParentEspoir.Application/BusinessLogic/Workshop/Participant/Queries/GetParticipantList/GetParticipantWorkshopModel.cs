using System;

namespace ParentEspoir.Application
{
    public class GetParticipantWorkshopModel
    {
        public int CustomerId { get; set; }
        public int ParticipantId { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public TimeSpan NbHourLate { get; set; }

        public override bool Equals(object obj)
        {
            return obj is GetParticipantWorkshopModel model &&
                   CustomerId == model.CustomerId &&
                   Name == model.Name &&
                   NbHourLate.Equals(model.NbHourLate);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerId, Name, NbHourLate);
        }
    }
}