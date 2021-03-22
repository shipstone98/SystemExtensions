using System;
using System.Collections.Generic;
using System.Linq;

namespace Shipstone.System.Collections
{
    /// <summary>
    /// Provides a set of <c>static</c> (<c>Shared</c> in Visual Basic) methods for querying objects that implement <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the median value(s) in the specified sorted collection.
        /// </summary>
        /// <typeparam name="T">The type of elements of <c><paramref name="source" /></c>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> that contains the elements to retrieve the median value(s) from. The collection must be sorted.</param>
        /// <returns>The median value(s) in the <c><paramref name="source" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="source" /></c> is <c>null</c>.</exception>
        public static IEnumerable<T> Median<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof (source));
            }

            int count = source.Count();

            if (count == 0)
            {
                return Array.Empty<T>();
            }

            int half = count / 2;

            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                for (int i = 0; i < half; i ++)
                {
                    enumerator.MoveNext();
                }

                if (count % 2 == 0)
                {
                    T[] array = new T[2];
                    array[0] = enumerator.Current;
                    enumerator.MoveNext();
                    array[1] = enumerator.Current;
                    return array;
                }

                enumerator.MoveNext();
                return new T[1] { enumerator.Current };
            }
        }
    }
}