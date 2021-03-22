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
        private static double _Mean(IEnumerable<double> collection) => MathExtensions._Mean(collection, out int n);

        private static double _Mean(IEnumerable<double> collection, out int n)
        {
            double sum = 0;
            n = 0;

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
        /// Calculates the median average of values in the specified sorted collection.
        /// </summary>
        /// <param name="collection">An <see cref="IEnumerable{T}" /> of values to calculate the median average from. The collection must be sorted.</param>
        /// <returns>The median average (middle) of values in the <c><paramref name="collection" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="collection" /></c> is <c>null</c>.</exception>
        public static double Median(IEnumerable<double> collection)
        {
            IEnumerable<double> median = EnumerableExtensions.Median<double>(collection);
            return MathExtensions._Mean(median);
        }

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

        /// <summary>
        /// Calculates the population variance of values in the specified collection.
        /// </summary>
        /// <param name="collection">An <see cref="IEnumerable{T}" /> of values to calculate the population variance from.</param>
        /// <returns>The population variance of values in the <c><paramref name="collection" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="collection" /></c> is <c>null</c>.</exception>
        public static double Variance(IEnumerable<double> collection) => MathExtensions.Variance(collection, out double mean);

        /// <summary>
        /// Calculates the population variance of values in the specified collection using the pre-calculated mean. You should only use this method if the mean is known.
        /// </summary>
        /// <param name="collection">An <see cref="IEnumerable{T}" /> of values to calculate the population variance from.</param>
        /// <param name="mean">The pre-calculated mean average of <c><paramref name="collection" /></c>. If the value is inaccurate, the result will be incorrect.</param>
        /// <returns>The population variance of values in the <c><paramref name="collection" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="collection" /></c> is <c>null</c>.</exception>
        public static double Variance(IEnumerable<double> collection, double mean)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof (collection));
            }

            double sum = 0;
            int n = 0;

            foreach (double item in collection)
            {
                double diff = item - mean;
                sum += diff * diff;
                ++ n;
            }

            return n == 0 ? 0 : sum / n;
        }

        /// <summary>
        /// Calculates the population variance of values in the specified collection.
        /// </summary>
        /// <param name="collection">An <see cref="IEnumerable{T}" /> of values to calculate the population variance from.</param>
        /// <param name="mean">The mean average of values in the <c><paramref name="collection" /></c>. This parameter is passed uninitialized.</param>
        /// <returns>The population variance of values in the <c><paramref name="collection" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="collection" /></c> is <c>null</c>.</exception>
        public static double Variance(IEnumerable<double> collection, out double mean)
        {
            mean = MathExtensions._Mean(collection ?? throw new ArgumentNullException(nameof (collection)), out int n);

            if (n == 0)
            {
                return 0;
            }

            double sum = 0;

            foreach (double item in collection)
            {
                double diff = item - mean;
                sum += diff * diff;
            }

            return sum / n;
        }
    }
}