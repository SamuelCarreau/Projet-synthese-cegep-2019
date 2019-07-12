using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ParentEspoir.Common;
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
    public class CustomerCreateTest : TestBase
    {
        private static readonly string CUSTOMER_ADDRESS = "288, rang Sainte-Marie";
        private static readonly string CUSTOMER_CITY = "Saint-Alfred";
        private static readonly string CUSTOMER_COUNTRY = "Canada";
        private static readonly DateTime CUSTOMER_DATE_OF_BIRTH = new DateTime(1994, 01, 21);
        private static readonly string CUSTOMER_FIRST_NAME = "Frédéric";
        private static readonly string CUSTOMER_LAST_NAME = "Jacques";
        private static readonly string CUSTOMER_PHONE = "(1)-581-372-6081";
        private static readonly string CUSTOMER_PROVINCE_NAME = "Québec";
        private static readonly string CUSTOMER_POSTAL_CODE = "G0M 1L0";
        private static readonly string CUSTOMER_SECONDARY_PHONE = "418-774-9890";
        private static readonly DateTime CUSTOMER_INSCRIPTIONDATE = DateTime.Now - TimeSpan.FromDays(66);

        private ParentEspoirDbContext _context;

        public CustomerCreateTest()
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

        [Fact]
        public void CreateTest_Success_IEntityTest()
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
                InscriptionDate = CUSTOMER_INSCRIPTIONDATE
            };

            _time.Now = new DateTime(2019, 03, 01);
            _mediator.Send(new CreateCustomerCommand { Model = model });

            Customer customer = _context.Customers.Last();
            customer.CreationDate.ShouldBe(_time.Now);
            var modelCustomer = (CustomerModel)customer;
            modelCustomer.IsInscripted.ShouldBe(true);
            customer.SuppressionDate.ShouldBe(null);
        }

        [Fact]
        public void CreateTest_Success_NoNull()
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
                SupportGroupId = 1,
                ReferenceById = 2,
                HeardOfUsFromId = 3
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).Result;
            response.ShouldBeOfType(typeof(Unit));

            Customer customer = _context.Customers.Last();
            customer.CustomerActivations.Where(ca => ca.IsActive = true && ca.IsInactiveSince == null).Any().ShouldBe(true);
            customer.IsDelete.ShouldBe(false);
            customer.CustomerDescription.ShouldNotBe(null);

            customer.FirstName.ShouldBe(CUSTOMER_FIRST_NAME);
            customer.LastName.ShouldBe(CUSTOMER_LAST_NAME);
            customer.DateOfBirth.ShouldBe(CUSTOMER_DATE_OF_BIRTH);
            customer.Address.ShouldBe(CUSTOMER_ADDRESS);
            customer.PostalCode.ShouldBe(CUSTOMER_POSTAL_CODE);
            customer.City.ShouldBe(CUSTOMER_CITY);
            customer.Province.ShouldBe(CUSTOMER_PROVINCE_NAME);
            customer.Country.ShouldBe(CUSTOMER_COUNTRY);
            customer.Phone.ShouldBe(CUSTOMER_PHONE);
            customer.SecondaryPhone.ShouldBe(CUSTOMER_SECONDARY_PHONE);
            customer.SupportGroup.SupportGroupId.ShouldBe(1);
            customer.ReferenceBy.Id.ShouldBe(2);
            customer.HeardOfUsFrom.Id.ShouldBe(3);

            var modelCustomer = (CustomerModel)customer;
            modelCustomer.IsInscripted.ShouldBe(false);
        }

        [Fact]
        public void CreateTest_Success_AllNull()
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).Result;
            response.ShouldBeOfType(typeof(Unit));

            Customer customer = _context.Customers.Last();
            customer.CustomerActivations.Where(ca => ca.IsActive = true && ca.IsInactiveSince == null).Any().ShouldBe(true);
            customer.IsDelete.ShouldBe(false);
            customer.CustomerDescription.ShouldNotBe(null);

            customer.FirstName.ShouldBe(CUSTOMER_FIRST_NAME);
            customer.LastName.ShouldBe(CUSTOMER_LAST_NAME);
            customer.DateOfBirth.ShouldBe(CUSTOMER_DATE_OF_BIRTH);
            customer.Address.ShouldBe(CUSTOMER_ADDRESS);
            customer.PostalCode.ShouldBe(CUSTOMER_POSTAL_CODE);
            customer.City.ShouldBe(CUSTOMER_CITY);
            customer.Province.ShouldBe(CUSTOMER_PROVINCE_NAME);
            customer.Country.ShouldBe(CUSTOMER_COUNTRY);
            customer.Phone.ShouldBe(CUSTOMER_PHONE);
            customer.SecondaryPhone.ShouldBe(CUSTOMER_SECONDARY_PHONE);
            customer.SupportGroup.ShouldBe(null);
            customer.ReferenceBy.ShouldBe(null);
            customer.HeardOfUsFrom.ShouldBe(null);
        }

        [Fact]
        public void CreateTest_Success_OneFileNumber()
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            _time.Now = new DateTime(2019, 03, 01);
            _mediator.Send(new CreateCustomerCommand { Model = model });

            _context.Customers.Single(c => c.FileNumber == 20190301);
        }

        [Fact]
        public void CreateTest_Success_ManyFileNumberSameMonth()
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            for (int i = 1; i < 15; i++)
            {
                _time.Now = new DateTime(2019, 03, i);
                _mediator.Send(new CreateCustomerCommand { Model = model });
            }

            IEnumerable<Customer> customers = _context.Customers.ToList();

            for (int i = 1; i < 15; i++)
            {
                if (i < 10)
                {
                    customers.Single(c => c.FileNumber == Convert.ToInt32("2019030" + Convert.ToString(i)));
                }
                else
                {
                    customers.Single(c => c.FileNumber == Convert.ToInt32("201903" + Convert.ToString(i)));
                }
            }
        }

        [Fact]
        public void CreateTest_Success_ManyFileNumberDifferentMonths()
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            for (int i = 1; i < 6; i++)
            {
                _time.Now = new DateTime(2019, i, 8);
                _mediator.Send(new CreateCustomerCommand { Model = model });

                _time.Now = new DateTime(2019, i, 15);
                _mediator.Send(new CreateCustomerCommand { Model = model });
            }

            IEnumerable<Customer> customers = _context.Customers.ToList();

            for (int i = 1; i < 6; i++)
            {
                customers.Single(c => c.FileNumber == Convert.ToInt32("20190" + Convert.ToString(i) + "01"));
                customers.Single(c => c.FileNumber == Convert.ToInt32("20190" + Convert.ToString(i) + "02"));
            }
        }

        [Fact]
        public void CreateTest_Success_ManyFileNumberDifferentYears()
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            for (int i = 1; i < 10; i++)
            {
                int year = Convert.ToInt32("200" + Convert.ToString(i));
                _time.Now = new DateTime(year, 03, 8);
                _mediator.Send(new CreateCustomerCommand { Model = model });

                _time.Now = new DateTime(year, 3, 15);
                _mediator.Send(new CreateCustomerCommand { Model = model });
            }

            IEnumerable<Customer> customers = _context.Customers.ToList();

            for (int i = 1; i < 10; i++)
            {
                string year = "200" + Convert.ToString(i);
                customers.Single(c => c.FileNumber == Convert.ToInt32(year + "0301"));
                customers.Single(c => c.FileNumber == Convert.ToInt32(year + "0302"));
            }
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(87)]
        public void CreateTest_Exception_InvalidReferenceById(int id)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
                ReferenceById = id
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(87)]
        public void CreateTest_Exception_InvalidHearOfUsFromId(int id)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
                HeardOfUsFromId = id
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(87)]
        public void CreateTest_Exception_InvalidSupportGrouptId(int id)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
                SupportGroupId = id
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void CreateTest_Exception_InvalidFirstName(string data)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = data,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void CreateTest_Exception_InvalidLastName(string data)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = data,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Fact]
        public void CreateTest_Exception_InvalidDateOfBirthAfterToday()
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = DateTime.Now + new TimeSpan(10, 0, 0, 0),
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Fact]
        public void CreateTest_Exception_InvalidDateOfBirthBefore1900()
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = new DateTime(1899, 1, 1),
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void CreateTest_Exception_InvalidAddress(string data)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = data,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void CreateTest_Exception_InvalidPostalCodeName(string data)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = data,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void CreateTest_Exception_InvalidCityName(string data)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = data,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void CreateTest_Exception_InvalidProvinceName(string data)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = data,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void CreateTest_Exception_InvalidCountryName(string data)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = data,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void CreateTest_Exception_InvalidPhone(string data)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = data,
                SecondaryPhone = CUSTOMER_SECONDARY_PHONE,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("skdjfklasdfnlsadnfknaskdlfnksadjlfjaskdjflsjdkfjkasldjfklajsdklfjkalsdjfklasdnfklasndklfnaslk")]
        public void CreateTest_Exception_InvalidSecondaryPhone(string data)
        {
            CustomerModel model = new CustomerModel
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                DateOfBirth = CUSTOMER_DATE_OF_BIRTH,
                Address = CUSTOMER_ADDRESS,
                PostalCodeName = CUSTOMER_POSTAL_CODE,
                CityName = CUSTOMER_CITY,
                ProvinceName = CUSTOMER_PROVINCE_NAME,
                CountryName = CUSTOMER_COUNTRY,
                Phone = CUSTOMER_PHONE,
                SecondaryPhone = data,
            };

            var response = _mediator.Send(new CreateCustomerCommand { Model = model }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

    }
}
