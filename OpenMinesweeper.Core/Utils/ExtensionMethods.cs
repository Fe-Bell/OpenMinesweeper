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
    }
}
