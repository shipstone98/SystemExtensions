using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Net;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void TestConstructor_BytesNegative()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Log(null, null, null, null, HttpStatusCode.OK, Int32.MinValue));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Log(null, null, null, null, HttpStatusCode.OK, -1));
        }
    }
}