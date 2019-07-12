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
    public class CustomerGetListTest : TestBase
    {
        private static readonly int TOTAL_CUSTOMER_COUNT = 67;
        private static readonly int TOTAL_PAGES_COUNT = 4;
        private static readonly int TOTAL_CUSTOMER_PER_PAGE = 20;
        private static readonly int TOTAL_CUSTOMER_ON_LAST_PAGE = 7;

        private static readonly string SORT_BY_NAME_ASCENDING = "FirstNameAscending";
        private static readonly string SORT_BY_NAME_DESCENDING = "FirstNameDescending";
        private static readonly string SORT_BY_CITY_ASCENDING = "CityAscending";
        private static readonly string SORT_BY_DESCENDING = "CityDescending";

        private ParentEspoirDbContext _context;

        public CustomerGetListTest()
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
        public void GetTest_Success_GetListOfMultiple()
        {
            List<Customer> customers = new List<Customer>
            {
                CreateCustomer("Multiple_01"),
                CreateCustomer("Multiple_02"),
                CreateCustomer("Multiple_03"),
                CreateCustomer("Multiple_04"),
                CreateCustomer("Multiple_05"),
                CreateCustomer("Multiple_06")
            };

            var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = 1 }).Result;
            response.ShouldNotBe(null);
            response.ShouldBeOfType(typeof(CustomerListModel));
            response.Customers.GetType().GetInterfaces().ShouldContain(typeof(IEnumerable<IndexCustomerModel>));
            response.Customers.Count().ShouldBe(6);
            response.CurrentPage.ShouldBe(1);
            response.TotalPages.ShouldBe(1);
            response.SortOrder.ShouldBe(null);

            foreach (var customer in customers)
            {
                response.Customers.Where(r => r.Id == customer.CustomerId).Any().ShouldBe(true);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetTest_Success_MultiplePages_FirstFullPage(int page)
        {
            for (int i = 0; i < TOTAL_CUSTOMER_COUNT; i++)
            {
                CreateCustomer("MultiplePage-client-" + i);
            }

            var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = page } ).Result;
            response.ShouldNotBe(null);
            response.Customers.Count().ShouldBe(TOTAL_CUSTOMER_PER_PAGE);
            response.CurrentPage.ShouldBe(page);
            response.TotalPages.ShouldBe(TOTAL_PAGES_COUNT);
            response.SearchFilter.ShouldBe(null);
            response.SortOrder.ShouldBe(null);
        }

        [Fact]
        public void GetTest_Success_MultiplePages_LastPage()
        {
            for (int i = 0; i < TOTAL_CUSTOMER_COUNT; i++)
            {
                CreateCustomer("MultiplePage-client-" + i);
            }

            var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = TOTAL_PAGES_COUNT }).Result;
            response.ShouldNotBe(null);
            response.Customers.Count().ShouldBe(TOTAL_CUSTOMER_ON_LAST_PAGE);
            response.CurrentPage.ShouldBe(TOTAL_PAGES_COUNT);
            response.TotalPages.ShouldBe(TOTAL_PAGES_COUNT);
            response.SearchFilter.ShouldBe(null);
            response.SortOrder.ShouldBe(null);
        }

        [Theory]
        [InlineData(-99)]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetTest_Success_MultiplePages_InvalidCurrentPage_BeforeFirstPage(int page)
        {
            for (int i = 0; i < TOTAL_CUSTOMER_COUNT; i++)
            {
                CreateCustomer("MultiplePage-client-" + i);
            }

            var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = page }).Result;
            response.ShouldNotBe(null);
            response.Customers.Count().ShouldBe(TOTAL_CUSTOMER_PER_PAGE);
            response.CurrentPage.ShouldBe(1);
            response.TotalPages.ShouldBe(TOTAL_PAGES_COUNT);
            response.SearchFilter.ShouldBe(null);
            response.SortOrder.ShouldBe(null);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(89)]
        public void GetTest_Success_MultiplePages_AfterLastPage(int page)
        {
            for (int i = 0; i < TOTAL_CUSTOMER_COUNT; i++)
            {
                CreateCustomer("MultiplePage-client-" + i);
            }

            var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = page }).Result;
            response.ShouldNotBe(null);
            response.Customers.Count().ShouldBe(TOTAL_CUSTOMER_ON_LAST_PAGE);
            response.CurrentPage.ShouldBe(TOTAL_PAGES_COUNT);
            response.TotalPages.ShouldBe(TOTAL_PAGES_COUNT);
            response.SearchFilter.ShouldBe(null);
            response.SortOrder.ShouldBe(null);
        }

        //[Fact]
        //public void GetTest_Success_SortTest()
        //{
        //    // NOT FINISH

        //    CreateCustomer("EEE");
        //    CreateCustomer("AAA");
        //    CreateCustomer("BBB");
        //    CreateCustomer("FFF");
        //    CreateCustomer("DDD");
        //    CreateCustomer("CCC");

        //    var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = 1, SortOrder = SORT_BY_NAME_ASCENDING }).Result;
        //    response.ShouldNotBe(null);

        //    response.Customers.ShouldBeInOrder();
        //}

        //[Theory]
        //[InlineData("Jean")]
        //[InlineData("jean")]
        //[InlineData("JeAn")]
        //[InlineData("J")]
        //[InlineData("jean-Francois")]
        //[InlineData("jean-FrancOis")]
        //public void GetTest_Success_SearchTest(string searchFilter)
        //{
        //    //CreateCustomer("Jean-Francois");
        //    //CreateCustomer("Fred");
        //    //CreateCustomer("Sam");
        //    //CreateCustomer("Tommy");
        //    //CreateCustomer("Miguel");

        //    //var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = 1, SearchFilter = searchFilter }).Result;
        //    //response.ShouldNotBe(null);

        //    //response.Customers.ShouldContain(c => c.FirstName.Contains("Jean-Francois"));

        //    //response.Customers.ShouldNotContain(c => c.FirstName.Contains("Fred"));
        //    //response.Customers.ShouldNotContain(c => c.FirstName.Contains("Sam"));
        //    //response.Customers.ShouldNotContain(c => c.FirstName.Contains("Tommy"));
        //    //response.Customers.ShouldNotContain(c => c.FirstName.Contains("Miguel"));
        //}

        //[Fact]
        //public void GetTest_Success_SearchTest_EmptyResult()
        //{
        //    CreateCustomer("Jean-Francois");
        //    CreateCustomer("Fred");
        //    CreateCustomer("Sam");
        //    CreateCustomer("Tommy");
        //    CreateCustomer("Miguel");

        //    var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = 1, SearchFilter = "aksdbfjksdbfjkasdjkfbasjk" }).Result;
        //    response.ShouldNotBe(null);

        //    response.Customers.ShouldBeEmpty();
        //}

        [Fact]
        public void GetTest_Success_GetListOfNone()
        {
            var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = 1 }).Result;
            response.ShouldNotBe(null);
            response.ShouldBeOfType(typeof(CustomerListModel));
            response.Customers.Count().ShouldBe(0);
        }

        [Fact]
        public void GetTest_Success_GetListOfOne()
        {
            Customer customer = CreateCustomer("GetListOfOne");

            var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = 1 }).Result;
            response.ShouldNotBe(null);
            response.ShouldBeOfType(typeof(CustomerListModel));
            response.Customers.Count().ShouldBe(1);

            response.Customers.Where(r => r.Id == customer.CustomerId).Any().ShouldBe(true);
        }

        [Fact]
        public void GetTest_Success_GetListOfNoneWithDelete()
        {
            Customer customer = CreateCustomer("GetListOfNoneWithOneDelete");
            customer.CustomerActivations.First().IsActive = false;
            customer.CustomerActivations.First().IsInactiveSince = DateTime.Now;
            customer.IsDelete = true;

            _context.Update(customer);
            _context.SaveChanges();

            var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = 1 }).Result;
            response.ShouldNotBe(null);
            response.ShouldBeOfType(typeof(CustomerListModel));
            response.Customers.Count().ShouldBe(0);
        }

        [Fact]
        public void GetTest_Success_GetListOfOneWithDelete()
        {
            Customer customer01 = CreateCustomer("GetListOfOneWithDelete_01");
            Customer customer02 = CreateCustomer("GetListOfOneWithDelete_02");
            customer02.CustomerActivations.First().IsActive = false;
            customer02.CustomerActivations.First().IsInactiveSince = DateTime.Now;
            customer02.IsDelete = true;
            _context.Update(customer02);
            _context.SaveChanges();

            var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = 1 }).Result;
            response.ShouldNotBe(null);
            response.ShouldBeOfType(typeof(CustomerListModel));
            response.Customers.Count().ShouldBe(1);

            response.Customers.Where(r => r.Id == customer01.CustomerId).Any().ShouldBe(true);
        }

        [Fact]
        public void GetTest_Success_GetListOfMultipleWithDelete()
        {
            List<Customer> customers = new List<Customer>
            {
                CreateCustomer("Multiple_01"),
                CreateCustomer("Multiple_02"),
                CreateCustomer("Multiple_03"),
                CreateCustomer("Multiple_04"),
                CreateCustomer("Multiple_05"),
                CreateCustomer("Multiple_06")
            };

            for (int i = 0; i < 3; i++)
            {
                customers[i].CustomerActivations.First().IsActive = false;
                customers[i].CustomerActivations.First().IsInactiveSince = DateTime.Now;
                customers[i].IsDelete = true;
                _context.Update(customers[i]);
            }
            _context.SaveChanges();

            var response = _mediator.Send(new GetCustomerListQuery { CurrentPage = 1 }).Result;
            response.ShouldNotBe(null);
            response.ShouldBeOfType(typeof(CustomerListModel));
            response.Customers.Count().ShouldBe(3);

            for (int i = 6; i < 6; i++)
            {
                response.Customers.Where(r => r.Id == customers[i].CustomerId).Any().ShouldBe(true);
            }
        }
    }
}
