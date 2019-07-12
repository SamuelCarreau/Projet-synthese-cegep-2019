using MediatR;
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
    public class CustomerDescriptionUpdateTest : CustomerDescriptionTestBase
    {
        [Fact]
        public void UpdateTest_Success_AllValueChanged()
        {
            Customer customer = CreateCustomer("AllValuechange");
            int id = customer.CustomerId;

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = null,
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            customerDescriptionExpected.PreferedDays.Add(new PreferedDay { CustomerDescription = customerDescriptionExpected, Day = DayOfWeek.Friday, IsDelete = false });
            customerDescriptionExpected.PreferedDays.Add(new PreferedDay { CustomerDescription = customerDescriptionExpected, Day = DayOfWeek.Saturday, IsDelete = false });

            customerDescriptionExpected.CustomerSkillToDevelop.Add(new CustomerSkillToDevelop { Customer = customerDescriptionExpected, SkillId = 1, IsDelete = false });
            customerDescriptionExpected.CustomerSkillToDevelop.Add(new CustomerSkillToDevelop { Customer = customerDescriptionExpected, SkillId = 3, IsDelete = false });

            customerDescriptionExpected.CustomerChildrenAgeBracket.Add(new CustomerChildrenAgeBracket { Customer = customerDescriptionExpected, AgeBracketId = 1, IsDelete = false });
            customerDescriptionExpected.CustomerChildrenAgeBracket.Add(new CustomerChildrenAgeBracket { Customer = customerDescriptionExpected, AgeBracketId = 3, IsDelete = false });

            customerDescriptionExpected.CustomerSocialService.Add(new CustomerSocialService { Customer = customerDescriptionExpected, SocialServiceId = 2, IsDelete = false });
            customerDescriptionExpected.CustomerSocialService.Add(new CustomerSocialService { Customer = customerDescriptionExpected, SocialServiceId = 4, IsDelete = false });

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            DateTime? pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = id,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 2,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 1,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 1,
                IncomeSourceId = 2,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).Result;

            response.ShouldBeOfType(typeof(Unit));

            customerDescriptionExpected.Pregnancy.ShouldNotBe(null);
            customerDescriptionExpected.Pregnancy.ChildBirthExpectedDate.ShouldBe((DateTime)pregnancyDateTime);
            customerDescriptionExpected.PersonnalFollowUp.MeetingCount.ShouldBe(6);
            customerDescriptionExpected.LegalCustody.ShouldBe(null);
            customerDescriptionExpected.ChildrenCount.ShouldBe(5);
            customerDescriptionExpected.Sex.Id.ShouldBe(2);
            customerDescriptionExpected.Parent.Id.ShouldBe(2);
            customerDescriptionExpected.MaritalStatus.Id.ShouldBe(2);
            customerDescriptionExpected.CitizenStatus.Id.ShouldBe(1);
            customerDescriptionExpected.FamilyType.Id.ShouldBe(3);
            customerDescriptionExpected.LanguageSpoken.Id.ShouldBe(4);
            customerDescriptionExpected.HomeType.Id.ShouldBe(2);
            customerDescriptionExpected.TransportType.Id.ShouldBe(1);
            customerDescriptionExpected.Schooling.Id.ShouldBe(1);
            customerDescriptionExpected.IncomeSource.Id.ShouldBe(2);
            customerDescriptionExpected.Availability.Id.ShouldBe(4);
            customerDescriptionExpected.YearlyIncome.Id.ShouldBe(1);

            customerDescriptionExpected.WantsToBecomeMember.ShouldBe(false);
            customerDescriptionExpected.HasMentalHealthDiagnostic.ShouldBe(true);
            customerDescriptionExpected.HasBeenHospitalisedInPsychiatry.ShouldBe(false);
            customerDescriptionExpected.HasContactWithDPJinPast.ShouldBe(false);
            customerDescriptionExpected.HasContactWithDPJnow.ShouldBe(true);
            customerDescriptionExpected.WillParticipateToHelpingGroup.ShouldBe(false);

            _context.PreferedDays.Where(pd => pd.CustomerDescriptionID == customer.CustomerId).Count().ShouldBe(3);
            customerDescriptionExpected.PreferedDays.Count().ShouldBe(3);
            customerDescriptionExpected.PreferedDays.Where(pd => pd.Day == DayOfWeek.Tuesday && pd.IsDelete == false).Any().ShouldBe(true);
            customerDescriptionExpected.PreferedDays.Where(pd => pd.Day == DayOfWeek.Wednesday && pd.IsDelete == false).Any().ShouldBe(true);
            customerDescriptionExpected.PreferedDays.Where(pd => pd.Day == DayOfWeek.Thursday && pd.IsDelete == false).Any().ShouldBe(true);

            _context.CustomerSkillToDevelops.Where(cstd => cstd.CustomerId == customer.CustomerId).Count().ShouldBe(1);
            customerDescriptionExpected.CustomerSkillToDevelop.Count().ShouldBe(1);
            customerDescriptionExpected.CustomerSkillToDevelop.Where(csd => csd.SkillId == 2).Any().ShouldBe(true);

            _context.CustomerChildrenAgeBrackets.Where(ccab => ccab.CustomerId == customer.CustomerId).Count().ShouldBe(2);
            customerDescriptionExpected.CustomerChildrenAgeBracket.Count().ShouldBe(2);
            customerDescriptionExpected.CustomerChildrenAgeBracket.Where(ccab => ccab.AgeBracketId == 2).Any().ShouldBe(true);
            customerDescriptionExpected.CustomerChildrenAgeBracket.Where(ccab => ccab.AgeBracketId == 4).Any().ShouldBe(true);

            _context.CustomerSocialServices.Where(css => css.CustomerId == customer.CustomerId).Count().ShouldBe(2);
            customerDescriptionExpected.CustomerSocialService.Count().ShouldBe(2);
            customerDescriptionExpected.CustomerSocialService.Where(css => css.SocialServiceId == 1).Any().ShouldBe(true);
            customerDescriptionExpected.CustomerSocialService.Where(css => css.SocialServiceId == 3).Any().ShouldBe(true);
        }

        [Fact]
        public void UpdateTest_Success_AllNullToNotNull()
        {
            Customer customer = CreateCustomer("AllNullToNotNull");
            int id = customer.CustomerId;

            var customerDescriptionExpected = new CustomerDescription
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            DateTime? pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = id,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = 2,
                ChildrenCount = 5,
                SexId = 2,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 1,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 1,
                IncomeSourceId = 2,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).Result;

            response.ShouldBeOfType(typeof(Unit));

            customerDescriptionExpected.Pregnancy.ShouldNotBe(null);
            customerDescriptionExpected.Pregnancy.ChildBirthExpectedDate.ShouldBe((DateTime)pregnancyDateTime);
            customerDescriptionExpected.PersonnalFollowUp.MeetingCount.ShouldBe(6);
            customerDescriptionExpected.LegalCustody.Id.ShouldBe(2);
            customerDescriptionExpected.ChildrenCount.ShouldBe(5);
            customerDescriptionExpected.Sex.Id.ShouldBe(2);
            customerDescriptionExpected.Parent.Id.ShouldBe(2);
            customerDescriptionExpected.MaritalStatus.Id.ShouldBe(2);
            customerDescriptionExpected.CitizenStatus.Id.ShouldBe(1);
            customerDescriptionExpected.FamilyType.Id.ShouldBe(3);
            customerDescriptionExpected.LanguageSpoken.Id.ShouldBe(4);
            customerDescriptionExpected.HomeType.Id.ShouldBe(2);
            customerDescriptionExpected.TransportType.Id.ShouldBe(1);
            customerDescriptionExpected.Schooling.Id.ShouldBe(1);
            customerDescriptionExpected.IncomeSource.Id.ShouldBe(2);
            customerDescriptionExpected.Availability.Id.ShouldBe(4);
            customerDescriptionExpected.YearlyIncome.Id.ShouldBe(1);

            customerDescriptionExpected.WantsToBecomeMember.ShouldBe(false);
            customerDescriptionExpected.HasMentalHealthDiagnostic.ShouldBe(true);
            customerDescriptionExpected.HasBeenHospitalisedInPsychiatry.ShouldBe(false);
            customerDescriptionExpected.HasContactWithDPJinPast.ShouldBe(false);
            customerDescriptionExpected.HasContactWithDPJnow.ShouldBe(true);
            customerDescriptionExpected.WillParticipateToHelpingGroup.ShouldBe(false);

            _context.PreferedDays.Where(pd => pd.CustomerDescriptionID == customer.CustomerId).Count().ShouldBe(3);
            customerDescriptionExpected.PreferedDays.Count().ShouldBe(3);
            customerDescriptionExpected.PreferedDays.Where(pd => pd.Day == DayOfWeek.Tuesday && pd.IsDelete == false).Any().ShouldBe(true);
            customerDescriptionExpected.PreferedDays.Where(pd => pd.Day == DayOfWeek.Wednesday && pd.IsDelete == false).Any().ShouldBe(true);
            customerDescriptionExpected.PreferedDays.Where(pd => pd.Day == DayOfWeek.Thursday && pd.IsDelete == false).Any().ShouldBe(true);

            _context.CustomerSkillToDevelops.Where(cstd => cstd.CustomerId == customer.CustomerId).Count().ShouldBe(1);
            customerDescriptionExpected.CustomerSkillToDevelop.Count().ShouldBe(1);
            customerDescriptionExpected.CustomerSkillToDevelop.Where(csd => csd.SkillId == 2).Any().ShouldBe(true);

            _context.CustomerChildrenAgeBrackets.Where(ccab => ccab.CustomerId == customer.CustomerId).Count().ShouldBe(2);
            customerDescriptionExpected.CustomerChildrenAgeBracket.Count().ShouldBe(2);
            customerDescriptionExpected.CustomerChildrenAgeBracket.Where(ccab => ccab.AgeBracketId == 2).Any().ShouldBe(true);
            customerDescriptionExpected.CustomerChildrenAgeBracket.Where(ccab => ccab.AgeBracketId == 4).Any().ShouldBe(true);

            _context.CustomerSocialServices.Where(css => css.CustomerId == customer.CustomerId).Count().ShouldBe(2);
            customerDescriptionExpected.CustomerSocialService.Count().ShouldBe(2);
            customerDescriptionExpected.CustomerSocialService.Where(css => css.SocialServiceId == 1).Any().ShouldBe(true);
            customerDescriptionExpected.CustomerSocialService.Where(css => css.SocialServiceId == 3).Any().ShouldBe(true);
        }

        [Fact]
        public void UpdateTest_Success_NoNullToAllNull()
        {
            Customer customer = CreateCustomer("NoNullToAllNull");
            int id = customer.CustomerId;

            DateTime? pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = (DateTime)pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            customerDescriptionExpected.PreferedDays.Add(new PreferedDay { CustomerDescription = customerDescriptionExpected, Day = DayOfWeek.Friday, IsDelete = false });
            customerDescriptionExpected.PreferedDays.Add(new PreferedDay { CustomerDescription = customerDescriptionExpected, Day = DayOfWeek.Saturday, IsDelete = false });

            customerDescriptionExpected.CustomerSkillToDevelop.Add(new CustomerSkillToDevelop { Customer = customerDescriptionExpected, SkillId = 1, IsDelete = false });
            customerDescriptionExpected.CustomerSkillToDevelop.Add(new CustomerSkillToDevelop { Customer = customerDescriptionExpected, SkillId = 3, IsDelete = false });

            customerDescriptionExpected.CustomerChildrenAgeBracket.Add(new CustomerChildrenAgeBracket { Customer = customerDescriptionExpected, AgeBracketId = 1, IsDelete = false });
            customerDescriptionExpected.CustomerChildrenAgeBracket.Add(new CustomerChildrenAgeBracket { Customer = customerDescriptionExpected, AgeBracketId = 3, IsDelete = false });

            customerDescriptionExpected.CustomerSocialService.Add(new CustomerSocialService { Customer = customerDescriptionExpected, SocialServiceId = 2, IsDelete = false });
            customerDescriptionExpected.CustomerSocialService.Add(new CustomerSocialService { Customer = customerDescriptionExpected, SocialServiceId = 4, IsDelete = false });

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();


            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = id,
                PregnancyExpectedDate = null,
                PersonnalFollowUpMeetingCount = null,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = null,
                ParentId = null,
                MaritalStatusId = null,
                CitizenStatusId = null,
                FamilyTypeId = null,
                LanguageSpokenId = null,
                HomeTypeId = null,
                TransportTypeId = null,
                SchoolingId = null,
                IncomeSourceId = null,
                AvailabilityId = null,
                YearlyIncomeId = null,
                WantsToBecomeMember = null,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = null,
                CustomerSkillToDevelop = null,
                CustomerChildrenAgeBracket = null,
                CustomerSocialService = null
            };

            var response = _mediator.Send(updateCommand).Result;

            response.ShouldBeOfType(typeof(Unit));

            customerDescriptionExpected.Pregnancy.ShouldBe(null);
            customerDescriptionExpected.PersonnalFollowUp.ShouldBe(null);


            customerDescriptionExpected.LegalCustody.ShouldBe(null);
            customerDescriptionExpected.ChildrenCount.ShouldBe(5);
            customerDescriptionExpected.Sex.ShouldBe(null);
            customerDescriptionExpected.Parent.ShouldBe(null);
            customerDescriptionExpected.MaritalStatus.ShouldBe(null);
            customerDescriptionExpected.CitizenStatus.ShouldBe(null);
            customerDescriptionExpected.FamilyType.ShouldBe(null);
            customerDescriptionExpected.LanguageSpoken.ShouldBe(null);
            customerDescriptionExpected.HomeType.ShouldBe(null);
            customerDescriptionExpected.TransportType.ShouldBe(null);
            customerDescriptionExpected.Schooling.ShouldBe(null);
            customerDescriptionExpected.IncomeSource.ShouldBe(null);
            customerDescriptionExpected.Availability.ShouldBe(null);
            customerDescriptionExpected.YearlyIncome.ShouldBe(null);

            customerDescriptionExpected.WantsToBecomeMember.ShouldBe(null);
            customerDescriptionExpected.HasMentalHealthDiagnostic.ShouldBe(true);
            customerDescriptionExpected.HasBeenHospitalisedInPsychiatry.ShouldBe(false);
            customerDescriptionExpected.HasContactWithDPJinPast.ShouldBe(false);
            customerDescriptionExpected.HasContactWithDPJnow.ShouldBe(true);
            customerDescriptionExpected.WillParticipateToHelpingGroup.ShouldBe(false);

            _context.PreferedDays.Where(pd => pd.CustomerDescriptionID == customer.CustomerId).Count().ShouldBe(0);
            customerDescriptionExpected.PreferedDays.Count().ShouldBe(0);

            _context.CustomerSkillToDevelops.Where(cstd => cstd.CustomerId == customer.CustomerId).Count().ShouldBe(0);
            customerDescriptionExpected.CustomerSkillToDevelop.Count().ShouldBe(0);

            _context.CustomerChildrenAgeBrackets.Where(ccab => ccab.CustomerId == customer.CustomerId).Count().ShouldBe(0);
            customerDescriptionExpected.CustomerChildrenAgeBracket.Count().ShouldBe(0);

            _context.CustomerSocialServices.Where(css => css.CustomerId == customer.CustomerId).Count().ShouldBe(0);
            customerDescriptionExpected.CustomerSocialService.Count().ShouldBe(0);
        }

        [Fact]
        public void UpdateTest_Success_PregantToNotPregnant()
        {
            Customer customer = CreateCustomer("PregantToNotPregnant");
            int id = customer.CustomerId;

            DateTime? pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = (DateTime)pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = id,
                PregnancyExpectedDate = null,
                PersonnalFollowUpMeetingCount = 5,
                LegalCustodyId = 2,
                ChildrenCount = 3,
                SexId = 1,
                ParentId = 1,
                MaritalStatusId = 3,
                CitizenStatusId = 2,
                FamilyTypeId = 1,
                LanguageSpokenId = 1,
                HomeTypeId = 4,
                TransportTypeId = 2,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 3,
                YearlyIncomeId = 2,
                WantsToBecomeMember = true,
                HasMentalHealthDiagnostic = false,
                HasBeenHospitalisedInPsychiatry = true,
                HasContactWithDPJinPast = true,
                HasContactWithDPJnow = false,
                WillParticipateToHelpingGroup = true,

                PreferedDays = null,
                CustomerSkillToDevelop = null,
                CustomerChildrenAgeBracket = null,
                CustomerSocialService = null
            };

            var response = _mediator.Send(updateCommand).Result;
            response.ShouldBeOfType(typeof(Unit));
            customerDescriptionExpected.Pregnancy.ShouldBe(null);
            _context.Pregnancies.Where(p => p.PregnancyId == id).SingleOrDefault().ShouldBe(null);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_CustomerDontExist(int id)
        {
            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = id,
                PregnancyExpectedDate = null,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 2,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 1,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Fact]
        public void UpdateTest_Exception_CustomerDescriptionDontExist()
        {
            Customer customer = CreateCustomer("CustomerDescriptionDontExist");

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customer.CustomerId,
                PregnancyExpectedDate = null,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 2,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 1,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Fact]
        public void UpdateTest_Exception_CustomerExistButIsDelete()
        {
            Customer customer = CreateCustomer("CustomerExistButIsDelete");
            DateTime? pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = (DateTime)pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);

            var activation = customer.CustomerActivations.Where(ca => ca.IsActive == true).SingleOrDefault();
            activation.IsActive = false;
            activation.IsInactiveSince = DateTime.Now;
            customer.IsDelete = true;
            _context.Update(customer);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customer.CustomerId,
                PregnancyExpectedDate = null,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 2,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 1,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidSex(int sexId)
        {
            Customer customer = CreateCustomer("InvalidSex");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = sexId,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 1,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidHomeType(int id)
        {
            Customer customer = CreateCustomer("InvalidHomeType");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 1,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = id,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidParent(int id)
        {
            Customer customer = CreateCustomer("InvalidParent");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = id,
                MaritalStatusId = 2,
                CitizenStatusId = 1,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidMaritalStatus(int id)
        {
            Customer customer = CreateCustomer("InvalidMaritalStatus");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = id,
                CitizenStatusId = 1,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidCitizenStatus(int id)
        {
            Customer customer = CreateCustomer("InvalidCitizenStatus");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = id,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidFamilyType(int id)
        {
            Customer customer = CreateCustomer("InvalidFamilyType");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 2,
                FamilyTypeId = id,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidLanguageSpoken(int id)
        {
            Customer customer = CreateCustomer("InvalidLanguageSpoken");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 2,
                FamilyTypeId = 1,
                LanguageSpokenId = id,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidTransportType(int id)
        {
            Customer customer = CreateCustomer("InvalidTransportType");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 2,
                FamilyTypeId = 1,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = id,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidSchooling(int id)
        {
            Customer customer = CreateCustomer("InvalidSchooling");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 2,
                FamilyTypeId = 1,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = id,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidIncomeSource(int id)
        {
            Customer customer = CreateCustomer("InvalidIncomeSource");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 2,
                FamilyTypeId = 1,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = id,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidAvailability(int id)
        {
            Customer customer = CreateCustomer("InvalidAvailability");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 2,
                FamilyTypeId = 1,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 2,
                AvailabilityId = id,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidYearlyIncome(int id)
        {
            Customer customer = CreateCustomer("InvalidYearlyIncome");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 2,
                FamilyTypeId = 1,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 2,
                AvailabilityId = 1,
                YearlyIncomeId = id,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Fact]
        public void UpdateTest_Exception_InvalidPregnancyDate()
        {
            Customer customer = CreateCustomer("InvalidPregnancyDate");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = DateTime.Now - new TimeSpan(155, 0, 0, 0),
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 2,
                FamilyTypeId = 1,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 2,
                AvailabilityId = 1,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }


        [Fact]
        public void UpdateTest_Exception_InvalidChildrenCount()
        {
            Customer customer = CreateCustomer("InvalidChildrenCount");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = -1,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 2,
                FamilyTypeId = 1,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 2,
                AvailabilityId = 1,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Fact]
        public void UpdateTest_Exception_InvalidMeetinCount()
        {
            Customer customer = CreateCustomer("InvalidMeetinCount");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = -1,
                LegalCustodyId = null,
                ChildrenCount = 2,
                SexId = 1,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 2,
                FamilyTypeId = 1,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 2,
                AvailabilityId = 1,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { 2 },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void UpdateTest_Exception_InvalidSkill(int skillId)
        {
            Customer customer = CreateCustomer("InvalidSex");

            DateTime pregnancyDateTime = DateTime.Now + new TimeSpan(155, 0, 0, 0);

            var customerDescriptionExpected = new CustomerDescription
            {
                Customer = customer,
                Pregnancy = new Pregnancy { ChildBirthExpectedDate = pregnancyDateTime },
                PersonnalFollowUp = new PersonnalFollowUp { MeetingCount = 5 },
                LegalCustody = _context.LegalCustodies.Find(2),
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
                WillParticipateToHelpingGroup = true
            };

            _context.Add(customerDescriptionExpected);
            _context.SaveChanges();

            UpdateCustomerDescriptionCommand updateCommand = new UpdateCustomerDescriptionCommand
            {
                CustomerDescriptionId = customerDescriptionExpected.CustomerDescriptionId,
                PregnancyExpectedDate = pregnancyDateTime,
                PersonnalFollowUpMeetingCount = 6,
                LegalCustodyId = null,
                ChildrenCount = 5,
                SexId = 2,
                ParentId = 2,
                MaritalStatusId = 2,
                CitizenStatusId = 1,
                FamilyTypeId = 3,
                LanguageSpokenId = 4,
                HomeTypeId = 2,
                TransportTypeId = 1,
                SchoolingId = 2,
                IncomeSourceId = 1,
                AvailabilityId = 4,
                YearlyIncomeId = 1,
                WantsToBecomeMember = false,
                HasMentalHealthDiagnostic = true,
                HasBeenHospitalisedInPsychiatry = false,
                HasContactWithDPJinPast = false,
                HasContactWithDPJnow = true,
                WillParticipateToHelpingGroup = false,

                PreferedDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                CustomerSkillToDevelop = new List<int> { skillId },
                CustomerChildrenAgeBracket = new List<int> { 2, 4 },
                CustomerSocialService = new List<int>() { 1, 3 }
            };

            var response = _mediator.Send(updateCommand).ShouldThrow(typeof(FluentValidation.ValidationException));
        }
    }
}
