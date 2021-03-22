using System;
using System.Collections.Generic;

using Shipstone.System.Collections;

namespace Shipstone.System.Numerics
{
    /// <summary>
    /// Provides static methods for common mathematical and statistical functions.
    /// </summary>
    public static class MathExtensions
    {
        private static double _Mean(IEnumerable<double> collection)
        {
            double sum = 0;
            int n = 0;

            foreach (double item in collection)
            {
                sum += item;
                ++ n;
            }

            return n == 0 ? 0 : sum / n;
        }

        /// <summary>
        /// Calculates the mean average of values in the specified collection.
        /// </summary>
        /// <param name="collection">An <see cref="IEnumerable{T}" /> of values to calculate the mean average from.</param>
        /// <returns>The mean average of values in the <c><paramref name="collection" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="collection" /></c> is <c>null</c>.</exception>
        public static double Mean(IEnumerable<double> collection) => MathExtensions._Mean(collection ?? throw new ArgumentNullException(nameof (collection)));

        /// <summary>
        /// Calculates the modal average of values in the specified collection.
        /// </summary>
        /// <param name="collection">An <see cref="IEnumerable{T}" /> of values to calculate the modal average from.</param>
        /// <returns>The modal average (most occurring) of values in the <c><paramref name="collection" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="collection" /></c> is <c>null</c>.</exception>
        public static double Mode(IEnumerable<double> collection)
        {
            FrequencyTable<double> table = new FrequencyTable<double>(collection);
            return table.Count == 0 ? 0 : MathExtensions._Mean(table.GetMax());
        }
    }
}