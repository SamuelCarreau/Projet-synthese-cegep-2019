using ParentEspoir.Domain.Enums;
using Xunit;

namespace ParentEspoir.Domain.Test
{
    public class SeasonTest
    {
        [Fact]
        public void InvalideValueIsZero()
        {
            Assert.Equal(Season.None, (Season)0);
        }

        [Fact]
        public void ToStringOfSeasonIsValid()
        {
            Assert.Equal("Spring", Season.Spring.ToString());
        }

        [Fact]
        public void IntValueInOrder()
        {
            Assert.Equal(2, (int)Season.Winter);
            Assert.Equal(3, (int)Season.Spring);
            Assert.Equal(4, (int)Season.Summer);
            Assert.Equal(1, (int)Season.Fall);
        }
    }
}
