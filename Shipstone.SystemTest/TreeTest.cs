using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Collections;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class TreeTest
    {
        private Tree<String> _Tree;

        [TestInitialize]
        public void Initialize() => this._Tree = new();

#region Constructor tests
        [TestMethod]
        public void TestConstructor_Empty()
        {
            Assert.IsNotNull(this._Tree);
            Assert.AreEqual(0, this._Tree.Count);
            Assert.IsNotNull(this._Tree.RootBranches);
            Assert.AreEqual(0, this._Tree.RootBranches.Count());
        }

        [TestMethod]
        public void TestConstructor_Tree_Empty()
        {
            Tree<String> tree = new(this._Tree);
            Assert.AreEqual(0, tree.Count);
            Assert.IsNotNull(tree.RootBranches);
            Assert.AreEqual(0, tree.RootBranches.Count());
        }

        [TestMethod]
        public void TestConstructor_Tree_NotEmpty()
        {
            String[] array = new String[5] { "Aaa", "Bbb", "Ccc", "Ddd", "Eee" };
            this._Tree.AddRange(array);
            Tree<String> tree = new(this._Tree);
            Assert.AreEqual(array.Length, tree.Count);
            Assert.IsNotNull(tree.RootBranches);
            Assert.AreEqual(array.Length, tree.RootBranches.Count());
            ICollection<String> remaining = new LinkedList<String>(array);

            foreach (String item in tree)
            {
                Assert.IsTrue(remaining.Remove(item));
            }
            
            Assert.AreEqual(0, remaining.Count);
        }

        [TestMethod]
        public void TestConstructor_Tree_Null() => Assert.ThrowsException<ArgumentNullException>(() => new Tree<String>(null as Tree<String>));
#endregion

#region Add tests
        [TestMethod]
        public void TestAdd_Item_Empty()
        {
            const String S = "Hello, world!";
            Tree<String>.Node node = this._Tree.Add(S);
            Assert.IsNotNull(node);
            Assert.IsNotNull(node.ChildBranches);
            Assert.AreEqual(0, node.ChildBranches.Count());
            Assert.AreEqual(0, node.Count);
            Assert.IsNull(node.RootBranch);
            Assert.IsTrue(Object.ReferenceEquals(this._Tree, node.Tree));
            Assert.AreEqual(S, node.Value);
            Assert.AreEqual(1, this._Tree.Count);
            Assert.AreEqual(1, this._Tree.RootBranches.Count());

            using (IEnumerator<String> enumerator = this._Tree.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.AreEqual(S, enumerator.Current);
                Assert.IsFalse(enumerator.MoveNext());
            }
        }

        [TestMethod]
        public void TestAdd_Item_NotEmpty()
        {
            const String S_1 = "Hello, world!", S_2 = "Bonjour, le monde!";
            this._Tree.Add(S_1);
            Tree<String>.Node node = this._Tree.Add(S_2);
            Assert.IsNotNull(node);
            Assert.IsNotNull(node.ChildBranches);
            Assert.AreEqual(0, node.ChildBranches.Count());
            Assert.AreEqual(0, node.Count);
            Assert.IsNull(node.RootBranch);
            Assert.IsTrue(Object.ReferenceEquals(this._Tree, node.Tree));
            Assert.AreEqual(S_2, node.Value);
            Assert.AreEqual(2, this._Tree.Count);
            Assert.AreEqual(2, this._Tree.RootBranches.Count());

            using (IEnumerator<String> enumerator = this._Tree.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.AreEqual(S_1, enumerator.Current);
                Assert.IsTrue(enumerator.MoveNext());
                Assert.AreEqual(S_2, enumerator.Current);
                Assert.IsFalse(enumerator.MoveNext());
            }
        }

        [TestMethod]
        public void TestAdd_Item_Null()
        {
            Tree<String>.Node node = this._Tree.Add(null as String);
            Assert.IsNotNull(node);
            Assert.IsNotNull(node.ChildBranches);
            Assert.AreEqual(0, node.ChildBranches.Count());
            Assert.AreEqual(0, node.Count);
            Assert.IsNull(node.RootBranch);
            Assert.IsTrue(Object.ReferenceEquals(this._Tree, node.Tree));
            Assert.IsNull(node.Value);
            Assert.AreEqual(1, this._Tree.Count);
            Assert.AreEqual(1, this._Tree.RootBranches.Count());

            using (IEnumerator<String> enumerator = this._Tree.GetEnumerator())
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsNull(enumerator.Current);
                Assert.IsFalse(enumerator.MoveNext());
            }
        }
#endregion

#region GetEnumerator tests
        [TestMethod]
        public void TestGetEnumerator_Disposed()
        {
            using (IEnumerator<String> enumerator = this._Tree.GetEnumerator())
            {
                enumerator.Dispose();
                Assert.ThrowsException<ObjectDisposedException>(() => enumerator.MoveNext());
                Assert.ThrowsException<ObjectDisposedException>(() => enumerator.Reset());
            }
        }

        [TestMethod]
        public void TestGetEnumerator_Empty()
        {
            using (IEnumerator<String> enumerator = this._Tree.GetEnumerator())
            {
                Assert.IsNotNull(enumerator);
                Assert.IsFalse(enumerator.MoveNext());
            }

            foreach (String item in this._Tree)
            {
                Assert.Fail("The enumerator returned items for an empty collection.");
            }
        }

        [TestMethod]
        public void TestGetEnumerator_Modified()
        {
            using (IEnumerator<String> enumerator = this._Tree.GetEnumerator())
            {
                this._Tree.Add("Hello, world!");
                const String MESSAGE = "The collection was modified after the enumerator was created.";
                Exception ex = Assert.ThrowsException<InvalidOperationException>(() => enumerator.MoveNext());
                Assert.AreEqual(MESSAGE, ex.Message);
                ex = Assert.ThrowsException<InvalidOperationException>(() => enumerator.Reset());
                Assert.AreEqual(MESSAGE, ex.Message);
            }
        }

        [TestMethod]
        public void TestGetEnumerator_NotEmpty()
        {
            String[] array = new String[5] { "Aaa", "Bbb", "Ccc", "Ddd", "Eee" };
            this._Tree.AddRange(array);
            int n = 0;
            ICollection<String> remaining = new LinkedList<String>(array);

            using (IEnumerator<String> enumerator = this._Tree.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Assert.IsTrue(remaining.Remove(enumerator.Current));
                    ++ n;
                }

                Assert.AreEqual(array.Length, n);
            }
        }
#endregion
    }
}