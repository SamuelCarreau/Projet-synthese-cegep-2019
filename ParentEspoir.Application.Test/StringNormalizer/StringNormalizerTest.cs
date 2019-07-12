using System;
using System.Collections.Generic;
using System.Text;
using ParentEspoir.Application;
using Shouldly;
using Xunit;

namespace ParentEspoir.Application
{
    public class StringNormalizerTest
    {

        [Theory]
        [InlineData("        ")]
        [InlineData("123456789")]
        [InlineData("#!/$%?&*()_+.,`<¸^;")]
        [InlineData(null)]
        public void TestShouldBeBlank(string text)
        {
            string result = StringNormalizer.Normalize(text);

            result.ShouldBe("");
        }

        [Fact]
        public void TestShouldBeQUEBEC()
        {
            string result = StringNormalizer.Normalize("Québec");

            result.ShouldBe("QUEBEC");
        }

        [Fact]
        public void TestShouldBeSAINTALFRED()
        {
            string result = StringNormalizer.Normalize("Saint-Alfred");

            result.ShouldBe("SAINTALFRED");
        }

        [Fact]
        public void TestShouldBeLESBOULES()
        {
            string result = StringNormalizer.Normalize(" les Boules ^^ :)");

            result.ShouldBe("LESBOULES");
        }

        [Fact]
        public void TestShouldC()
        {
            string result = StringNormalizer.Normalize("ç");

            result.ShouldBe("C");
        }

    }
}
