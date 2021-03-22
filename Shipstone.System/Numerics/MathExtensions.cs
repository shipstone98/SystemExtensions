using System;
using System.Collections.Generic;

namespace Shipstone.System.Numerics
{
    /// <summary>
    /// Provides static methods for common mathematical and statistical functions.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Calculates the mean average of values in the specified collection.
        /// </summary>
        /// <param name="collection">An <see cref="IEnumerable{T}" /> of values to calculate the mean average from.</param>
        /// <returns>The mean average of values in the <c><paramref name="collection" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="collection" /></c> is <c>null</c>.</exception>
        public static double Mean(IEnumerable<double> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof (collection));
            }

            double sum = 0;
            int n = 0;

            foreach (double item in collection)
            {
                sum += item;
                ++ n;
            }

            return n == 0 ? 0 : sum / n;
        }
    }
}