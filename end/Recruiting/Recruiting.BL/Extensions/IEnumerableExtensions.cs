using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> SortList<TSource>(this IEnumerable<TSource> list, bool isDescending, Func<TSource, string> orderFunction)
        {
            if (isDescending)
            {
                return list.OrderByDescending(orderFunction);
            }
            else
            {
                return list.OrderBy(orderFunction);
            }
        }

        public static (IEnumerable<TSource>, int) GetPageElements<TSource>(this IEnumerable<TSource> list, int indexPage, int itemsPerPage)
        =>
            (list
                .Skip((indexPage - 1) * itemsPerPage)
                .Take(itemsPerPage),
            list.Count());
    }
}
