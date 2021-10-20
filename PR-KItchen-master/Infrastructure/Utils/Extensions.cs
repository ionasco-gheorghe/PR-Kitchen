using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kitchen.Infrastructure.Utils
{
    public static class Extensions
    {
        public static void Sort<T>(this SynchronizedCollection<T> collection, Comparison<T> comparer)
        {
            var list = collection.ToList();
            list.Sort(comparer);
            collection = new SynchronizedCollection<T>();
            list.ForEach(i => collection.Add(i));
        }
    }
}
