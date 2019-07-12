using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System.Threading;
using MediatR;
using System.Collections.Generic;
using System;
using FluentValidation;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public class ChildrenAgeBracketTest : ProfilOptionTestBase<ChildrenAgeBracket>
    {
        private ParentEspoirDbContext _context;
        public ChildrenAgeBracketTest() : base()
        {
            _context = GetDbContext();

            var customer = _context.Add(new Customer
            {
                CustomerDescription = new CustomerDescription()
            }).Entity;

            customer.CustomerActivations.Add(new CustomerActivation { IsActive = true, IsActiveSince = DateTime.Now });

            _context.SaveChanges();
        }

        [Fact]
        public override void CantDeleteRelatedData()
        {
            var entity = _context.Add(new ChildrenAgeBracket { Name = "RandomName" }).Entity;
            _context.SaveChanges();

            _context.CustomerChildrenAgeBrackets.Add(new CustomerChildrenAgeBracket
            {
                AgeBracket = entity,
                Customer = _context.CustomerDescriptions.First()
            });
            _context.SaveChanges();

            entity.CustomerChildrenAgeBrackets.Add(_context.CustomerChildrenAgeBrackets.First());

            _mediator.Send(new DeleteProfilOptionCommand<ChildrenAgeBracket> { Id = entity.Id })
                .ShouldThrow(typeof(ValidationException));
        }
    }
}