using System.Collections.Generic;
using System.Linq;

namespace Brt.NetStandard.Util
{
    public static class ListExtensions
    {
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static List<T> InList<T>(this T item)
        {
            return new List<T> { item };
        }
    }
}
