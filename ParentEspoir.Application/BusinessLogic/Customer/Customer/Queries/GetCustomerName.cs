using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    /// <summary>
    /// Cette query retourne une liste de client a partir d'une requête fait.
    /// C'est la recherche par nom dans la web form. 
    /// </summary>

    public class GetCustomerNameQuery : IRequest<string>
    {
        public int CustomerId { get; set; }
    }

    public class GetCustomerNameQueryHandler : IRequestHandler<GetCustomerNameQuery, string>
    {
        private readonly ParentEspoirDbContext _context;

        public GetCustomerNameQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetCustomerNameQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .Where(c => c.CustomerId == request.CustomerId)
                .Select(c => new { c.FirstName, c.LastName })
                .SingleAsync();
           
            return customer.FirstName + " " + customer.LastName;
        }
    }
}
