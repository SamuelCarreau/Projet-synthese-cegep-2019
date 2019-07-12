using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using System;

namespace ParentEspoir.Application
{
    public class UpdateCustomerDescriptionCommand : IRequest
    {
        public UpdateCustomerDescriptionCommand()
        {
            PreferedDays = new List<DayOfWeek>();
            CustomerChildrenAgeBracket = new List<int>();
            CustomerSkillToDevelop = new List<int>();
            CustomerSocialService = new List<int>();
        }

        public int CustomerDescriptionId { get; set; }

        public bool IsPregnant { get; set; }
        public DateTime? PregnancyExpectedDate  { get; set; }

        public bool HasPersonnalFollowUp { get; set; }
        public int? PersonnalFollowUpMeetingCount { get; set; }

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
        public bool? WantsToBecomeMember { get; set; }

        public int ChildrenCount { get; set; }

        public bool HasMentalHealthDiagnostic { get; set; }
        public bool HasBeenHospitalisedInPsychiatry { get; set; }
        public bool HasContactWithDPJnow { get; set; }
        public bool WillParticipateToHelpingGroup { get; set; }
        public bool HasContactWithDPJinPast { get; set; }

        public ICollection<int> CustomerSocialService { get; set; }
        public ICollection<DayOfWeek> PreferedDays { get; set; }
        public ICollection<int> CustomerSkillToDevelop { get; set; }
        public ICollection<int> CustomerChildrenAgeBracket { get; set; }
    }
}