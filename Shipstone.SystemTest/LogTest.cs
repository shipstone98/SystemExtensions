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

        [TestMethod]
        public void TestEquals_Equal()
        {
            const String AUTH_USER = "chris";
            const String HOST = "localhost";
            const String IDENT = null;
            const String REQUEST = "GET /index.html\r\n";
            const HttpStatusCode STATUS = HttpStatusCode.OK;
            const int BYTES = 4;
            Log expected = new Log(HOST, IDENT, AUTH_USER, REQUEST, STATUS, BYTES);
            Log actual = expected;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(expected.Equals(actual));
            Assert.IsTrue(Object.Equals(expected, actual));
            Assert.IsTrue(expected == actual);
            Assert.IsFalse(expected != actual);
            Object actualObject = actual;
            Assert.IsTrue(expected.Equals(actualObject));
        }

        [TestMethod]
        public void TestEquals_NotEqual()
        {
            const String AUTH_USER = "chris";
            const String HOST = "localhost";
            const String IDENT = null;
            const String REQUEST = "GET /index.html\r\n";
            const HttpStatusCode STATUS = HttpStatusCode.OK;
            const int BYTES = 4;
            Log logA = new Log(HOST, IDENT, AUTH_USER, REQUEST, STATUS, BYTES);
            Log logB = new Log(HOST, IDENT, AUTH_USER, REQUEST, STATUS, BYTES);
            Assert.AreNotEqual(logA, logB);
            Assert.IsFalse(logA.Equals(logB));
            Assert.IsFalse(Object.Equals(logA, logB));
            Assert.IsFalse(logA == logB);
            Assert.IsTrue(logA != logB);
            Object actualObject = logB;
            Assert.IsFalse(logA.Equals(actualObject));
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            const String AUTH_USER = "chris";
            const String HOST = "localhost";
            const String IDENT = null;
            const String REQUEST = "GET /index.html\r\n";
            const HttpStatusCode STATUS = HttpStatusCode.OK;
            const int BYTES = 4;
            Log logA = new Log(HOST, IDENT, AUTH_USER, REQUEST, STATUS, BYTES);
            Log logB = logA;
            Assert.AreEqual(logA, logB);
            Assert.AreEqual(logA.GetHashCode(), logB.GetHashCode());
        }
    }
}