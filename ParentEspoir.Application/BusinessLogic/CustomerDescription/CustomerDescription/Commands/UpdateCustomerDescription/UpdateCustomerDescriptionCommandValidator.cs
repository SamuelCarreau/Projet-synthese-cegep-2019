using FluentValidation;
using FluentValidation.Validators;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Persistence;
using System;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateCustomerDescriptionCommandValidator : AbstractValidator<UpdateCustomerDescriptionCommand>
    {
        public UpdateCustomerDescriptionCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(cd => cd.CustomerDescriptionId).Must(x => context.Customers.Where(c => c.CustomerId == x && c.IsDelete == false).Any());
            RuleFor(cd => cd.CustomerDescriptionId).Must(x => context.CustomerDescriptions.Where(c => c.CustomerDescriptionId == x).Any());

            RuleFor(cd => cd.PersonnalFollowUpMeetingCount).Must(x => x >= 0).When(cd => cd.HasPersonnalFollowUp)
                .WithMessage(CustomerDescriptionConstant.ERROR_MESSAGE_PREGNANCY);
            RuleFor(cd => cd.PersonnalFollowUpMeetingCount).Must(x => x == null || x >= 0).When(cd => !cd.HasPersonnalFollowUp);

            RuleFor(cd => cd.ChildrenCount).Must(cc => cc >= 0);

            RuleFor(cd => cd.PregnancyExpectedDate).Must(cc => cc > DateTime.Now).When(cd => cd.IsPregnant)
                .WithMessage(CustomerDescriptionConstant.ERROR_MESSAGE_PERSONNAL_FOLLOW_UP);
            RuleFor(cd => cd.PregnancyExpectedDate).Must(cc => cc == null || cc > DateTime.Now).When(cd => !cd.IsPregnant);

            RuleFor(cd => cd.SexId).Must(x => x == null || (context.Sexs.Where(s => s.Id == x && s.IsDelete == false).Any()));
            RuleFor(cd => cd.ParentId).Must(x => x == null || (context.Parents.Where(p => p.Id == x && p.IsDelete == false).Any()));
            RuleFor(cd => cd.MaritalStatusId).Must(x => x == null || (context.MaritalStatuses.Where(ms => ms.Id == x && ms.IsDelete == false).Any()));
            RuleFor(cd => cd.CitizenStatusId).Must(x => x == null || (context.CitizenStatuses.Where(cs => cs.Id == x && cs.IsDelete == false).Any()));
            RuleFor(cd => cd.FamilyTypeId).Must(x => x == null || (context.FamilyTypes.Where(ft => ft.Id == x && ft.IsDelete == false).Any()));
            RuleFor(cd => cd.LanguageSpokenId).Must(x => x == null || (context.Languages.Where(l => l.Id == x && l.IsDelete == false).Any()));
            RuleFor(cd => cd.HomeTypeId).Must(x => x == null || (context.HomeTypes.Where(ht => ht.Id == x && ht.IsDelete == false).Any()));
            RuleFor(cd => cd.TransportTypeId).Must(x => x == null || (context.TransportTypes.Where(tt => tt.Id == x && tt.IsDelete == false).Any()));
            RuleFor(cd => cd.SchoolingId).Must(x => x == null || (context.Schoolings.Where(s => s.Id == x && s.IsDelete == false).Any()));
            RuleFor(cd => cd.IncomeSourceId).Must(x => x == null || (context.IncomeSources.Where(i => i.Id == x && i.IsDelete == false).Any()));
            RuleFor(cd => cd.AvailabilityId).Must(x => x == null || (context.Availabilities.Where(a => a.Id == x && a.IsDelete == false).Any()));
            RuleFor(cd => cd.YearlyIncomeId).Must(x => x == null || (context.YearlyIncomes.Where(yi => yi.Id == x && yi.IsDelete == false).Any()));
            RuleFor(cd => cd.LegalCustodyId).Must(x => x == null || (context.LegalCustodies.Where(lc => lc.Id == x && lc.IsDelete == false).Any()));

            RuleFor(cd => cd.PreferedDays).Must(x => x == null || x.All(pd => (int)pd >= 0 && (int)pd <= 6));
            RuleFor(cd => cd.CustomerChildrenAgeBracket).Must(x => x == null || x.All(id => (context.ChildrenAgeBrackets.Where(cab => cab.Id == id && cab.IsDelete == false).Any())));
            RuleFor(cd => cd.CustomerSkillToDevelop).Must(x => x == null || x.All(id => (context.SkillToDevelops.Where(cab => cab.Id == id && cab.IsDelete == false).Any())));
            RuleFor(cd => cd.CustomerSocialService).Must(x => x == null || x.All(id => (context.SocialServices.Where(cab => cab.Id == id && cab.IsDelete == false).Any())));
        }
    }
}