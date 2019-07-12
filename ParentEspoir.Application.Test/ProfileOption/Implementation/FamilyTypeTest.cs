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
using System.ComponentModel.DataAnnotations;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public class FamilyTypeTest : ProfilOptionTestBase<FamilyType>
    {
        private ParentEspoirDbContext _context;
        public FamilyTypeTest() : base()
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
            //var entity = new FamilyType { Name = "testTrue" };

            //entity.CustomerDescriptions.Add(_context.CustomerDescriptions.First());

            //_context.Add(entity);

            //_context.SaveChanges();

            //var result = _mediator.Send(new DeleteProfilOptionCommand<FamilyType> { Id = entity.Id })
            //    .ShouldThrow(typeof(ValidationException));
        }
    }
}