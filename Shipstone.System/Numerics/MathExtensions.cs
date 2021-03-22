using System;
using System.Collections.Generic;
using System.Numerics;

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
        /// Solves for x the quadratic equation of the form <c><paramref name="a" />x ^ 2 + <paramref name="b" />x + <paramref name="c" /> = 0 </c>.
        /// </summary>
        /// <param name="a">The quadratic coefficient of the equation.</param>
        /// <param name="b">The linear coefficient of the equation.</param>
        /// <param name="c">The constant coefficient of the equation.</param>
        /// <param name="x1">The lesser root of the equation.</param>
        /// <param name="x2">The greater root of the equation.</param>
        /// <returns>2 if the equation has two distinct, real roots; 1 if the equation has two repeated roots; otherwise, 0 if the equation has two complex roots.</returns>
        /// <exception cref="ArgumentException"><c><paramref name="a" /></c> is equal to 0.</exception>
        public static int SolveQuadratic(double a, double b, double c, out Complex x1, out Complex x2)
        {
            if (a == 0)
            {
                throw new ArgumentException("a is equal to 0.");
            }

            Decimal adbl = Convert.ToDecimal(a), bdbl = Convert.ToDecimal(b), cdbl = Convert.ToDecimal(c), a2 = 2 * adbl, minusB = bdbl * -1, disc = bdbl * bdbl - 4 * adbl * cdbl, discSqrt;
            int compar = disc.CompareTo(0);

            if (compar > 0)
            {
                discSqrt = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(disc)));
                x1 = new Complex(Convert.ToDouble((minusB - discSqrt) / a2), 0);
                x2 = new Complex(Convert.ToDouble((minusB + discSqrt) / a2), 0);
                return 2;
            }

            double real = Convert.ToDouble(minusB / a2);

            if (compar == 0)
            {
                x1 = x2 = new Complex(real, 0);
                return 1;
            }

            discSqrt = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(disc * -1)));
            double imag = Convert.ToDouble(discSqrt / a2);
            x1 = new Complex(real, -1 * imag);
            x2 = new Complex(real, imag);
            return 0;
        }

        /// <summary>
        /// Solves for x the quadratic equation of the form <c><paramref name="a" />x ^ 2 + <paramref name="b" />x + <paramref name="c" /> = 0 </c>.
        /// </summary>
        /// <param name="a">The quadratic coefficient of the equation.</param>
        /// <param name="b">The linear coefficient of the equation.</param>
        /// <param name="c">The constant coefficient of the equation.</param>
        /// <param name="x1">The lesser root of the equation.</param>
        /// <param name="x2">The greater root of the equation.</param>
        /// <returns><c>true</c> if the equation has one or more real roots; otherwise, <c>false</c> if the equation has only complex roots.</returns>
        /// <exception cref="ArgumentException"><c><paramref name="a" /></c> is equal to 0.</exception>
        public static bool TrySolveQuadratic(double a, double b, double c, out double x1, out double x2)
        {
            if (MathExtensions.SolveQuadratic(a, b, c, out Complex cmplx1, out Complex cmplx2) == 0)
            {
                x1 = x2 = 0;
                return false;
            }

            x1 = cmplx1.Real;
            x2 = cmplx2.Real;
            return true;
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