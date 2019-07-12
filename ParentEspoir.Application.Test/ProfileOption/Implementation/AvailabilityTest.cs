using ParentEspoir.Persistence;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System;
using FluentValidation;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public class AvailabilityTest : ProfilOptionTestBase<Availability>
    {
        private ParentEspoirDbContext _context;
        public AvailabilityTest() : base()
        {
            _context = GetDbContext();

            var custo = _context.Add(new Customer
            {
                CustomerDescription = new CustomerDescription(),
            }).Entity;

            custo.CustomerActivations.Add(new CustomerActivation { IsActive = true, IsActiveSince = DateTime.Now });

            _context.SaveChanges();
        }

        [Fact]
        public override void CantDeleteRelatedData()
        {
            var entity = new Availability { Name = "testTrue" };

            entity.CustomerDescriptions.Add(_context.CustomerDescriptions.First());

            _context.Add(entity);

            _context.SaveChanges();

            var result = _mediator.Send(new DeleteProfilOptionCommand<Availability> { Id = entity.Id })
                .ShouldThrow(typeof(ValidationException));
        }
    }
}