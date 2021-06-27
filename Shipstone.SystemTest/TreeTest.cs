using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Collections;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class TreeTest
    {
        private const int _DefaultValue = Int32.MaxValue;

        private Tree<int> _Tree;

        [TestInitialize]
        public void Initialize() => this._Tree = new Tree<int>(TreeTest._DefaultValue);

        [TestMethod]
        public void TestConstructor_T()
        {
            Assert.AreEqual(0, this._Tree.Count);
            Assert.IsNotNull(this._Tree.Root);
            Assert.AreEqual(0, this._Tree.TotalCount);
            TreeBranch<int> root = this._Tree.Root;
            Assert.IsNotNull(root.Children);
            Assert.AreEqual(0, root.Children.Count());
            Assert.AreEqual(0, root.Count);
            Assert.IsNull(root.Parent);
            Assert.AreEqual(0, root.TotalCount);
            Assert.IsTrue(Object.ReferenceEquals(this._Tree, root.Tree));
            Assert.AreEqual(TreeTest._DefaultValue, root.Value);
        }
    }
}