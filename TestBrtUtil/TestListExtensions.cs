using Brt.NetStandard.Util;
using System.Collections.Generic;
using Xunit;

namespace TestBrtUtil
{
    public class TestListExtensions
    {
        [Theory]
        [InlineData("600.049.326-68")]
        [InlineData(1)]
        [InlineData(2.45)]
        public void TestInList(object source)
        {
            var aux = source.InList();
            Assert.Single(aux);
        }

        [Theory]
        [InlineData(1, 3, 5, 7, 3)]
        [InlineData("a", "b", "c", "d", "e")]
        public void TestChunkBy(object a, object b, object c, object d, object e)
        {
            var auxList = new List<object> { a, b, c, d, e };
            var aux = auxList.ChunkBy(2);

            Assert.Equal(3, aux.Count);

            Assert.Equal(2, aux[0].Count);
            Assert.Equal(2, aux[1].Count);
            Assert.Single(aux[2]);
        }
    }
}
