using OpenMinesweeper.Core.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenMinesweeper.Core.Utils
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Enables "foreach" loops for IEnumerables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var cur in enumerable)
            {
                action(cur);
            }
        }
        public static ObservableDictionary<TKey, TSource> ToObservableDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return ToObservableDictionary<TSource, TKey, TSource>(source, keySelector, ObservableDictionary<TKey, TSource>.IdentityFunction<TSource>.Instance, null);
        }
        public static ObservableDictionary<TKey, TSource> ToObservableDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return ToObservableDictionary<TSource, TKey, TSource>(source, keySelector, ObservableDictionary<TKey, TSource>.IdentityFunction<TSource>.Instance, comparer);
        }
        public static ObservableDictionary<TKey, TElement> ToObservableDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return ToObservableDictionary<TSource, TKey, TElement>(source, keySelector, elementSelector, null);
        }
        public static ObservableDictionary<TKey, TElement> ToObservableDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (elementSelector == null) throw new ArgumentNullException("elementSelector");

            ObservableDictionary<TKey, TElement> d = new ObservableDictionary<TKey, TElement>(comparer);
            foreach (TSource element in source) d.Add(keySelector(element), elementSelector(element));

            return d;
        }
    }
}
