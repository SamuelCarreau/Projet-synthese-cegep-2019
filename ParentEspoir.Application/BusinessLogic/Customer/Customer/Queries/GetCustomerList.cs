using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class CustomerListModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public string SortOrder { get; set; }
        public string SearchFilter { get; set; }
        public IEnumerable<IndexCustomerModel> Customers { get; set; }
    }

    public class GetCustomerListQuery : IRequest<CustomerListModel>
    {
        public int CurrentPage { get; set; }
        public string SortOrder { get; set; }
        public string SearchFilter { get; set; }
    }

    public class GetCustomerQueryListHandler : IRequestHandler<GetCustomerListQuery, CustomerListModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetCustomerQueryListHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerListModel> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        {
            ICollection<IndexCustomerModel> customers = new HashSet<IndexCustomerModel>();

            var customersQuery = _context.Customers
                .Where(c => c.IsDelete == false)
                .Select(c => new IndexCustomerModel
                {
                    Id = c.CustomerId,
                    FileNumber = c.FileNumber,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    NormalizedName = c.NormalizedName,
                    Address = c.Address,
                    City = c.City,
                    Phone = c.Phone
                });
            
            if (request.SearchFilter != null)
            {
                string[] filters = request.SearchFilter.Split(' ');

                foreach (var filter in filters)
                {
                    var tempList = await customersQuery
                            .Where(c => c.NormalizedName
                            .Contains(StringNormalizer.Normalize(filter)))
                            .ToListAsync();

                    foreach (var item in tempList)
                    {
                        customers.Add(item);
                    }
                }
            }
            else
            {
                customers = await customersQuery.ToListAsync();
            }
            
            switch (request.SortOrder)
            {
                case "LastNameAscending":
                    customers = customers.OrderBy(c => c.LastName).ToList(); 
                    break;
                case "LastNameDescending":
                    customers = customers.OrderByDescending(c => c.LastName).ToList(); 
                    break;
                case "CityAscending":
                    customers = customers.OrderBy(c => c.City).ToList();
                    break;
                case "CityDescending":
                    customers = customers.OrderByDescending(c => c.City).ToList();
                    break;
            }

            int totalCount = customers.Count();
            var pageCalculator = new PageCalculator(20, request.CurrentPage, totalCount);

            var entities = customers
                .Skip(pageCalculator.Skip)
                .Take(pageCalculator.Take);

            return new CustomerListModel
            {
                CurrentPage = pageCalculator.CurrentPage,
                TotalPages = pageCalculator.TotalPage,
                TotalCount = totalCount,
                Customers = entities,
                SortOrder = request.SortOrder,
                SearchFilter = request.SearchFilter
            };
        }
    }
}