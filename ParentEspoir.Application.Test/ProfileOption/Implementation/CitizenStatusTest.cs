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
    public class CitizenStatusTest : ProfilOptionTestBase<CitizenStatus>
    {
        private ParentEspoirDbContext _context;
        public CitizenStatusTest() : base()
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
            var entity = _context.Add(new CitizenStatus
            {
                Name = "CantBeDeleted"
            }).Entity;

            entity.CustomerDescriptions.Add(_context.CustomerDescriptions.First());

            _context.SaveChanges();

            _mediator.Send(new DeleteProfilOptionCommand<CitizenStatus>
            {
                Id = entity.Id
            }).ShouldThrow(typeof(ValidationException));
        }
    }
}