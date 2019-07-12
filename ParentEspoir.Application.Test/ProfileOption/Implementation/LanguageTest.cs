using Xunit;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using Shouldly;
using FluentValidation;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public class LanguageTest : ProfilOptionTestBase<Language>
    {
        private readonly ParentEspoirDbContext _context;

        public LanguageTest() : base()
        {
            _context = GetDbContext();
        }

        [Fact]
        public override void CantDeleteRelatedData()
        {
            var language = _context.Add(new Language { Name = "Testing" }).Entity;

            var customer = _context.Add(new Customer
            {
                CustomerDescription = new CustomerDescription()
            }).Entity;

            customer.CustomerActivations.Add(new CustomerActivation());

            language.CustomerDescriptions.Add(customer.CustomerDescription);

            _context.SaveChanges();

            _mediator.Send(new DeleteProfilOptionCommand<Language> { Id = language.Id })
                .ShouldThrow(typeof(ValidationException));
        }
    }
}