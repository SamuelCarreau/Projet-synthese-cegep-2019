using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ParentEspoir.Application.Test
{
    public class CustomerUpdateTest : TestBase
    {
        private static readonly string FIRST_NAME_CHANGE = "FirstNamedChanged";
        private static readonly string LAST_NAME_CHANGE = "LastNamedChanged";
        private static readonly string ADDRESS_CHANGE = "AddressChanged";
        private static readonly string POSTAL_CODE_CHANGE = "PostalCodeChanged";
        private static readonly string CITY_CHANGE = "CityChanged";
        private static readonly string PROVINCE_NAME_CHANGE = "ProvinceChanged";
        private static readonly string COUNTRY_CHANGE = "CountryChanged";
        private static readonly DateTime DATE_OF_BIRTH_CHANGE = new DateTime(1994, 01, 21);
        private static readonly string PHONE_CHANGE = "(1)-581-372-6081";
        private static readonly string SECONDARY_PHONE_CHANGE = "(1)-418-774-4589";
        private static readonly int HEARD_OF_US_ID_CHANGE = 2;
        private static readonly int REFERENCE_BY_ID_CHANGE = 1;
        private static readonly int SUPPORT_GROUP_ID_CHANGE = 1;

        private ParentEspoirDbContext _context;

        public CustomerUpdateTest()
        {
            _context = GetDbContext();
            PopulateTestingData();
        }

        private void PopulateTestingData()
        {
            _context.AddRange(new SupportGroup { Name = "Cegep de sainte-foy" }, new SupportGroup { Name = "Universite Laval" }, new SupportGroup { Name = "Hopital regional" }, new SupportGroup { Name = "Centre des femmes" }, new SupportGroup { Name = "isDelete", IsDelete = true });
            _context.AddRange(new ReferenceType { Name = "Ecole" }, new ReferenceType { Name = "Hopital" }, new ReferenceType { Name = "CLSC" }, new ReferenceType { Name = "isDelete", IsDelete = true });
            _context.AddRange(new HeardOfUsFrom { Name = "reseau sociaux" }, new HeardOfUsFrom { Name = "moteur de recherche" }, new HeardOfUsFrom { Name = "journaux" }, new HeardOfUsFrom { Name = "connaissance" }, new HeardOfUsFrom { Name = "professionnel de la sante" }, new HeardOfUsFrom { Name = "isDelete", IsDelete = true });
            _context.SaveChanges();
        }

        protected Customer CreateCustomer(string customerFirstName)
        {
            var customer = new Customer
            {
                FirstName = customerFirstName,
                LastName = "Legrand",
                Address = "516 rue Saint-Luc",
                PostalCode = "G1N2V3",
                City = "Quebec",
                Province = "Quebec",
                Country = "Canada",
                DateOfBirth = new DateTime(1986, 11, 25),
                Phone = "418-271-3856",
                SecondaryPhone = "418-895-5996",
                HeardOfUsFrom = _context.HeardOfUsFroms.Find(3),
                ReferenceBy = _context.ReferenceTypes.Find(2),
                SupportGroup = _context.SupportGroups.Find(4),
                CustomerDescription = new CustomerDescription()
            };
            customer.CustomerActivations.Add(new CustomerActivation { IsActive = true, IsActiveSince = DateTime.Now });

            _context.Add(customer);
            _context.SaveChanges();
            return _context.Customers.Include(c => c.CustomerActivations).Where(c => c.FirstName.Equals(customerFirstName)).Single();
        }

        [Fact]
        public void UpdateTest_Success_ChangeAllValue()
        {
            Customer customer = CreateCustomer("ChangeAllValue");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).Result;
            response.ShouldBeOfType(typeof(Unit));

            customer.CustomerDescription.ShouldNotBe(null);

            customer.CustomerId.ShouldBe(customer.CustomerId);
            customer.FirstName.ShouldBe(FIRST_NAME_CHANGE);
            customer.LastName.ShouldBe(LAST_NAME_CHANGE);
            customer.Address.ShouldBe(ADDRESS_CHANGE);
            customer.PostalCode.ShouldBe(POSTAL_CODE_CHANGE);
            customer.City.ShouldBe(CITY_CHANGE);
            customer.Province.ShouldBe(PROVINCE_NAME_CHANGE);
            customer.Country.ShouldBe(COUNTRY_CHANGE);
            customer.DateOfBirth.ShouldBe(DATE_OF_BIRTH_CHANGE);
            customer.Phone.ShouldBe(PHONE_CHANGE);
            customer.SecondaryPhone.ShouldBe(SECONDARY_PHONE_CHANGE);
            customer.HeardOfUsFrom.Id.ShouldBe(HEARD_OF_US_ID_CHANGE);
            customer.ReferenceBy.Id.ShouldBe(REFERENCE_BY_ID_CHANGE);
            customer.SupportGroup.SupportGroupId.ShouldBe(SUPPORT_GROUP_ID_CHANGE);
        }

        [Fact]
        public void UpdateTest_Success_NullToNotNull()
        {
            var customer = new Customer
            {
                FirstName = "NullToNotNull",
                LastName = "Legrand",
                Address = "516 rue Saint-Luc",
                PostalCode = "G1N2V3",
                City = "Quebec",
                Province = "Quebec",
                Country = "Canada",
                DateOfBirth = new DateTime(1986, 11, 25),
                Phone = "418-271-3856",
                SecondaryPhone = "418-895-5996",
                ReferenceBy = null,
                SupportGroup = null,
                HeardOfUsFrom = null
            };
            customer.CustomerActivations.Add(new CustomerActivation { IsActive = true, IsActiveSince = DateTime.Now });

            _context.Add(customer);
            _context.SaveChanges();

            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).Result;
            response.ShouldBeOfType(typeof(Unit));

            customer.HeardOfUsFrom.Id.ShouldBe(HEARD_OF_US_ID_CHANGE);
            customer.ReferenceBy.Id.ShouldBe(REFERENCE_BY_ID_CHANGE);
            customer.SupportGroup.SupportGroupId.ShouldBe(SUPPORT_GROUP_ID_CHANGE);
        }

        [Fact]
        public void UpdateTest_Success_NoNullToNull()
        {
            Customer customer = CreateCustomer("NoNullToNull");

            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = null,
                ReferenceById = null,
                SupportGroupId = null
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).Result;
            response.ShouldBeOfType(typeof(Unit));

            customer.HeardOfUsFrom.ShouldBe(null);
            customer.ReferenceBy.ShouldBe(null);
            customer.SupportGroup.ShouldBe(null);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(87)]
        public void UpdateTest_Exception_InvalidCustomerId(int id)
        {
            var model = new CustomerModel
            {
                Id = id,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void UpdateTest_Exception_FirstName(string data)
        {
            Customer customer = CreateCustomer("InvalidFisrtName");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = data,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void UpdateTest_Exception_InvalidLastName(string data)
        {
            Customer customer = CreateCustomer("InvalidLastName");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = data,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void UpdateTest_Exception_InvalidPostalCode(string data)
        {
            Customer customer = CreateCustomer("InvalidPostalCode");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = data,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void UpdateTest_Exception_InvalidCityName(string data)
        {
            Customer customer = CreateCustomer("InvalidCityName");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = data,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void UpdateTest_Exception_InvalidProvinceName(string data)
        {
            Customer customer = CreateCustomer("InvalidProvinceName");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = data,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void UpdateTest_Exception_InvalidCountryName(string data)
        {
            Customer customer = CreateCustomer("InvalidCountryName");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = data,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Fact]
        public void UpdateTest_Exception_InvalidDateOfBirthBefore1900()
        {
            Customer customer = CreateCustomer("InvalidDateOfBirthBefore1900");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = new DateTime(1899, 1, 1),
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }


        [Fact]
        public void UpdateTest_Exception_InvalidDateOfBirthAfterToday()
        {
            Customer customer = CreateCustomer("InvalidDateOfBirthAfterToday");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DateTime.Now + new TimeSpan(10, 0, 0, 0),
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void UpdateTest_Exception_InvalidPhone(string data)
        {
            Customer customer = CreateCustomer("InvalidPhone");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = data,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void UpdateTest_Exception_InvalidSecondaryPhone(string data)
        {
            Customer customer = CreateCustomer("InvalidSecondaryPhone");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = data,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(87)]
        public void UpdateTest_Exception_InvalidHeardOfUsFromId(int id)
        {
            Customer customer = CreateCustomer("InvalidHeardOfUsFromId");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = id,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(87)]
        public void UpdateTest_Exception_InvalidReferenceById(int id)
        {
            Customer customer = CreateCustomer("InvalidReferenceById");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = id,
                SupportGroupId = SUPPORT_GROUP_ID_CHANGE
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(87)]
        public void UpdateTest_Exception_InvalidSupportGroupId(int id)
        {
            Customer customer = CreateCustomer("InvalidSupportGroupId");
            var model = new CustomerModel
            {
                Id = customer.CustomerId,
                FirstName = FIRST_NAME_CHANGE,
                LastName = LAST_NAME_CHANGE,
                Address = ADDRESS_CHANGE,
                PostalCodeName = POSTAL_CODE_CHANGE,
                CityName = CITY_CHANGE,
                ProvinceName = PROVINCE_NAME_CHANGE,
                CountryName = COUNTRY_CHANGE,
                DateOfBirth = DATE_OF_BIRTH_CHANGE,
                Phone = PHONE_CHANGE,
                SecondaryPhone = SECONDARY_PHONE_CHANGE,
                HeardOfUsFromId = HEARD_OF_US_ID_CHANGE,
                ReferenceById = REFERENCE_BY_ID_CHANGE,
                SupportGroupId = id
            };

            var response = _mediator.Send(new UpdateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }
    }
}
