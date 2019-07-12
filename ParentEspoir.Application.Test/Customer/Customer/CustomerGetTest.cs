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
    public class CustomerGetTest : TestBase
    {
        private ParentEspoirDbContext _context;

        public CustomerGetTest()
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
                HeardOfUsFrom = _context.HeardOfUsFroms.Find(3),
                Phone = "418-271-3856",
                SecondaryPhone = "418-895-5996",
                ReferenceBy = _context.ReferenceTypes.Find(2),
                SupportGroup = _context.SupportGroups.Find(4)
            };
            customer.CustomerActivations.Add(new CustomerActivation { IsActive = true, IsActiveSince = DateTime.Now });

            _context.Add(customer);
            _context.SaveChanges();
            return _context.Customers.Include(c => c.CustomerActivations).Where(c => c.FirstName.Equals(customerFirstName)).Single();
        }

        [Fact]
        public void GetTest_Success_GetSingle()
        {
            Customer customer = CreateCustomer("GetSingle");

            var response = _mediator.Send(new GetCustomerQuery { CustomerId = customer.CustomerId }).Result;
            response.ShouldNotBe(null);
            response.ShouldBeOfType(typeof(CustomerModel));
            
            response.Id.ShouldBe(customer.CustomerId);
            response.FirstName.ShouldBe(customer.FirstName);
            response.LastName.ShouldBe(customer.LastName);
            response.DateOfBirth.ShouldBe(customer.DateOfBirth);
            response.Address.ShouldBe(customer.Address);
            response.PostalCodeName.ShouldBe(customer.PostalCode);
            response.CityName.ShouldBe(customer.City);
            response.ProvinceName.ShouldBe(customer.Province);
            response.CountryName.ShouldBe(customer.Country);
            response.Phone.ShouldBe(customer.Phone);
            response.SecondaryPhone.ShouldBe(customer.SecondaryPhone);
            response.SupportGroupId.ShouldBe(customer.SupportGroup?.SupportGroupId);
            response.ReferenceById.ShouldBe(customer.ReferenceBy?.Id);
            response.HeardOfUsFromId.ShouldBe(customer.HeardOfUsFrom?.Id);
        }

        [Theory]
        [InlineData(-123)]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(88)]
        public void GetTest_Exception_CustomerDontExist(int id)
        {
            var response = _mediator.Send(new GetCustomerQuery { CustomerId = id }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Fact]
        public void GetTest_Exception_CustomerExistButIsDelete()
        {
            Customer customer = CreateCustomer("CustomerExistButIsNotValid");

            customer.CustomerActivations.First().IsActive = false;
            customer.CustomerActivations.First().IsInactiveSince = DateTime.Now;
            customer.IsDelete = true;
            _context.Update(customer);
            _context.SaveChanges();

            var response = _mediator.Send(new GetCustomerQuery { CustomerId = customer.CustomerId }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }
    }
}