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

        private static readonly Random _Random;

        private Tree<int> _Tree;

        static TreeTest() => TreeTest._Random = new Random();

        private static void AssertBranch<T>(TreeBranch<T> branch, int count, TreeBranch<T> parent, int totalCount, Tree<T> tree, T val)
        {
            Assert.IsNotNull(branch.Children);
            Assert.AreEqual(count, branch.Count);
            Assert.IsTrue(Object.ReferenceEquals(parent, branch.Parent));
            Assert.AreEqual(totalCount, branch.TotalCount);
            Assert.IsTrue(Object.ReferenceEquals(tree, branch.Tree));
            Assert.AreEqual(val, branch.Value);
        }

        private static void AssertTree<T>(Tree<T> tree, int count, int totalCount)
        {
            Assert.AreEqual(count, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(totalCount, tree.TotalCount);
        }

        private static int NextRandomNotEquals(int val)
        {
            int newVal;

            do
            {
                newVal = TreeTest._Random.Next();
            } while (newVal == val);

            return newVal;
        }
        
        private void AssertRoot(int count, int totalCount, Tree<int> tree, int val) => TreeTest.AssertBranch(this._Tree.Root, count, null, totalCount, tree, val);
        private void AssertTree(int count, int totalCount) => TreeTest.AssertTree(this._Tree, count, totalCount);

        [TestInitialize]
        public void Initialize() => this._Tree = new Tree<int>(TreeTest._DefaultValue);

        [TestMethod]
        public void TestConstructor_T()
        {
            this.AssertTree(0, 0);
            this.AssertRoot(0, 0, this._Tree, TreeTest._DefaultValue);
        }

        [TestMethod]
        public void TestAdd_T()
        {
            int val = TreeTest._Random.Next();
            TreeBranch<int> child = this._Tree.Add(val);
            this.AssertTree(1, 1);
            this.AssertRoot(1, 1, this._Tree, TreeTest._DefaultValue);

            foreach (TreeBranch<int> branch in this._Tree.Root)
            {
                Assert.IsTrue(Object.ReferenceEquals(branch, child));
            }
        }

        [TestMethod]
        public void TestAdd_TreeBranch_Belongs()
        {
            Tree<int> newTree = new Tree<int>(TreeTest._DefaultValue);
            int val = TreeTest._Random.Next();
            TreeBranch<int> child = newTree.Add(val);
            Exception ex = Assert.ThrowsException<InvalidOperationException>(() => this._Tree.Add(child));
            Assert.AreEqual("branch belongs to another Tree<T>.", ex.Message);
        }

        [TestMethod]
        public void TestAdd_TreeBranch_NotBelongs()
        {
            int val = TreeTest._Random.Next();
            TreeBranch<int> child = new TreeBranch<int>(val);
            this._Tree.Add(child);
            this.AssertTree(1, 1);
            this.AssertRoot(1, 1, this._Tree, TreeTest._DefaultValue);
            TreeTest.AssertBranch(child, 0, this._Tree.Root, 0, this._Tree, val);
        }

        [TestMethod]
        public void TestAdd_TreeBranch_Null() => Assert.ThrowsException<ArgumentNullException>(() => this._Tree.Add(null));

        [TestMethod]
        public void TestAdd_TreeBranch_T_Belongs()
        {
            int parentVal = TreeTest._Random.Next();
            TreeBranch<int> parent = this._Tree.Add(parentVal);
            int childVal = TreeTest.NextRandomNotEquals(parentVal);
            TreeBranch<int> child = this._Tree.Add(parent, childVal);
            this.AssertTree(1, 2);
            this.AssertRoot(1, 2, this._Tree, TreeTest._DefaultValue);
            TreeTest.AssertBranch(parent, 1, this._Tree.Root, 1, this._Tree, parentVal);
            TreeTest.AssertBranch(child, 0, parent, 0, this._Tree, childVal);
        }

        [TestMethod]
        public void TestAdd_TreeBranch_T_NotBelongs()
        {
            int parentVal = TreeTest._Random.Next();
            TreeBranch<int> parent = new TreeBranch<int>(parentVal);
            Exception ex = Assert.ThrowsException<InvalidOperationException>(() => this._Tree.Add(parent, TreeTest.NextRandomNotEquals(parentVal)));
            Assert.AreEqual("branch does not belong to the current Tree<T>.", ex.Message);
        }

        [TestMethod]
        public void TestAdd_TreeBranch_TreeBranch()
        {
            int parentVal = TreeTest._Random.Next();
            TreeBranch<int> parent = this._Tree.Add(parentVal);
            int childVal = TreeTest.NextRandomNotEquals(parentVal);
            TreeBranch<int> child = new TreeBranch<int>(childVal);
            this._Tree.Add(parent, child);
            this.AssertTree(1, 2);
            this.AssertRoot(1, 2, this._Tree, TreeTest._DefaultValue);
            TreeTest.AssertBranch(parent, 1, this._Tree.Root, 1, this._Tree, parentVal);
            TreeTest.AssertBranch(child, 0, parent, 0, this._Tree, childVal);
        }

        [TestMethod]
        public void TestAdd_TreeBranch_TreeBranch_ChildBelongs()
        {
            Tree<int> newTree = new Tree<int>(0);
            int val = TreeTest._Random.Next();
            TreeBranch<int> child = new TreeBranch<int>(val);
            TreeBranch<int> parent = new TreeBranch<int>(val);
            this._Tree.Add(parent);
            newTree.Add(child);
            Exception ex = Assert.ThrowsException<InvalidOperationException>(() => this._Tree.Add(parent, child));
            Assert.AreEqual("childBranch belongs to another Tree<T>.", ex.Message);
        }

        [TestMethod]
        public void TestAdd_TreeBranch_TreeBranch_ParentNotBelongs()
        {
            int val = TreeTest._Random.Next();
            TreeBranch<int> child = new TreeBranch<int>(val);
            TreeBranch<int> parent = new TreeBranch<int>(val);
            Exception ex = Assert.ThrowsException<InvalidOperationException>(() => this._Tree.Add(parent, child));
            Assert.AreEqual("branch does not belong to the current Tree<T>.", ex.Message);
        }

        [TestMethod]
        public void TestAdd_TreeBranch_T_BranchNull() => Assert.ThrowsException<ArgumentNullException>(() => this._Tree.Add(null, TreeTest._Random.Next()));

        [TestMethod]
        public void TestAdd_TreeBranch_TreeBranch_BothNull() => Assert.ThrowsException<ArgumentNullException>(() => this._Tree.Add(null, null));

        [TestMethod]
        public void TestAdd_TreeBranch_TreeBranch_ChildNull() => Assert.ThrowsException<ArgumentNullException>(() => this._Tree.Add(this._Tree.Add(0), null));

        [TestMethod]
        public void TestAdd_TreeBranch_TreeBranch_ParentNull() => Assert.ThrowsException<ArgumentNullException>(() => this._Tree.Add(null, new TreeBranch<int>(0)));

        [TestMethod]
        public void TestContains_T()
        {
            const int MAX = 100;
            int[] array = new int[10];

            for (int i = 0; i < array.Length; i ++)
            {
                array[i] = TreeTest._Random.Next(MAX);
                this._Tree.Add(array[i]);
            }

            for (int i = 0; i < MAX; i ++)
            {
                Assert.AreEqual(array.Contains(i), this._Tree.Contains(i));
            }
        }

        [TestMethod]
        public void TestContains_TreeBranch_T()
        {
            const int MAX = 100;
            TreeBranch<int> branch = this._Tree.Add(MAX + 1);
            int[] array = new int[10];

            for (int i = 0; i < array.Length; i ++)
            {
                array[i] = TreeTest._Random.Next(MAX);
                this._Tree.Add(branch, array[i]);
            }

            for (int i = 0; i < MAX; i ++)
            {
                Assert.IsFalse(this._Tree.Contains(i));
                Assert.AreEqual(array.Contains(i), this._Tree.Contains(branch, i));
            }
        }
    }
}