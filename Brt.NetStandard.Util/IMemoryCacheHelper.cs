using System;
using System.Threading.Tasks;

namespace Brt.NetStandard.Util
{
    public interface IMemoryCacheHelper<TItem>
    {
        Task<TItem> GetOrCreateAsync(object key, Func<Task<TItem>> createItem, DateTime dateExpiration);
        Task ClearCache(object key);
    }
}
