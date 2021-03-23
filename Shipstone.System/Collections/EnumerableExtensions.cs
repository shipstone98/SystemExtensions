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
        private static int _CompareTo<T>(this IEnumerable<T> source, IEnumerable<T> collection, IComparer<T> comparer) where T : IComparable<T>
        {
            if (collection is null)
            {
                return 1;
            }

            using (IEnumerator<T> sourceEnumerator = source.GetEnumerator())
            {
                using (IEnumerator<T> collectionEnumerator = collection.GetEnumerator())
                {
                    while (sourceEnumerator.MoveNext())
                    {
                        if (!collectionEnumerator.MoveNext())
                        {
                            return 1;
                        }
                        
                        int compar = comparer.Compare(sourceEnumerator.Current, collectionEnumerator.Current);

                        if (compar != 0)
                        {
                            return compar;
                        }
                    }

                    if (collectionEnumerator.MoveNext())
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Compares all elements in this collection with the specified <see cref="IEnumerable{T}" /> using the default comparer for type <c><typeparamref name="T" /></c>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">A sequence of <c><typeparamref name="T" /></c> elements to compare with <c><paramref name="collection" /></c>.</param>
        /// <param name="collection">An <see cref="IEnumerable{T}" /> to compare with the current instance.</param>
        /// <returns>A signed number indicating the relative elements of this instance and <c><paramref name="collection" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="source" /></c> is <c>null</c>.</exception>
        public static int CompareTo<T>(this IEnumerable<T> source, IEnumerable<T> collection) where T : IComparable<T> => source is null ? throw new ArgumentNullException(nameof (source)) : EnumerableExtensions._CompareTo(source, collection, Comparer<T>.Default);

        /// <summary>
        /// Compares all elements in this collection with the specified <see cref="IEnumerable{T}" /> using the specified <see cref="IComparer{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">A sequence of <c><typeparamref name="T" /></c> elements to compare with <c><paramref name="collection" /></c>.</param>
        /// <param name="collection">An <see cref="IEnumerable{T}" /> to compare with the current instance.</param>
        /// <param name="comparer">An <see cref="IComparer{T}" /> used to compare elements, or <c>null</c> to use <see cref="Comparer{T}.Default" />.</param>
        /// <returns>A signed number indicating the relative elements of this instance and <c><paramref name="collection" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="source" /></c> is <c>null</c>.</exception>
        public static int CompareTo<T>(this IEnumerable<T> source, IEnumerable<T> collection, IComparer<T> comparer) where T : IComparable<T> => source is null ? throw new ArgumentNullException(nameof (source)) : EnumerableExtensions._CompareTo(source, collection, comparer ?? Comparer<T>.Default);

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