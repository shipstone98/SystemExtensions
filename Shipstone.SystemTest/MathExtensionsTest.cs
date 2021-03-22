using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Numerics;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class MathExtensionsTest
    {
        private static readonly double[] _Sample;
        private const double _SampleLowerQuartile = -8.78;
        private const double _SampleMean = 1.0166666666667;
        private const double _SampleMedian = 7.8;
        private const double _SampleMode = 4.37;
        private const double _SampleUpperQuartile = 9.285;
        private const double _SampleVariance = 531.76417777778;

        static MathExtensionsTest() => MathExtensionsTest._Sample = new double[] { 41.3, -17.6, 9.87, -49.7, 0.04, 0.04, 8.7, 7.8, 8.7 };

        internal static int _CompareDouble(double a, double b)
        {
            if (a == b)
            {
                return 0;
            }

            const double TOLERANCE = 0.00001;
            double margin = TOLERANCE;
            bool aGreater = false;

            if (a < b)
            {
                margin *= a;
            }

            else
            {
                margin *= b;
                aGreater = true;
            }

			return Math.Abs(a - b) <= Math.Abs(margin) ? 0 : aGreater ? 1 : -1;
		}

        [TestMethod]
        public void TestMean_Empty() => Assert.AreEqual(0, MathExtensions.Mean(Array.Empty<double>()));

        [TestMethod]
        public void TestMean_NotEmpty_AllZeroes()
        {
            double[] array = new double[10];
            Assert.AreEqual(0, MathExtensions.Mean(array));
        }

        [TestMethod]
        public void TestMean_NotEmpty_ValidSample() => Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._SampleMean, MathExtensions.Mean(MathExtensionsTest._Sample)));

        [TestMethod]
        public void TestMean_Null() => Assert.ThrowsException<ArgumentNullException>(() => MathExtensions.Mean(null));
    }
}