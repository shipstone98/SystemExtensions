using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Numerics;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class MathExtensionsTest
    {
#region Fields
        private const double _EvenSampleLowerQuartile = -8.78;
        private const double _EvenSampleMean = -4.01875;
        private const double _EvenSampleMedian = 3.92;
        private const double _EvenSampleMode = 4.37;
        private const double _EvenSampleUpperQuartile = 8.7;
        private const double _EvenSampleVariance = 370.0359125;
        private const double _OddSampleLowerQuartile = -8.78;
        private const double _OddSampleMean = 1.0166666666667;
        private const double _OddSampleMedian = 7.8;
        private const double _OddSampleMode = 4.37;
        private const double _OddSampleUpperQuartile = 9.285;
        private const double _OddSampleVariance = 531.76417777778;

        private static readonly double[] _EvenSample;
        private static readonly double[] _OddSample;
#endregion

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

#region Mean tests
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
#endregion

#region Median tests
        [TestMethod]
        public void TestMedian_Empty() => Assert.AreEqual(0, MathExtensions.Median(Array.Empty<double>()));

        [TestMethod]
        public void TestMedian_NotEmpty_AllZeroes()
        {
            double[] array = new double[10];
            Assert.AreEqual(0, MathExtensions.Median(array));
        }

        [TestMethod]
        public void TestMedian_NotEmpty_ValidSample()
        {
            Array.Sort(MathExtensionsTest._EvenSample);
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._EvenSampleMedian, MathExtensions.Median(MathExtensionsTest._EvenSample)));
            Array.Sort(MathExtensionsTest._OddSample);
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._OddSampleMedian, MathExtensions.Median(MathExtensionsTest._OddSample)));
        }

        [TestMethod]
        public void TestMedian_Null() => Assert.ThrowsException<ArgumentNullException>(() => MathExtensions.Median(null));
#endregion

#region Mode tests
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
#endregion

#region Quadratic functionality
        [TestMethod]
        public void TestSolveQuadratic_AllZeroes()
        {
            Exception ex = Assert.ThrowsException<ArgumentException>(() => MathExtensions.SolveQuadratic(0, 0, 0, out Complex x1, out Complex x2));
            Assert.AreEqual("a is equal to 0.", ex.Message);
        }

        [TestMethod]
        public void TestSolveQuadratic_DistinctRoots()
        {
            const double A = 23.7, B = 14.5, C = -67.9;
            Assert.AreEqual(2, MathExtensions.SolveQuadratic(A, B, C, out Complex x1, out Complex x2));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(-2.0259531321038, x1.Real));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(1.4141387861122, x2.Real));
            Assert.AreEqual(0, x1.Imaginary);
            Assert.AreEqual(0, x2.Imaginary);
        }

        [TestMethod]
        public void TestSolveQuadratic_RepeatedRoots()
        {
            const double A = 0.4, B = 1.2, C = 0.9;
            Assert.AreEqual(1, MathExtensions.SolveQuadratic(A, B, C, out Complex x1, out Complex x2));
            const double ROOT = -1.5;
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(ROOT, x1.Real));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(ROOT, x2.Real));
            Assert.AreEqual(0, x1.Imaginary);
            Assert.AreEqual(0, x2.Imaginary);
        }

        [TestMethod]
        public void TestSolveQuadratic_ImaginaryRoots()
        {
            const double A = 73.77, B = -14.999, C = 3.71;
            Assert.AreEqual(0, MathExtensions.SolveQuadratic(A, B, C, out Complex x1, out Complex x2));
            const double REAL = 0.101660566626, IMAG = 0.199891409472;
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(REAL, x1.Real));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(REAL, x2.Real));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(IMAG * -1, x1.Imaginary));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(IMAG, x2.Imaginary));
        }

        [TestMethod]
        public void TestTrySolveQuadratic_AllZeroes()
        {
            Exception ex = Assert.ThrowsException<ArgumentException>(() => MathExtensions.TrySolveQuadratic(0, 0, 0, out double x1, out double x2));
            Assert.AreEqual("a is equal to 0.", ex.Message);
        }

        [TestMethod]
        public void TestTrySolveQuadratic_DistinctRoots()
        {
            const double A = 23.7, B = 14.5, C = -67.9;
            Assert.IsTrue(MathExtensions.TrySolveQuadratic(A, B, C, out double x1, out double x2));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(-2.0259531321038, x1));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(1.4141387861122, x2));
        }

        [TestMethod]
        public void TestTrySolveQuadratic_RepeatedRoots()
        {
            const double A = 0.4, B = 1.2, C = 0.9;
            Assert.IsTrue(MathExtensions.TrySolveQuadratic(A, B, C, out double x1, out double x2));
            const double ROOT = -1.5;
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(ROOT, x1));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(ROOT, x2));
        }

        [TestMethod]
        public void TestTrySolveQuadratic_ImaginaryRoots()
        {
            const double A = 73.77, B = -14.999, C = 3.71;
            Assert.IsFalse(MathExtensions.TrySolveQuadratic(A, B, C, out double x1, out double x2));
        }
#endregion

#region Variance tests
        [TestMethod]
        public void TestVariance_Empty()
        {
            Assert.AreEqual(0, MathExtensions.Variance(Array.Empty<double>()));
            Assert.AreEqual(0, MathExtensions.Variance(Array.Empty<double>(), 0));
            Assert.AreEqual(0, MathExtensions.Variance(Array.Empty<double>(), out double mean));
            Assert.AreEqual(0.0, mean);
        }

        [TestMethod]
        public void TestVariance_NotEmpty_AllZeroes()
        {
            double[] array = new double[10];
            Assert.AreEqual(0, MathExtensions.Variance(array));
            Assert.AreEqual(0, MathExtensions.Variance(array, 0));
            Assert.AreEqual(0, MathExtensions.Variance(array, out double mean));
            Assert.AreEqual(0.0, mean);
        }

        [TestMethod]
        public void TestVariance_NotEmpty_ValidSample()
        {
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._EvenSampleVariance, MathExtensions.Variance(MathExtensionsTest._EvenSample)));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._OddSampleVariance, MathExtensions.Variance(MathExtensionsTest._OddSample)));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._EvenSampleVariance, MathExtensions.Variance(MathExtensionsTest._EvenSample, MathExtensionsTest._EvenSampleMean)));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._OddSampleVariance, MathExtensions.Variance(MathExtensionsTest._OddSample, MathExtensionsTest._OddSampleMean)));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._EvenSampleVariance, MathExtensions.Variance(MathExtensionsTest._EvenSample, out double mean)));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._EvenSampleMean, mean));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._OddSampleVariance, MathExtensions.Variance(MathExtensionsTest._OddSample, out mean)));
            Assert.AreEqual(0, MathExtensionsTest._CompareDouble(MathExtensionsTest._OddSampleMean, mean));
        }

        [TestMethod]
        public void TestVariance_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => MathExtensions.Variance(null));
            Assert.ThrowsException<ArgumentNullException>(() => MathExtensions.Variance(null, 0));
            Assert.ThrowsException<ArgumentNullException>(() => MathExtensions.Variance(null, out double mean));
        }
#endregion
    }
}