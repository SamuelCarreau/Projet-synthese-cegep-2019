using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ParentEspoir.Application
{
    public class GetCustomerDescriptionQueryHandler : IRequestHandler<GetCustomerDescriptionQuery, GetCustomerDescriptionModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetCustomerDescriptionQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<GetCustomerDescriptionModel> Handle(GetCustomerDescriptionQuery request, CancellationToken cancellationToken)
        {
            Customer customer = await _context.Customers
                .Include(c => c.CustomerActivations)
                .Where(c => c.CustomerId == request.CustomerDescriptionId).SingleOrDefaultAsync() ?? throw new EntityNotFoundException(nameof(Customer));

            if (!customer.CustomerActivations.Where(ca => ca.IsActive == true).Any())
            {
                throw new EntityNotFoundException(nameof(Customer));
            }

            CustomerDescription customerDescription = _context.CustomerDescriptions
                .Include(cd => cd.Pregnancy)
                .Include(cd => cd.PersonnalFollowUp)
                .Include(cd => cd.Sex)
                .Include(cd => cd.Parent)
                .Include(cd => cd.MaritalStatus)
                .Include(cd => cd.CitizenStatus)
                .Include(cd => cd.FamilyType)
                .Include(cd => cd.LanguageSpoken)
                .Include(cd => cd.HomeType)
                .Include(cd => cd.TransportType)
                .Include(cd => cd.Schooling)
                .Include(cd => cd.IncomeSource)
                .Include(cd => cd.Availability)
                .Include(cd => cd.YearlyIncome)
                .Include(cd => cd.LegalCustody)
                .Include(cd => cd.PreferedDays)
                .Include(cd => cd.CustomerChildrenAgeBracket)
                    .ThenInclude(x => x.AgeBracket)
                .Include(cd => cd.CustomerSkillToDevelop)
                    .ThenInclude(x => x.Skill)
                .Include(cd => cd.CustomerSocialService)
                    .ThenInclude(x => x.SocialService)
                .Where(cd => cd.CustomerDescriptionId == request.CustomerDescriptionId)
                .SingleOrDefault();

            if (customerDescription == null)
            {
                throw new EntityNotFoundException(nameof(CustomerDescription));
            }

            var model = new GetCustomerDescriptionModel
            {
                CustomerDescriptionId = request.CustomerDescriptionId,
                FileNumber = customer.FileNumber,
                IsPregnant = customerDescription.Pregnancy != null ? true : false,
                PregnancyExpectedDate = customerDescription.Pregnancy?.ChildBirthExpectedDate,
                HasPersonnalFollowUp = customerDescription.PersonnalFollowUp != null ? true : false,
                PersonnalFollowUpMeetingCount = customerDescription.PersonnalFollowUp?.MeetingCount,
                ChildrenCount = customerDescription.ChildrenCount,
                SexId = customerDescription.SexId,
                Sex = customerDescription.Sex?.Name,
                ParentId = customerDescription.ParentId,
                Parent = customerDescription.Parent?.Name,
                MaritalStatusId = customerDescription.MaritalStatusId,
                MaritalStatus = customerDescription.MaritalStatus?.Name,
                CitizenStatusId = customerDescription.CitizenStatusId,
                CitizenStatus = customerDescription.CitizenStatus?.Name,
                FamilyTypeId = customerDescription.FamilyTypeId,
                FamilyType = customerDescription.FamilyType?.Name,
                LanguageSpokenId = customerDescription.LanguageSpokenId,
                LanguageSpoken = customerDescription.LanguageSpoken?.Name,
                HomeTypeId = customerDescription.HomeTypeId,
                HomeType = customerDescription.HomeType?.Name,
                TransportTypeId = customerDescription.TransportTypeId,
                TransportType = customerDescription.TransportType?.Name,
                SchoolingId = customerDescription.SchoolingId,
                Schooling = customerDescription.Schooling?.Name,
                IncomeSourceId = customerDescription.IncomeSourceId,
                IncomeSource = customerDescription.IncomeSource?.Name,
                AvailabilityId = customerDescription.AvailabilityId,
                Availability = customerDescription.Availability?.Name,
                YearlyIncomeId = customerDescription.YearlyIncomeId,
                YearlyIncomeName = customerDescription.YearlyIncome?.Name,
                LegalCustodyId = customerDescription.LegalCustodyId,
                LegalCustody = customerDescription.LegalCustody?.Name,
                WantsToBecomeMember = customerDescription.WantsToBecomeMember,

                HasMentalHealthDiagnostic = customerDescription.HasMentalHealthDiagnostic,
                HasBeenHospitalisedInPsychiatry = customerDescription.HasBeenHospitalisedInPsychiatry,
                HasContactWithDPJnow = customerDescription.HasContactWithDPJnow,
                WillParticipateToHelpingGroup = customerDescription.WillParticipateToHelpingGroup,
                HasContactWithDPJinPast = customerDescription.HasContactWithDPJinPast
            };

            foreach (var day in customerDescription.PreferedDays)
            {
                model.PreferedDays.Add(day.Day);
            }
            foreach (var customerAgeBracket in customerDescription.CustomerChildrenAgeBracket)
            {
                model.CustomerChildrenAgeBracket.Add(customerAgeBracket.AgeBracket.Name);
            }
            foreach (var customerSkill in customerDescription.CustomerSkillToDevelop)
            {
                model.CustomerSkillToDevelop.Add(customerSkill.Skill.Name);
            }
            foreach (var customerSocialService in customerDescription.CustomerSocialService)
            {
                model.CustomerSocialService.Add(customerSocialService.SocialService.Name);
            }

            return model;
        }
    }
}