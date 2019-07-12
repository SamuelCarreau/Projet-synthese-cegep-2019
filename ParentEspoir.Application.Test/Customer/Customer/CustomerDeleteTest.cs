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
    public class CustomerDeleteTest : TestBase
    {
        private ParentEspoirDbContext _context;

        public CustomerDeleteTest()
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
        public void DeleteTest_Success_Normal()
        {
            Customer customer = CreateCustomer("Normal");

            var response = _mediator.Send(new DeleteCustomerCommand { CustomerId = customer.CustomerId }).Result;
            response.ShouldBeOfType(typeof(Unit));
            customer.IsDelete.ShouldBe(true);
            customer.CustomerActivations.Where(ca => ca.IsActive == true).Any().ShouldBe(false);
            customer.CustomerActivations.Where(ca => ca.IsInactiveSince == null).Any().ShouldBe(false);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(89)]
        public void DeleteTest_Exception_WrongId(int id)
        {
            var response = _mediator.Send(new DeleteCustomerCommand { CustomerId = id }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Fact]
        public void DeleteTest_Exception_IsAlreadyDelete()
        {
            Customer customer = CreateCustomer("IsAlreadyDelete");
            customer.IsDelete = true;
            CustomerActivation customerActivation = customer.CustomerActivations.Where(ca => ca.IsActive == true).SingleOrDefault();
            customerActivation.IsActive = false;
            customerActivation.IsInactiveSince = DateTime.Now;
            _context.Update(customer);
            _context.SaveChanges();


            var response = _mediator.Send(new DeleteCustomerCommand { CustomerId = customer.CustomerId }).ShouldThrow(typeof(FluentValidation.ValidationException));
        }

    }
}
