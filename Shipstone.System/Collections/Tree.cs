using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    /// <summary>
    /// Represents a tree with zero or more branches.
    /// </summary>
    /// <typeparam name="T">Specifies the item type of the tree.</typeparam>
    public partial class Tree<T> : ICollection<T>, ICollection<Tree<T>.Node>
    {
        private int _Count;
        private readonly ICollection<Tree<T>.Enumerator> _Enumerators;
        private readonly LinkedList<Tree<T>.Node> _RootBranches;

        /// <summary>
        /// Gets the number of branches in the <see cref="Tree{T}" />.
        /// </summary>
        /// <value>The number of branches in the <see cref="Tree{T}" />.</value>
        public int Count => this._Count;

        bool ICollection<T>.IsReadOnly => false;
        bool ICollection<Tree<T>.Node>.IsReadOnly => false;

        /// <summary>
        /// Gets a collection containing the root branch nodes of the <see cref="Tree{T}" />.
        /// </summary>
        /// <value>A collection containing the root branch nodes of the <see cref="Tree{T}" />.</value>
        public IEnumerable<Tree<T>.Node> RootBranches => this._RootBranches;

#region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Tree{T}" /> class that is empty.
        /// </summary>
        public Tree()
        {
            this._Enumerators = new LinkedList<Tree<T>.Enumerator>();
            this._RootBranches = new LinkedList<Tree<T>.Node>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tree{T}" /> class that contains nodes and values copied from <c><paramref name="tree" /></c>.
        /// </summary>
        /// <param name="tree">The <see cref="Tree{T}" /> to copy nodes and values from.</param>
        /// <exception cref="ArgumentNullException"><c><paramref name="tree" /></c> is <c>null</c>.</exception>
        public Tree(Tree<T> tree) : this()
        {
            if (tree is null)
            {
                throw new ArgumentNullException(nameof (tree));
            }

            foreach (Tree<T>.Node treeRoot in tree._RootBranches)
            {
                Tree<T>.Node root = this._AddRoot(treeRoot._Value, false);
                this._Copy(treeRoot, root);
            }
        }

        public Tree(IEnumerable<Tree<T>.Node> collection) : this() => throw new NotImplementedException();
        public Tree(IEnumerable<T> collection) : this() => throw new NotImplementedException();
#endregion

#region Private methods
        private Tree<T>.Node _Add(Tree<T>.Node node, T item, bool notify)
        {
            Tree<T>.Node newNode = new Tree<T>.Node(item);
            newNode._RootBranch = node;
            newNode._Tree = this;
            node._ChildBranches.AddLast(newNode);
            ++ node._Count;
            ++ this._Count;

            if (notify)
            {
                this._NotifyEnumerators();
            }

            return newNode;
        }

        private Tree<T>.Node _AddRoot(T item, bool notify)
        {
            Tree<T>.Node node = new Tree<T>.Node(item);
            node._Tree = this;
            this._RootBranches.AddLast(node);
            ++ this._Count;

            if (notify)
            {
                this._NotifyEnumerators();
            }

            return node;
        }

        private void _Copy(Tree<T>.Node sourceNode, Tree<T>.Node destNode)
        {
            foreach (Tree<T>.Node sourceBranch in sourceNode)
            {
                Tree<T>.Node destBranch = this._Add(destNode, sourceBranch._Value, false);
                this._Copy(sourceBranch, destBranch);
            }

            this._NotifyEnumerators();
        }

        private void _NotifyEnumerators()
        {
            foreach (Tree<T>.Enumerator enumerator in this._Enumerators)
            {
                enumerator._IsModified = true;
            }
        }
#endregion

#region Public methods
#region Add functionality
        public void Add(Tree<T>.Node node) => throw new NotImplementedException();

        public Tree<T>.Node Add(T item) => this._AddRoot(item, true);

        void ICollection<T>.Add(T item) => this._AddRoot(item, true);

        public void Add(Tree<T>.Node node, Tree<T>.Node newNode) => throw new NotImplementedException();
        public Tree<T>.Node Add(Tree<T>.Node node, T item) => throw new NotImplementedException();
        public void AddRange(IEnumerable<Tree<T>.Node> collection) => throw new NotImplementedException();

        public IEnumerable<Tree<T>.Node> AddRange(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof (collection));
            }

            ICollection<Tree<T>.Node> range = new List<Tree<T>.Node>();

            foreach (T item in collection)
            {
                range.Add(this._AddRoot(item, false));
            }

            this._NotifyEnumerators();
            return range;
        }

        public void AddRange(Tree<T>.Node node, IEnumerable<Tree<T>.Node> collection) => throw new NotImplementedException();
        public IEnumerable<Tree<T>.Node> AddRange(Tree<T>.Node node, IEnumerable<T> collection) => throw new NotImplementedException();
