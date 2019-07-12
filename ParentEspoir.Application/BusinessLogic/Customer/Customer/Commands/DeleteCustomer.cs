using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class DeleteCustomerCommand : IRequest
    {
        public int CustomerId { get; set; }
    }

    public class DeleteCustomerModelValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerModelValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.CustomerId).Must(x => context.Customers.Where(c => c.CustomerId == x && c.IsDelete == false).Any());
        }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public DeleteCustomerCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer customer = await _context.Customers
                .Include(c => c.CustomerActivations)
                .Where(c => c.CustomerId == request.CustomerId)
                .SingleOrDefaultAsync();

            customer.IsDelete = true;
            CustomerActivation customerActivation = customer.CustomerActivations
                .SingleOrDefault(ca => ca.IsActive == true);
            customerActivation.IsActive = false;
            customerActivation.IsInactiveSince = DateTime.Now;

            _context.Update(customer);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}