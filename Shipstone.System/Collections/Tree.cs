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
        private Tree<T>.Node _Root;

        /// <summary>
        /// Gets the number of branches in the <see cref="Tree{T}" />.
        /// </summary>
        /// <value>The number of branches in the <see cref="Tree{T}" />. The value is recursive.</value>
        public int Count => this._Root is null ? 0 : this._Root._Count;
        bool ICollection<T>.IsReadOnly => false;
        bool ICollection<Tree<T>.Node>.IsReadOnly => false;

        /// <summary>
        /// Gets the root <see cref="Tree{T}.Node" /> of the <see cref="Tree{T}" />.
        /// </summary>
        /// <value>The root <see cref="Tree{T}.Node" /> of the <see cref="Tree{T}" />, or <c>null</c> if the tree is empty.</value>
        public Tree<T>.Node Root => this._Root;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tree{T}" /> class that is empty.
        /// </summary>
        public Tree() { }

        public Tree(Tree<T> tree) : this(tree, false) { }
        public Tree(Tree<T> tree, bool recursive) => throw new NotImplementedException();
        public Tree(Tree<T>.Node node) : this(node, false) { }
        public Tree(Tree<T>.Node node, bool recursive) => throw new NotImplementedException();

        public void Add(Tree<T>.Node node) => throw new NotImplementedException();
        public Tree<T>.Node Add(T item) => throw new NotImplementedException();
        void ICollection<T>.Add(T item) => this.Add(item);
        public void Add(Tree<T>.Node node, Tree<T>.Node newNode) => throw new NotImplementedException();
        public Tree<T>.Node Add(Tree<T>.Node node, T item) => throw new NotImplementedException();
        public void Clear() => throw new NotImplementedException();
        public void Clear(Tree<T>.Node node) => throw new NotImplementedException();
        public bool Contains(Tree<T>.Node node) => throw new NotImplementedException();
        public bool Contains(T item) => throw new NotImplementedException();
        public bool Contains(Tree<T>.Node node, Tree<T>.Node containsNode) => throw new NotImplementedException();
        public bool Contains(Tree<T>.Node node, T item) => throw new NotImplementedException();
        public void CopyTo(T[] array) => throw new NotImplementedException();
        public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();
        public void CopyTo(T[] array, Tree<T>.Node node) => throw new NotImplementedException();
        public void CopyTo(T[] array, bool recursive) => throw new NotImplementedException();
        public void CopyTo(T[] array, int arrayIndex, Tree<T>.Node node) => throw new NotImplementedException();
        public void CopyTo(T[] array, int arrayIndex, bool recursive) => throw new NotImplementedException();
        public void CopyTo(T[] array, int arrayIndex, Tree<T>.Node node, bool recursive) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array, int arrayIndex) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array, Tree<T>.Node node) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array, bool recursive) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array, int arrayIndex, Tree<T>.Node node) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array, int arrayIndex, bool recursive) => throw new NotImplementedException();
        public void CopyTo(Tree<T>.Node[] array, int arrayIndex, Tree<T>.Node node, bool recursive) => throw new NotImplementedException();
        public IEnumerator<T> GetEnumerator() => this.GetEnumerator(false);
        public IEnumerator<T> GetEnumerator(bool recursive) => throw new NotImplementedException();
        IEnumerator<Tree<T>.Node> IEnumerable<Tree<T>.Node>.GetEnumerator() => throw new NotImplementedException();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        public IEnumerator<Tree<T>.Node> GetNodeEnumerator() => this.GetNodeEnumerator(false);
        public IEnumerator<Tree<T>.Node> GetNodeEnumerator(bool recursive) => throw new NotImplementedException();
        public IEnumerable<Tree<T>.Node> GetNodes(T item) => this.GetNodes(item, false, null);
        public IEnumerable<Tree<T>.Node> GetNodes(T item, bool recursive) => this.GetNodes(item, recursive, null);
        public IEnumerable<Tree<T>.Node> GetNodes(T item, IEqualityComparer<T> comparer) => this.GetNodes(item, false, comparer);
        public IEnumerable<Tree<T>.Node> GetNodes(T item, bool recursive, IEqualityComparer<T> comparer) => throw new NotImplementedException();
        public IEnumerable<Tree<T>.Node> GetNodes(Tree<T>.Node node, T item) => this.GetNodes(node, item, false, null);
        public IEnumerable<Tree<T>.Node> GetNodes(Tree<T>.Node node, T item, bool recursive) => this.GetNodes(node, item, recursive, null);
        public IEnumerable<Tree<T>.Node> GetNodes(Tree<T>.Node node, T item, IEqualityComparer<T> comparer) => this.GetNodes(node, item, false, comparer);
        public IEnumerable<Tree<T>.Node> GetNodes(Tree<T>.Node node, T item, bool recursive, IEqualityComparer<T> comparer) => throw new NotImplementedException();
        public bool Remove(Tree<T>.Node node) => throw new NotImplementedException();
        public bool Remove(Tree<T>.Node node, T item) => throw new NotImplementedException();
        public bool Remove(Tree<T>.Node node, Tree<T>.Node removeNode) => throw new NotImplementedException();
        public bool Remove(T item) => throw new NotImplementedException();
        public int RemoveAll(Tree<T>.Node node) => throw new NotImplementedException();
        public int RemoveAll(T item) => throw new NotImplementedException();
        public T[] ToArray() => throw new NotImplementedException();
        public T[] ToArray(bool recursive) => throw new NotImplementedException();
        public T[] ToArray(Tree<T>.Node node) => throw new NotImplementedException();
        public T[] ToArray(Tree<T>.Node node, bool recursive) => throw new NotImplementedException();
        public Tree<T>.Node[] ToNodeArray() => throw new NotImplementedException();
        public Tree<T>.Node[] ToNodeArray(bool recursive) => throw new NotImplementedException();
        public Tree<T>.Node[] ToNodeArray(Tree<T>.Node node) => throw new NotImplementedException();
        public Tree<T>.Node[] ToNodeArray(Tree<T>.Node node, bool recursive) => throw new NotImplementedException();
    }
}