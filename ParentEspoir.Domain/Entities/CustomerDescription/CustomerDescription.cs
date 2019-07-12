using ParentEspoir.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParentEspoir.Domain.Entities
{
    public class CustomerDescription
    {
        public CustomerDescription()
        {
            CustomerSocialService = new HashSet<CustomerSocialService>();
            PreferedDays = new HashSet<PreferedDay>();
            CustomerSkillToDevelop = new HashSet<CustomerSkillToDevelop>();
            CustomerChildrenAgeBracket = new HashSet<CustomerChildrenAgeBracket>();
        }

        public int CustomerDescriptionId { get; set; }
        
        // one to one relation:
        public Customer Customer { get; set; }
        public Pregnancy Pregnancy { get; set; }
        public PersonnalFollowUp PersonnalFollowUp { get; set; }

        public int? SexId { get; set; }
        public int? ParentId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? CitizenStatusId { get; set; }
        public int? FamilyTypeId { get; set; }
        public int? LanguageSpokenId { get; set; }
        public int? HomeTypeId { get; set; }
        public int? TransportTypeId { get; set; }
        public int? SchoolingId { get; set; }
        public int? IncomeSourceId { get; set; }
        public int? AvailabilityId { get; set; }
        public int? YearlyIncomeId { get; set; }
        public int? LegalCustodyId { get; set; }

        public Sex Sex { get; set; }
        public Parent Parent { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public CitizenStatus CitizenStatus { get; set; }
        public FamilyType FamilyType { get; set; }
        public Language LanguageSpoken { get; set; }
        public HomeType HomeType { get; set; }
        public TransportType TransportType { get ; set; }
        public Schooling Schooling { get; set; }
        public IncomeSource IncomeSource { get; set; }
        public Availability Availability { get; set; }
        public YearlyIncome YearlyIncome { get; set; }
        public LegalCustody LegalCustody { get; set; }

        public bool? WantsToBecomeMember { get; set; }

        public int ChildrenCount { get; set; }

        public bool HasMentalHealthDiagnostic { get; set; }
        public bool HasBeenHospitalisedInPsychiatry { get; set; }
        public bool HasContactWithDPJnow { get; set; }
        public bool WillParticipateToHelpingGroup { get; set; }
        public bool HasContactWithDPJinPast { get; set; }

        public ICollection<CustomerSocialService> CustomerSocialService { get; private set; }
        public ICollection<PreferedDay> PreferedDays { get; private set; }
        public ICollection<CustomerSkillToDevelop> CustomerSkillToDevelop { get; private set; }
        public ICollection<CustomerChildrenAgeBracket> CustomerChildrenAgeBracket { get; private set; }

    }
}
