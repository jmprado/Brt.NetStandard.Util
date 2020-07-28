using Brt.NetStandard.Util;
using Xunit;

namespace TestBrtUtil
{
    public class TesteValidaCpf
    {
        [Theory]
        [InlineData("600.049.326-68")]
        [InlineData("414.574.790-95")]
        public void TestCpfValido(string source)
        {
            Assert.True(ValidaCpf.Validate(source));
        }


        [Theory]
        [InlineData("600.049.326-69")]
        [InlineData("414.574.790-9A")]
        public void TestCpfInValido(string source)
        {
            Assert.False(ValidaCpf.Validate(source));
        }
    }
}
