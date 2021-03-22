using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Numerics;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class MathExtensionsTest
    {
        private static readonly double[] _EvenSample;
        private const double _EvenSampleLowerQuartile = -8.78;
        private const double _EvenSampleMean = -4.01875;
        private const double _EvenSampleMedian = 3.92;
        private const double _EvenSampleMode = 4.37;
        private const double _EvenSampleUpperQuartile = 8.7;
        private const double _EvenSampleVariance = 422.898184;
        private static readonly double[] _OddSample;
        private const double _OddSampleLowerQuartile = -8.78;
        private const double _OddSampleMean = 1.0166666666667;
        private const double _OddSampleMedian = 7.8;
        private const double _OddSampleMode = 4.37;
        private const double _OddSampleUpperQuartile = 9.285;
        private const double _OddSampleVariance = 531.76417777778;

        static MathExtensionsTest()
        {
            MathExtensionsTest._EvenSample = new double[] { -17.6, 9.87, -49.7, 0.04, 0.04, 8.7, 7.8, 8.7 };
            MathExtensionsTest._OddSample = new double[MathExtensionsTest._EvenSample.Length + 1];
            MathExtensionsTest._OddSample[0] = 41.3;
            Array.Copy(MathExtensionsTest._EvenSample, 0, MathExtensionsTest._OddSample, 1, MathExtensionsTest._EvenSample.Length);
        }

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
        public void TestMean_NotEmpty_ValidSample()
        {
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._EvenSampleMean, MathExtensions.Mean(MathExtensionsTest._EvenSample)));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._OddSampleMean, MathExtensions.Mean(MathExtensionsTest._OddSample)));
        }

        [TestMethod]
        public void TestMean_Null() => Assert.ThrowsException<ArgumentNullException>(() => MathExtensions.Mean(null));

        [TestMethod]
        public void TestMode_Empty() => Assert.AreEqual(0, MathExtensions.Mode(Array.Empty<double>()));

        [TestMethod]
        public void TestMode_NotEmpty_AllZeroes()
        {
            double[] array = new double[10];
            Assert.AreEqual(0, MathExtensions.Mode(array));
        }

        [TestMethod]
        public void TestMode_NotEmpty_ValidSample()
        {
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._EvenSampleMode, MathExtensions.Mode(MathExtensionsTest._EvenSample)));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._OddSampleMode, MathExtensions.Mode(MathExtensionsTest._OddSample)));
        }

        [TestMethod]
        public void TestMode_Null() => Assert.ThrowsException<ArgumentNullException>(() => MathExtensions.Mode(null));
    }
}