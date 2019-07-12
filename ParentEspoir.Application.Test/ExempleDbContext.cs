using ParentEspoir.Persistence;
using ParentEspoir.Domain.Entities;
using System.Linq;
using Xunit;
using Shouldly;
using System;

namespace ParentEspoir.Application.Test
{
    public class ExempleDbContext : TestBase
    {
        private static readonly string NAME = "NAME";

        private ParentEspoirDbContext _context;

        public ExempleDbContext()
        {
            _context = GetDbContext();
        }

        [Fact]
        public void Test1()
        {
            _context.Add(new FamilyType { Name = NAME });
            _context.SaveChanges();

            var familytype = _context.FamilyTypes.SingleOrDefault(x => x.Name == NAME);

            familytype.ShouldBeOfType<FamilyType>();
            familytype.Name.ShouldBe(NAME);
        }

        [Fact]
        public void Test2()
        {
            var familytype = _context.FamilyTypes.SingleOrDefault(x => x.Name == NAME);

            familytype.ShouldBe(null);
        }
    }
}
