using System;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Net;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class LogTest
    {
        private const String _DefaultAuthUser = "chris";
        private const int _DefaultBytes = 4;
        private const String _DefaultHost = "localhost";
        private const String _DefaultIdentity = null;
        private const String _DefaultRequest = "GET /index.html\r\n";
        private const HttpStatusCode _DefaultStatus = HttpStatusCode.OK;

        [TestMethod]
        public void TestConstructor_BytesNegative()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Log(null, null, null, DateTime.UnixEpoch, null, HttpStatusCode.OK, Int32.MinValue));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Log(null, null, null, DateTime.UnixEpoch, null, HttpStatusCode.OK, -1));
        }

        [TestMethod]
        public void TestEquals_Equal()
        {
            Log expected = new Log(LogTest._DefaultHost, LogTest._DefaultIdentity, LogTest._DefaultAuthUser, LogTest._DefaultRequest, LogTest._DefaultStatus, LogTest._DefaultBytes);
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
            Log logA = new Log(LogTest._DefaultHost, LogTest._DefaultIdentity, LogTest._DefaultAuthUser, LogTest._DefaultRequest, LogTest._DefaultStatus, LogTest._DefaultBytes);
            Log logB = new Log(LogTest._DefaultHost, LogTest._DefaultIdentity, LogTest._DefaultAuthUser, LogTest._DefaultRequest, LogTest._DefaultStatus, LogTest._DefaultBytes);
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
            Log logA = new Log(LogTest._DefaultHost, LogTest._DefaultIdentity, LogTest._DefaultAuthUser, LogTest._DefaultRequest, LogTest._DefaultStatus, LogTest._DefaultBytes);
            Log logB = logA;
            Assert.AreEqual(logA, logB);
            Assert.AreEqual(logA.GetHashCode(), logB.GetHashCode());
        }

        [TestMethod]
        public void TestParse_Valid()
        {
            Log expected = new Log(LogTest._DefaultHost, LogTest._DefaultIdentity, LogTest._DefaultAuthUser, DateTime.UnixEpoch, LogTest._DefaultRequest, LogTest._DefaultStatus, LogTest._DefaultBytes);
            Log actual = Log.Parse(expected.ToString());
            Assert.AreEqual(expected.AuthUser, actual.AuthUser);
            Assert.AreEqual(expected.Bytes, actual.Bytes);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.Host, actual.Host);
            Assert.AreEqual(expected.Identity, actual.Identity);
            Assert.AreEqual(Regex.Replace(expected.Request, @"\s+", " "), actual.Request);
            Assert.AreEqual(expected.Status, actual.Status);
            Assert.AreEqual(expected.UtcDate, actual.UtcDate);
        }

        [TestMethod]
        public void TestParse_Null() => Assert.ThrowsException<ArgumentNullException>(() => Log.Parse(null));

        [TestMethod]
        public void TestToString()
        {
            Log log = new Log(LogTest._DefaultHost, LogTest._DefaultIdentity, LogTest._DefaultAuthUser, DateTime.UnixEpoch, LogTest._DefaultRequest, LogTest._DefaultStatus, LogTest._DefaultBytes);
            Assert.AreEqual($"localhost - chris [01/Jan/1970 00:00:00 +00:00] \"GET /index.html \" 200 4", log.ToString());
        }

        [TestMethod]
        public void TestToString_Null()
        {
            Log log = new Log(LogTest._DefaultHost, LogTest._DefaultIdentity, LogTest._DefaultAuthUser, DateTime.UnixEpoch, LogTest._DefaultRequest, LogTest._DefaultStatus, LogTest._DefaultBytes);
            Assert.AreEqual($"localhost - chris [01/Jan/1970 00:00:00 +00:00] \"GET /index.html \" 200 4", log.ToString(null));
        }
    }
}