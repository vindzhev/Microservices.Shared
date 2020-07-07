namespace MicroservicesPOC.Shared.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
                return;

            foreach (T item in items)
                action(item);
        }
    }
}