#endregion

        public void Clear() => throw new NotImplementedException();
        public void Clear(Tree<T>.Node node) => throw new NotImplementedException();

#region Contains functionality
        public bool Contains(Tree<T>.Node node) => throw new NotImplementedException();
        public bool Contains(T item) => throw new NotImplementedException();
        public bool Contains(Tree<T>.Node node, Tree<T>.Node containsNode) => throw new NotImplementedException();
        public bool Contains(Tree<T>.Node node, T item) => throw new NotImplementedException();
        public bool ContainsRange(IEnumerable<Tree<T>.Node> collection) => throw new NotImplementedException();
        public bool ContainsRange(IEnumerable<T> collection) => throw new NotImplementedException();
        public bool ContainsRange(Tree<T>.Node node, IEnumerable<Tree<T>.Node> collection) => throw new NotImplementedException();
        public bool ContainsRange(Tree<T>.Node node, IEnumerable<T> collection) => throw new NotImplementedException();
#endregion

#region CopyTo methods
        public void CopyTo(T[] array) => throw new NotImplementedException();
        public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();
        public void CopyTo(T[] array, Tree<T>.Node node) => throw new NotImplementedException();
        public void CopyTo(T[] array, int arrayIndex, Tree<T>.Node node) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array, int arrayIndex) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array, Tree<T>.Node node) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array, int arrayIndex, Tree<T>.Node node) => throw new NotImplementedException();
#endregion

#region GetEnumerator functionality
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="Tree{T}" />.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}" /> for the <see cref="Tree{T}" />.</returns>
        public IEnumerator<T> GetEnumerator() => new Tree<T>.Enumerator(this);

        IEnumerator<Tree<T>.Node> IEnumerable<Tree<T>.Node>.GetEnumerator() => this.GetNodeEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        public IEnumerator<Tree<T>.Node> GetNodeEnumerator() => throw new NotImplementedException();
#endregion

#region GetNodes methods
        public IEnumerable<Tree<T>.Node> GetNodes(T item) => this.GetNodes(item, null);
        public IEnumerable<Tree<T>.Node> GetNodes(T item, IEqualityComparer<T> comparer) => throw new NotImplementedException();
        public IEnumerable<Tree<T>.Node> GetNodes(Tree<T>.Node node, T item) => this.GetNodes(node, item, null);
        public IEnumerable<Tree<T>.Node> GetNodes(Tree<T>.Node node, T item, IEqualityComparer<T> comparer) => throw new NotImplementedException();
#endregion

#region Remove functionality
        public bool Remove(Tree<T>.Node node) => throw new NotImplementedException();
        public bool Remove(Tree<T>.Node node, T item) => throw new NotImplementedException();
        public bool Remove(Tree<T>.Node node, Tree<T>.Node removeNode) => throw new NotImplementedException();
        public bool Remove(T item) => throw new NotImplementedException();
        public int RemoveAll(Tree<T>.Node node) => throw new NotImplementedException();
        public int RemoveAll(T item) => throw new NotImplementedException();
        public bool RemoveRange(IEnumerable<Tree<T>.Node> collection) => throw new NotImplementedException();
        public bool RemoveRange(IEnumerable<T> collection) => throw new NotImplementedException();
        public bool RemoveRange(Tree<T>.Node node, IEnumerable<Tree<T>.Node> collection) => throw new NotImplementedException();
        public bool RemoveRange(Tree<T>.Node node, IEnumerable<T> collection) => throw new NotImplementedException();
#endregion

#region ToArray functionality
        public T[] ToArray() => throw new NotImplementedException();
        public T[] ToArray(Tree<T>.Node node) => throw new NotImplementedException();
        public Tree<T>.Node[] ToNodeArray() => throw new NotImplementedException();
        public Tree<T>.Node[] ToNodeArray(Tree<T>.Node node) => throw new NotImplementedException();
#endregion
#endregion
    }
}