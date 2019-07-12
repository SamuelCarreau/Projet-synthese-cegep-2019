using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class GetCustomerQuery : IRequest<CustomerModel>
    {
        public int CustomerId { get; set; }
    }

    public class GetCustomerModelQueryValidator : AbstractValidator<GetCustomerQuery>
    { 
        public GetCustomerModelQueryValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.CustomerId)
                .Must(x => context.Customers.Where(cc => cc.CustomerId == x && cc.IsDelete == false).SingleOrDefault() != null);
            }
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerModel>
    {
        private readonly ParentEspoirDbContext _context;

        public GetCustomerQueryHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .Include(c => c.HeardOfUsFrom)
                .Include(c => c.ReferenceBy)
                .Include(c => c.SupportGroup)
                .SingleAsync(c => c.CustomerId == request.CustomerId);
            CustomerModel model = (CustomerModel)customer;
            return model;
        }
    }
}
