using System.Collections.Generic;

namespace SierotkiCore.Extensions
{
    public static class EnumerableExtensions
    {
        public static async IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                yield return item;
            }
        }
    }
}
