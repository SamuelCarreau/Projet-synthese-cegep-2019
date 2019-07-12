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
    public class UpdateCustomerDescriptionCommandHandler : IRequestHandler<UpdateCustomerDescriptionCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateCustomerDescriptionCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCustomerDescriptionCommand request, CancellationToken cancellationToken)
        {
            CustomerDescription customerDescription = await _context.CustomerDescriptions
                .Include(cd => cd.Customer)
                .Include(cd => cd.Pregnancy)
                .Include(cd => cd.PersonnalFollowUp)
                .Include(cd => cd.CustomerChildrenAgeBracket)
                .Include(cd => cd.CustomerSkillToDevelop)
                .Include(cd => cd.CustomerSocialService)
                .Include(cd => cd.PreferedDays)
                .SingleOrDefaultAsync(cd => cd.CustomerDescriptionId == request.CustomerDescriptionId);

            if (request.PregnancyExpectedDate != null)
            {
                if (customerDescription.Pregnancy == null)
                {
                    customerDescription.Pregnancy = new Pregnancy { CustomerDescription = customerDescription, ChildBirthExpectedDate = (DateTime)request.PregnancyExpectedDate, IsDelete = false };
                }
                else
                {
                    customerDescription.Pregnancy.ChildBirthExpectedDate = (DateTime)request.PregnancyExpectedDate;
                }
            }
            else
            {
                customerDescription.Pregnancy = null;
            }

            if (request.PersonnalFollowUpMeetingCount != null)
            {
                if (customerDescription.PersonnalFollowUp == null)
                {
                    customerDescription.PersonnalFollowUp = new PersonnalFollowUp { CustomerDescription = customerDescription, MeetingCount = (int)request.PersonnalFollowUpMeetingCount, IsDelete = false };
                }
                else
                {
                    customerDescription.PersonnalFollowUp.MeetingCount = (int)request.PersonnalFollowUpMeetingCount;
                }
            }
            else
            {
                customerDescription.PersonnalFollowUp = null;
            }

            if (request.LegalCustodyId != null)
            {
                customerDescription.LegalCustody = await _context.LegalCustodies.FindAsync(request.LegalCustodyId);
            }
            else
            {
                customerDescription.LegalCustody = null;
            }

            customerDescription.ChildrenCount = request.ChildrenCount;

            customerDescription.Sex = (request.SexId == null) ? null : await _context.Sexs.FindAsync(request.SexId);
            customerDescription.Parent = (request.ParentId == null) ? null : await _context.Parents.FindAsync(request.ParentId);
            customerDescription.MaritalStatus = (request.MaritalStatusId == null) ? null : await _context.MaritalStatuses.FindAsync(request.MaritalStatusId);
            customerDescription.CitizenStatus = (request.CitizenStatusId == null) ? null : await _context.CitizenStatuses.FindAsync(request.CitizenStatusId);
            customerDescription.FamilyType = (request.FamilyTypeId == null) ? null : await _context.FamilyTypes.FindAsync(request.FamilyTypeId);
            customerDescription.LanguageSpoken = (request.LanguageSpokenId == null) ? null : await _context.Languages.FindAsync(request.LanguageSpokenId);
            customerDescription.HomeType = (request.HomeTypeId == null) ? null : await _context.HomeTypes.FindAsync(request.HomeTypeId);
            customerDescription.TransportType = (request.TransportTypeId == null) ? null : await _context.TransportTypes.FindAsync(request.TransportTypeId);
            customerDescription.Schooling = (request.SchoolingId == null) ? null : await _context.Schoolings.FindAsync(request.SchoolingId);
            customerDescription.IncomeSource = (request.IncomeSourceId == null) ? null : await _context.IncomeSources.FindAsync(request.IncomeSourceId);
            customerDescription.Availability = (request.AvailabilityId == null) ? null : await _context.Availabilities.FindAsync(request.AvailabilityId);
            customerDescription.YearlyIncome = (request.YearlyIncomeId == null) ? null : await _context.YearlyIncomes.FindAsync(request.YearlyIncomeId);

            customerDescription.WantsToBecomeMember = (request.WantsToBecomeMember == null) ? null : request.WantsToBecomeMember;
            customerDescription.HasMentalHealthDiagnostic = request.HasMentalHealthDiagnostic;
            customerDescription.HasBeenHospitalisedInPsychiatry = request.HasBeenHospitalisedInPsychiatry;
            customerDescription.HasContactWithDPJinPast = request.HasContactWithDPJinPast;
            customerDescription.HasContactWithDPJnow = request.HasContactWithDPJnow;
            customerDescription.WillParticipateToHelpingGroup = request.WillParticipateToHelpingGroup;

            customerDescription.PreferedDays.Clear();
            if (request.PreferedDays != null)
            {
                foreach (var day in request.PreferedDays)
                {
                    customerDescription.PreferedDays.Add(new PreferedDay { CustomerDescription = customerDescription, Day = day, IsDelete = false });
                }
            }

            customerDescription.CustomerSkillToDevelop.Clear();
            if (request.CustomerSkillToDevelop != null)
            {
                foreach (var skillId in request.CustomerSkillToDevelop)
                {
                    SkillToDevelop skillToDevelop = await _context.SkillToDevelops.FindAsync(skillId);
                    customerDescription.CustomerSkillToDevelop.Add(new CustomerSkillToDevelop { Customer = customerDescription, Skill = skillToDevelop, IsDelete = false });
                }
            }

            customerDescription.CustomerChildrenAgeBracket.Clear();
            if (request.CustomerChildrenAgeBracket != null)
            {
                foreach (var childrenAgeBracketId in request.CustomerChildrenAgeBracket)
                {
                    ChildrenAgeBracket childrenAgeBracket = await _context.ChildrenAgeBrackets.FindAsync(childrenAgeBracketId);
                    customerDescription.CustomerChildrenAgeBracket.Add(new CustomerChildrenAgeBracket { Customer = customerDescription, AgeBracket = childrenAgeBracket, IsDelete = false });
                }
            }

            customerDescription.CustomerSocialService.Clear();
            if (request.CustomerSocialService != null)
            {
                foreach (var socialServicesId in request.CustomerSocialService)
                {
                    SocialService socialService = await _context.SocialServices.FindAsync(socialServicesId);
                    customerDescription.CustomerSocialService.Add(new CustomerSocialService { Customer = customerDescription, SocialService = socialService, IsDelete = false });
                }
            }

            _context.CustomerDescriptions.Update(customerDescription);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}