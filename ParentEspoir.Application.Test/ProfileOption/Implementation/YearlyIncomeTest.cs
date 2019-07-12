using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System.Threading;
using System.Collections.Generic;
using MediatR;
using FluentValidation;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public class YearlyIncomeTest : ProfilOptionTestBase<YearlyIncome>
    {
        private ParentEspoirDbContext _context;

        public YearlyIncomeTest() : base()
        {
            _context = GetDbContext();

            var customer = _context.Add(new Customer
            {
                CustomerDescription = new CustomerDescription()
            }).Entity;

            customer.CustomerActivations.Add(new CustomerActivation());

            _context.SaveChanges();
        }

        [Fact]
        public override void CantDeleteRelatedData()
        {
            var yearlyIncome = _context.YearlyIncomes.First();

            yearlyIncome.CustomerDescriptions.Add(_context.CustomerDescriptions.First());

            _context.SaveChanges();

            var result = _mediator.Send(new DeleteProfilOptionCommand<YearlyIncome>
            {
                Id = yearlyIncome.Id
            }).ShouldThrow(typeof(ValidationException));
        }
    }
}