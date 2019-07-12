using ParentEspoir.Persistence;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using FluentValidation;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public class VolunteeringTypeTest : ProfilOptionTestBase<VolunteeringType>
    {
        private readonly ParentEspoirDbContext _context;

        public VolunteeringTypeTest() : base()
        {
            _context = GetDbContext();
        }

        [Fact]
        public override void CantDeleteRelatedData()
        {
            var volonteeringType = new VolunteeringType { Name = "test" };

            _context.Add(new Volunteering
            {
                Type = volonteeringType
            });

            _context.SaveChanges();

            _mediator.Send(new DeleteProfilOptionCommand<VolunteeringType> { Id = volonteeringType.Id })
                .ShouldThrow(typeof(ValidationException));
        }
    }
}