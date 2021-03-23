using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Collections;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class EnumerableExtensionsTest
    {
        private static readonly String[] _Sample;

        static EnumerableExtensionsTest() => EnumerableExtensionsTest._Sample = new String[] { "DDD", "FFF", "CCC", "HHH", "AAA", "BBB", "GGG", "EEE" };

        [TestMethod]
        public void TestCompareTo_BothEmpty()
        {
            String[] empty1 = Array.Empty<String>(), empty2 = Array.Empty<String>();
            Assert.AreEqual(0, empty1.CompareTo(empty2));
            Assert.AreEqual(0, empty2.CompareTo(empty1));
        }
        
        [TestMethod]
        public void TestCompareTo_BothNull()
        {
            String[] nullArray1 = null, nullArray2 = null;
            Assert.ThrowsException<ArgumentNullException>(() => nullArray1.CompareTo(nullArray2));
            Assert.ThrowsException<ArgumentNullException>(() => nullArray2.CompareTo(nullArray1));
        }

        [TestMethod]
        public void TestCompareTo_BothNotEmpty_Equal()
        {
            String[] array = new String[EnumerableExtensionsTest._Sample.Length];
            Array.Copy(EnumerableExtensionsTest._Sample, array, array.Length);
            Assert.AreEqual(0, EnumerableExtensionsTest._Sample.CompareTo(array));
            Assert.AreEqual(0, array.CompareTo(EnumerableExtensionsTest._Sample));
        }

        [TestMethod]
        public void TestCompareTo_BothNotEmpty_NotEqual()
        {
            String[] sortedArray = new String[EnumerableExtensionsTest._Sample.Length], reversedArray = new String[EnumerableExtensionsTest._Sample.Length];
            Array.Copy(EnumerableExtensionsTest._Sample, sortedArray, sortedArray.Length);
            Array.Sort(sortedArray);
            int j = sortedArray.Length;

            for (int i = 0; i < sortedArray.Length; i ++)
            {
                reversedArray[-- j] = sortedArray[i];
            }

            Assert.IsTrue(sortedArray.CompareTo(reversedArray) < 0);
            Assert.IsTrue(reversedArray.CompareTo(sortedArray) > 0);
        }
        
        [TestMethod]
        public void TestCompareTo_OneNull()
        {
            String[] empty = Array.Empty<String>(), nullArray = null;
            Assert.ThrowsException<ArgumentNullException>(() => nullArray.CompareTo(empty));
            Assert.AreEqual(1, empty.CompareTo(nullArray));
        }
        
        [TestMethod]
        public void TestCompareTo_OneEmpty()
        {
            String[] source = Array.Empty<String>();
            Assert.IsTrue(source.CompareTo(EnumerableExtensionsTest._Sample) < 0);
            Assert.IsTrue(EnumerableExtensionsTest._Sample.CompareTo(source) > 0);
        }

        [TestMethod]
        public void TestMedian_Empty() => Assert.AreEqual(0, EnumerableExtensions.Median(Array.Empty<String>()).Count());

        [TestMethod]
        public void TestMedian_NotEmpty()
        {
            String[] oddArray = new string[EnumerableExtensionsTest._Sample.Length + 1];
            oddArray[EnumerableExtensionsTest._Sample.Length] = "III";
            Array.Copy(EnumerableExtensionsTest._Sample, oddArray, EnumerableExtensionsTest._Sample.Length);
            Array.Sort(EnumerableExtensionsTest._Sample);
            List<String> median = new List<String>(EnumerableExtensions.Median(EnumerableExtensionsTest._Sample));
            Assert.IsTrue(median.Remove("DDD"));
            Assert.IsTrue(median.Remove("EEE"));
            Array.Sort(oddArray);
            median = new List<String>(EnumerableExtensions.Median(oddArray));
            Assert.IsTrue(median.Remove("EEE"));
        }

        [TestMethod]
        public void TestMedian_Null() => Assert.ThrowsException<ArgumentNullException>(() => EnumerableExtensions.Median<Object>(null));
    }
}