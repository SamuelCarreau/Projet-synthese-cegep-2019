using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;

namespace ParentEspoir.Application.Test
{
    public class ObjectiveTest : TestBase
    {
        private ParentEspoirDbContext _context;
        public ObjectiveTest()
        {
            _context = GetDbContext();
        }

        //[Fact]
        //public void GetObjectiveTest()
        //{
        //    true.ShouldBe(false);
        //}

        //[Fact]
        //public void GetListObjectiveTest()
        //{
        //    true.ShouldBe(false);
        //}

        //[Fact]
        //public void CreateObjectiveTest()
        //{
        //    true.ShouldBe(false);
        //}

        //[Fact]
        //public void UpdateObjectiveTest()
        //{
        //    true.ShouldBe(false);
        //}

        //[Fact]
        //public void DeleteObjectiveTest()
        //{
        //    true.ShouldBe(false);
        //}

    }
}