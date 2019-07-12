using System;
using System.Collections.Generic;
using System.ComponentModel;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class GetCustomerDescriptionModel
    {
        public GetCustomerDescriptionModel()
        {
            PreferedDays = new List<DayOfWeek>();
            CustomerChildrenAgeBracket = new List<string>();
            CustomerSkillToDevelop = new List<string>();
            CustomerSocialService = new List<string>();
        }

        public int CustomerDescriptionId { get; set; }
        public int FileNumber { get; set; }

        public bool IsPregnant { get; set; }
        public DateTime? PregnancyExpectedDate { get; set; }

        public bool HasPersonnalFollowUp { get; set; }
        public int? PersonnalFollowUpMeetingCount { get; set; }

        public int? SexId { get; set; }
        public string Sex { get; set; }

        public int? ParentId { get; set; }
        public string Parent { get; set; }

        public int? MaritalStatusId { get; set; }
        public string MaritalStatus { get; set; }

        public int? CitizenStatusId { get; set; }
        public string CitizenStatus { get; set; }

        public int? FamilyTypeId { get; set; }
        public string FamilyType { get; set; }

        public int? LanguageSpokenId { get; set; }
        public string LanguageSpoken { get; set; }

        public int? HomeTypeId { get; set; }
        public string HomeType { get; set; }

        public int? TransportTypeId { get; set; }
        public string TransportType { get; set; }

        public int? SchoolingId { get; set; }
        public string Schooling { get; set; }

        public int? IncomeSourceId { get; set; }
        public string IncomeSource { get; set; }

        public int? AvailabilityId { get; set; }
        public string Availability { get; set; }

        public int? YearlyIncomeId { get; set; }
        public string YearlyIncomeName { get; set; }

        public int? LegalCustodyId { get; set; }
        public string LegalCustody { get; set; }

        public bool? WantsToBecomeMember { get; set; }
        public int ChildrenCount { get; set; }

        public bool HasMentalHealthDiagnostic { get; set; }
        public bool HasBeenHospitalisedInPsychiatry { get; set; }
        public bool HasContactWithDPJnow { get; set; }
        public bool WillParticipateToHelpingGroup { get; set; }
        public bool HasContactWithDPJinPast { get; set; }

        public ICollection<string> CustomerSocialService { get; private set; }
        public ICollection<DayOfWeek> PreferedDays { get; private set; }
        public ICollection<string> CustomerSkillToDevelop { get; private set; }
        public ICollection<string> CustomerChildrenAgeBracket { get; private set; }
    }
}