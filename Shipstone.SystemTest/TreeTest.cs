using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Collections;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class TreeTest
    {
        [TestMethod]
        public void TestConstructor_Empty()
        {
            Tree<String> tree = new Tree<String>();
            Assert.IsNotNull(tree);
            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.Root);
        }
    }
}