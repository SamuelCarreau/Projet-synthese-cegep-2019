using ParentEspoir.Domain.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace ParentEspoir.Application.Test
{
    public class CustomerDescriptionGetTest : CustomerDescriptionTestBase
    {
        [Fact]
        public void GetTest_Success_NoNullValueAnFullList()
        {
            Customer customer = CreateCustomer("NoNullValueAnFullList");
            int id = customer.CustomerId;
            DateTime pregnanceDate = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            CustomerDescription customerDescription = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnanceDate },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(1),
                ChildrenCount = 3,
                Sex = _context.Sexs.Find(1),
                Parent = _context.Parents.Find(1),
                MaritalStatus = _context.MaritalStatuses.Find(3),
                CitizenStatus = _context.CitizenStatuses.Find(2),
                FamilyType = _context.FamilyTypes.Find(1),
                LanguageSpoken = _context.Languages.Find(1),
                HomeType = _context.HomeTypes.Find(4),
                TransportType = _context.TransportTypes.Find(2),
                Schooling = _context.Schoolings.Find(2),
                IncomeSource = _context.IncomeSources.Find(1),
                Availability = _context.Availabilities.Find(3),
                YearlyIncome = _context.YearlyIncomes.Find(2),
                WantsToBecomeMember = true,
                HasMentalHealthDiagnostic = false,
                HasBeenHospitalisedInPsychiatry = true,
                HasContactWithDPJinPast = true,
                HasContactWithDPJnow = false,
                WillParticipateToHelpingGroup = true,
            };

            customerDescription.PreferedDays.Add(new PreferedDay { CustomerDescription = customerDescription, Day = DayOfWeek.Friday, IsDelete = false });
            customerDescription.PreferedDays.Add(new PreferedDay { CustomerDescription = customerDescription, Day = DayOfWeek.Saturday, IsDelete = false });

            customerDescription.CustomerSkillToDevelop.Add(new CustomerSkillToDevelop { Customer = customerDescription, SkillId = 1, IsDelete = false });
            customerDescription.CustomerSkillToDevelop.Add(new CustomerSkillToDevelop { Customer = customerDescription, SkillId = 3, IsDelete = false });

            customerDescription.CustomerChildrenAgeBracket.Add(new CustomerChildrenAgeBracket { Customer = customerDescription, AgeBracketId = 1, IsDelete = false });
            customerDescription.CustomerChildrenAgeBracket.Add(new CustomerChildrenAgeBracket { Customer = customerDescription, AgeBracketId = 3, IsDelete = false });

            customerDescription.CustomerSocialService.Add(new CustomerSocialService { Customer = customerDescription, SocialServiceId = 2, IsDelete = false });
            customerDescription.CustomerSocialService.Add(new CustomerSocialService { Customer = customerDescription, SocialServiceId = 4, IsDelete = false });

            _context.Add(customerDescription);
            _context.SaveChanges();

            var response = _mediator.Send(new GetCustomerDescriptionQuery { CustomerDescriptionId = id }).Result;

            response.ShouldBeOfType(typeof(GetCustomerDescriptionModel));
            response.CustomerDescriptionId.ShouldBe(id);

            response.PregnancyExpectedDate.ShouldBe(pregnanceDate);
            response.PersonnalFollowUpMeetingCount.ShouldBe(5);
            response.LegalCustody.ShouldBe(_context.LegalCustodies.Find(1).Name);
            response.ChildrenCount.ShouldBe(3);
            response.Sex.ShouldBe(_context.Sexs.Find(1).Name);
            response.Parent.ShouldBe(_context.Parents.Find(1).Name);
            response.MaritalStatus.ShouldBe(_context.MaritalStatuses.Find(3).Name);
            response.CitizenStatus.ShouldBe(_context.CitizenStatuses.Find(2).Name);
            response.FamilyType.ShouldBe(_context.FamilyTypes.Find(1).Name);
            response.LanguageSpoken.ShouldBe(_context.Languages.Find(1).Name);
            response.HomeType.ShouldBe(_context.HomeTypes.Find(4).Name);
            response.TransportType.ShouldBe(_context.TransportTypes.Find(2).Name);
            response.Schooling.ShouldBe(_context.Schoolings.Find(2).Name);
            response.IncomeSource.ShouldBe(_context.IncomeSources.Find(1).Name);
            response.Availability.ShouldBe(_context.Availabilities.Find(3).Name);
            response.YearlyIncomeName.ShouldBe(_context.YearlyIncomes.Find(2).Name);

            response.WantsToBecomeMember.ShouldBe(true);
            response.HasMentalHealthDiagnostic.ShouldBe(false);
            response.HasBeenHospitalisedInPsychiatry.ShouldBe(true);
            response.HasContactWithDPJinPast.ShouldBe(true);
            response.HasContactWithDPJnow.ShouldBe(false);
            response.WillParticipateToHelpingGroup.ShouldBe(true);

            response.PreferedDays.ShouldBeOfType(typeof(List<DayOfWeek>));
            response.PreferedDays.Count().ShouldBe(2);
            response.PreferedDays.Where(pd => pd.ToString().Equals("Friday")).Any().ShouldBe(true);
            response.PreferedDays.Where(pd => pd.ToString().Equals("Saturday")).Any().ShouldBe(true);

            response.CustomerSkillToDevelop.ShouldBeOfType(typeof(List<string>));
            response.CustomerSkillToDevelop.Count().ShouldBe(2);
            response.CustomerSkillToDevelop.Where(csd => csd.Equals(_context.SkillToDevelops.Find(1).Name)).Any().ShouldBe(true);
            response.CustomerSkillToDevelop.Where(csd => csd.Equals(_context.SkillToDevelops.Find(3).Name)).Any().ShouldBe(true);

            response.CustomerChildrenAgeBracket.ShouldBeOfType(typeof(List<string>));
            response.CustomerChildrenAgeBracket.Count().ShouldBe(2);
            response.CustomerChildrenAgeBracket.Where(ccab => ccab.Equals(_context.ChildrenAgeBrackets.Find(1).Name)).Any().ShouldBe(true);
            response.CustomerChildrenAgeBracket.Where(ccab => ccab.Equals(_context.ChildrenAgeBrackets.Find(3).Name)).Any().ShouldBe(true);

            response.CustomerSocialService.ShouldBeOfType(typeof(List<string>));
            response.CustomerSocialService.Count().ShouldBe(2);
            response.CustomerSocialService.Where(css => css.Equals(_context.SocialServices.Find(2).Name)).Any().ShouldBe(true);
            response.CustomerSocialService.Where(css => css.Equals(_context.SocialServices.Find(4).Name)).Any().ShouldBe(true);
        }

        [Fact]
        public void GetTest_Success_AllNullAndEmptyList()
        {
            Customer customer = CreateCustomer("AllNullAndEmptyList");
            int id = customer.CustomerId;

            CustomerDescription customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = null,
                PersonnalFollowUp = null,
                LegalCustody = null,
                ChildrenCount = 3,
                Sex = null,
                Parent = null,
                MaritalStatus = null,
                CitizenStatus = null,
                FamilyType = null,
                LanguageSpoken = null,
                HomeType = null,
                TransportType = null,
                Schooling = null,
                IncomeSource = null,
                Availability = null,
                YearlyIncome = null,
                WantsToBecomeMember = null,
                HasMentalHealthDiagnostic = false,
                HasBeenHospitalisedInPsychiatry = true,
                HasContactWithDPJinPast = true,
                HasContactWithDPJnow = false,
                WillParticipateToHelpingGroup = true,
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            var response = _mediator.Send(new GetCustomerDescriptionQuery { CustomerDescriptionId = id }).Result;

            response.ShouldBeOfType(typeof(GetCustomerDescriptionModel));
            response.CustomerDescriptionId.ShouldBe(id);

            response.PregnancyExpectedDate.ShouldBe(null);
            response.PersonnalFollowUpMeetingCount.ShouldBe(null);
            response.LegalCustody.ShouldBe(null);
            response.ChildrenCount.ShouldBe(3);
            response.Sex.ShouldBe(null);
            response.Parent.ShouldBe(null);
            response.MaritalStatus.ShouldBe(null);
            response.CitizenStatus.ShouldBe(null);
            response.FamilyType.ShouldBe(null);
            response.LanguageSpoken.ShouldBe(null);
            response.HomeType.ShouldBe(null);
            response.TransportType.ShouldBe(null);
            response.Schooling.ShouldBe(null);
            response.IncomeSource.ShouldBe(null);
            response.Availability.ShouldBe(null);
            response.YearlyIncomeName.ShouldBe(null);

            response.WantsToBecomeMember.ShouldBe(null);
            response.HasMentalHealthDiagnostic.ShouldBe(false);
            response.HasBeenHospitalisedInPsychiatry.ShouldBe(true);
            response.HasContactWithDPJinPast.ShouldBe(true);
            response.HasContactWithDPJnow.ShouldBe(false);
            response.WillParticipateToHelpingGroup.ShouldBe(true);

            response.PreferedDays.ShouldBeOfType(typeof(List<DayOfWeek>));
            response.PreferedDays.Count().ShouldBe(0);

            response.CustomerSkillToDevelop.ShouldBeOfType(typeof(List<string>));
            response.CustomerSkillToDevelop.Count().ShouldBe(0);

            response.CustomerChildrenAgeBracket.ShouldBeOfType(typeof(List<string>));
            response.CustomerChildrenAgeBracket.Count().ShouldBe(0);

            response.CustomerSocialService.ShouldBeOfType(typeof(List<string>));
            response.CustomerSocialService.Count().ShouldBe(0);
        }

        [Fact]
        public void GetTest_Success_SomeNull()
        {
            Customer customer = CreateCustomer("SomeNull");
            int id = customer.CustomerId;
            DateTime pregnanceDate = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            CustomerDescription customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnanceDate },
                PersonnalFollowUp = null,
                LegalCustody = _context.LegalCustodies.Find(1),
                ChildrenCount = 3,
                Sex = _context.Sexs.Find(1),
                Parent = _context.Parents.Find(1),
                MaritalStatus = null,
                CitizenStatus = _context.CitizenStatuses.Find(2),
                FamilyType = _context.FamilyTypes.Find(1),
                LanguageSpoken = _context.Languages.Find(1),
                HomeType = _context.HomeTypes.Find(4),
                TransportType = null,
                Schooling = _context.Schoolings.Find(2),
                IncomeSource = _context.IncomeSources.Find(1),
                Availability = _context.Availabilities.Find(3),
                YearlyIncome = _context.YearlyIncomes.Find(2),
                WantsToBecomeMember = null,
                HasMentalHealthDiagnostic = false,
                HasBeenHospitalisedInPsychiatry = true,
                HasContactWithDPJinPast = true,
                HasContactWithDPJnow = false,
                WillParticipateToHelpingGroup = true,
            };

            customerDescriptionExpected.PreferedDays.Add(new PreferedDay { CustomerDescription = customerDescriptionExpected, Day = DayOfWeek.Friday, IsDelete = false });
            customerDescriptionExpected.PreferedDays.Add(new PreferedDay { CustomerDescription = customerDescriptionExpected, Day = DayOfWeek.Saturday, IsDelete = false });

            customerDescriptionExpected.CustomerChildrenAgeBracket.Add(new CustomerChildrenAgeBracket { Customer = customerDescriptionExpected, AgeBracketId = 1, IsDelete = false });
            customerDescriptionExpected.CustomerChildrenAgeBracket.Add(new CustomerChildrenAgeBracket { Customer = customerDescriptionExpected, AgeBracketId = 3, IsDelete = false });

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            var response = _mediator.Send(new GetCustomerDescriptionQuery { CustomerDescriptionId = id }).Result;

            response.ShouldBeOfType(typeof(GetCustomerDescriptionModel));
            response.CustomerDescriptionId.ShouldBe(id);

            response.PregnancyExpectedDate.ShouldBe(pregnanceDate);
            response.PersonnalFollowUpMeetingCount.ShouldBe(null);
            response.LegalCustody.ShouldBe(_context.LegalCustodies.Find(1).Name);
            response.ChildrenCount.ShouldBe(3);
            response.Sex.ShouldBe(_context.Sexs.Find(1).Name);
            response.Parent.ShouldBe(_context.Parents.Find(1).Name);
            response.MaritalStatus.ShouldBe(null);
            response.CitizenStatus.ShouldBe(_context.CitizenStatuses.Find(2).Name);
            response.FamilyType.ShouldBe(_context.FamilyTypes.Find(1).Name);
            response.LanguageSpoken.ShouldBe(_context.Languages.Find(1).Name);
            response.HomeType.ShouldBe(_context.HomeTypes.Find(4).Name);
            response.TransportType.ShouldBe(null);
            response.Schooling.ShouldBe(_context.Schoolings.Find(2).Name);
            response.IncomeSource.ShouldBe(_context.IncomeSources.Find(1).Name);
            response.Availability.ShouldBe(_context.Availabilities.Find(3).Name);
            response.YearlyIncomeName.ShouldBe(_context.YearlyIncomes.Find(2).Name);

            response.WantsToBecomeMember.ShouldBe(null);
            response.HasMentalHealthDiagnostic.ShouldBe(false);
            response.HasBeenHospitalisedInPsychiatry.ShouldBe(true);
            response.HasContactWithDPJinPast.ShouldBe(true);
            response.HasContactWithDPJnow.ShouldBe(false);
            response.WillParticipateToHelpingGroup.ShouldBe(true);

            response.PreferedDays.ShouldBeOfType(typeof(List<DayOfWeek>));
            response.PreferedDays.Count().ShouldBe(2);
            response.PreferedDays.Where(pd => pd.ToString().Equals("Friday")).Any().ShouldBe(true);
            response.PreferedDays.Where(pd => pd.ToString().Equals("Saturday")).Any().ShouldBe(true);

            response.CustomerSkillToDevelop.ShouldBeOfType(typeof(List<string>));
            response.CustomerSkillToDevelop.Count().ShouldBe(0);

            response.CustomerChildrenAgeBracket.ShouldBeOfType(typeof(List<string>));
            response.CustomerChildrenAgeBracket.Count().ShouldBe(2);
            response.CustomerChildrenAgeBracket.Where(ccab => ccab.Equals(_context.ChildrenAgeBrackets.Find(1).Name)).Any().ShouldBe(true);
            response.CustomerChildrenAgeBracket.Where(ccab => ccab.Equals(_context.ChildrenAgeBrackets.Find(3).Name)).Any().ShouldBe(true);

            response.CustomerSocialService.ShouldBeOfType(typeof(List<string>));
            response.CustomerSocialService.Count().ShouldBe(0);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void GetTest_Exception_CustomerDontExist(int id)
        {
            var response = _mediator.Send(new GetCustomerDescriptionQuery { CustomerDescriptionId = id }).ShouldThrow(typeof(EntityNotFoundException));
        }

        [Fact]
        public void GetTest_Exception_CustomerDescriptionDontExist()
        {
            Customer customer = CreateCustomer("CustomerDescriptionDontExist");
            int id = customer.CustomerId;

            var response = _mediator.Send(new GetCustomerDescriptionQuery { CustomerDescriptionId = id }).ShouldThrow(typeof(EntityNotFoundException));
        }

        [Fact]
        public void GetTest_Excpetion_CustomerExistButIsDelete()
        {
            Customer customer = CreateCustomer("CustomerDescriptionDontExist");
            int id = customer.CustomerId;

            CustomerDescription customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = DateTime.Now + new TimeSpan(150, 0, 0, 0) },
                PersonnalFollowUp = null,
                LegalCustody = _context.LegalCustodies.Find(1),
                ChildrenCount = 3,
                Sex = _context.Sexs.Find(1),
                Parent = _context.Parents.Find(1),
                MaritalStatus = null,
                CitizenStatus = _context.CitizenStatuses.Find(2),
                FamilyType = _context.FamilyTypes.Find(1),
                LanguageSpoken = _context.Languages.Find(1),
                HomeType = _context.HomeTypes.Find(4),
                TransportType = null,
                Schooling = _context.Schoolings.Find(2),
                IncomeSource = _context.IncomeSources.Find(1),
                Availability = _context.Availabilities.Find(3),
                YearlyIncome = _context.YearlyIncomes.Find(2),
                WantsToBecomeMember = null,
                HasMentalHealthDiagnostic = false,
                HasBeenHospitalisedInPsychiatry = true,
                HasContactWithDPJinPast = true,
                HasContactWithDPJnow = false,
                WillParticipateToHelpingGroup = true,
            };

            var activation = customer.CustomerActivations.Where(ca => ca.IsActive == true).SingleOrDefault();
            activation.IsActive = false;
            _context.Update(customer);
            _context.SaveChanges();

            var response = _mediator.Send(new GetCustomerDescriptionQuery { CustomerDescriptionId = id }).ShouldThrow(typeof(EntityNotFoundException));
        }
    }
}
