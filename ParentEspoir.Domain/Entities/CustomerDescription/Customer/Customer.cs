using ParentEspoir.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class Customer
    {
        public Customer()
        {
            Documents = new HashSet<Document>();
            Notes = new HashSet<Note>();
            WorkshopParticipations = new HashSet<Participant>();
            Objectives = new HashSet<Objective>();
            Volunteerings = new HashSet<Volunteering>();
            CustomerActivations = new HashSet<CustomerActivation>();
        }

        public int CustomerId { get; set; }
        public CustomerDescription CustomerDescription { get; set; }
        public Member Member { get; set; }
        public int FileNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? SuppressionDate { get; set; }
        public DateTime? InscriptionDate { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
        public string NormalizedName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }  
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string SecondaryPhone { get; set; }
        public bool IsDelete { get; set; }


        public SupportGroup SupportGroup { get; set; }
        public ReferenceType ReferenceBy { get; set; }
        public HeardOfUsFrom HeardOfUsFrom { get; set; }

        public ICollection<CustomerActivation> CustomerActivations { get; private set; }
        public ICollection<Document> Documents { get; private set; }
        public ICollection<Note> Notes { get; private set; }
        public ICollection<Participant> WorkshopParticipations { get; private set; }
        public ICollection<Objective> Objectives { get; private set; }
        public ICollection<Volunteering> Volunteerings { get; private set; }
    }
}
