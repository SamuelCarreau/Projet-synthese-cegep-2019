using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;

namespace ParentEspoir.Application.Test
{
    public class StatisticTest : TestBase
    {
        private ParentEspoirDbContext _context;
        public StatisticTest()
        {
            _context = GetDbContext();
        }

        //[Fact]
        //public void GetStatisticTest()
        //{
        //    true.ShouldBe(false);
        //}

        //[Fact]
        //public void GetListStatisticTest()
        //{
        //    true.ShouldBe(false);
        //}

        //[Fact]
        //public void CreateStatisticTest()
        //{
        //    true.ShouldBe(false);
        //}

        //[Fact]
        //public void UpdateStatisticTest()
        //{
        //    true.ShouldBe(false);
        //}

        //[Fact]
        //public void DeleteStatisticTest()
        //{
        //    true.ShouldBe(false);
        //}

    }
}