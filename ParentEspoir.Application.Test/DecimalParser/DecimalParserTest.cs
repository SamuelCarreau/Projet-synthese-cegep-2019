using Shouldly;
using System;
using Xunit;

namespace ParentEspoir.Application.TestDecimalParser
{
    public class DecimalParserTest
    {
        [Theory]
        [InlineData("3", true)]
        [InlineData("7384578", true)]
        [InlineData("00123", true)]
        [InlineData(".12", true)]
        [InlineData("12.12", true)]
        [InlineData("12,12", true)]
        [InlineData(",000", true)]
        [InlineData(".00100", true)]
        [InlineData("12,000", true)]
        [InlineData("-1.34", true)]
        [InlineData("hello", false)]
        [InlineData("12.12$", false)]
        [InlineData("", false)]
        [InlineData(",", false)]
        [InlineData(".", false)]
        [InlineData(",,", false)]
        [InlineData("..", false)]
        public void CanParseTest(string text, bool canParse)
        {
            DecimalParser.CanParse(text).ShouldBe(canParse);
        }

        [Theory]
        [InlineData("3.45", 3.45)]
        [InlineData("0,45", 0.45)]
        [InlineData("1000,00", 1000)]
        [InlineData("45", 45)]
        [InlineData("-1.34", -1.34)]
        [InlineData("  3  ", 3)]
        [InlineData("00123", 123)]
        [InlineData("0", 0)]
        [InlineData(",12", 0.12)]
        public void ParseTest(string text, double expected)
        {
            DecimalParser.Parse(text).ShouldBe((decimal)expected);
        }
    }
}
