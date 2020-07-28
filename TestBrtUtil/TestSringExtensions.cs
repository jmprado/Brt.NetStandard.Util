using Brt.NetStandard.Util;
using System;
using Xunit;

namespace TestBrtUtil
{
    public class TestSringExtensions
    {
        [Theory]
        [InlineData("JOÃO", "JOAO")]
        [InlineData("CARROÇA", "CARROCA")]
        [InlineData("TÁBUA", "TABUA")]
        public void TestRemoveDiacritics(string source, string expected)
        {
            var aux = source.RemoveDiacritics();
            Assert.Equal(expected, aux);
        }

        [Theory]
        [InlineData("13/03/1971")]
        [InlineData("1971-03-13")]
        [InlineData("13/03/1971 01:00")]
        [InlineData("1971-03-13 01:00")]
        public void TestDateConversion(string value)
        {
            var aux = value.ParseDate();
            Assert.IsType<DateTime>(aux);
        }

        [Fact]
        public void TestDateConversionNullString()
        {
            string auxString = null;
            var result = auxString.ParseDate();

            Assert.Null(result);
        }

        [Theory]
        [InlineData("600.049.326-68", "60004932668")]
        [InlineData("1.300,00", "130000")]
        public void TestRemoveSpecialChar(string source, string expected)
        {
            var aux = source.RemoveSpecialCharacters();
            Assert.Equal(expected, aux);
        }
    }
}
