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
        [TestMethod]
        public void TestMedian_Empty() => Assert.AreEqual(0, EnumerableExtensions.Median(Array.Empty<String>()).Count());

        [TestMethod]
        public void TestMedian_NotEmpty()
        {
            String[] evenArray = new String[8] { "DDD", "FFF", "CCC", "HHH", "AAA", "BBB", "GGG", "EEE" };
            String[] oddArray = new string[evenArray.Length + 1];
            oddArray[evenArray.Length] = "III";
            Array.Copy(evenArray, oddArray, evenArray.Length);
            Array.Sort(evenArray);
            List<String> median = new List<String>(EnumerableExtensions.Median(evenArray));
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