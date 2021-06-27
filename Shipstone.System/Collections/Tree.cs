using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    /// <summary>
    /// Represents a tree capable of containing a variable number of branches.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the items contained in branches in the tree.</typeparam>
    public partial class Tree<T> : ICollection<T>, ICollection<TreeBranch<T>>, IReadOnlyCollection<T>, IReadOnlyCollection<TreeBranch<T>>
    {
        /// <summary>
        /// Gets the total number of branches contained in the root branch of the <see cref="Tree{T}" />.
        /// </summary>
        /// <value>The total number of branches contained in the root branch of the <see cref="Tree{T}" />.</value>
        public int Count => this.Root.Count;

        bool ICollection<T>.IsReadOnly => false;
        bool ICollection<TreeBranch<T>>.IsReadOnly => false;

        /// <summary>
        /// Gets the root branch of the <see cref="Tree{T}" />.
        /// </summary>
        /// <value>The root branch of the <see cref="Tree{T}" />.</value>
        public TreeBranch<T> Root { get; }

        /// <summary>
        /// Gets the total number of branches contained in all child branches of the <see cref="Tree{T}" /> recursively.
        /// </summary>
        /// <value>The total number of branches contained in all child branches of the <see cref="Tree{T}" /> recursively.</value>
        public int TotalCount => this.Root.TotalCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tree{T}" /> class containing the specified value in the root branch.
        /// </summary>
        /// <param name="rootValue">The value for the root branch.</param>
        public Tree(T rootValue)
        {
            this.Root = new TreeBranch<T>(rootValue);
            this.Root.Tree = this;
        }

        public Tree(IEnumerator<T> collection) => throw new NotImplementedException();
        public Tree(TreeBranch<T> root) => throw new NotImplementedException();
        public Tree(Tree<T> tree) => throw new NotImplementedException();

        public TreeBranch<T> Add(T item) => throw new NotImplementedException();
        void ICollection<T>.Add(T item) => this.Add(item);
        public void Add(TreeBranch<T> branch) => throw new NotImplementedException();
        public TreeBranch<T> Add(TreeBranch<T> branch, T item) => throw new NotImplementedException();
        public void Add(TreeBranch<T> branch, TreeBranch<T> childBranch) => throw new NotImplementedException();
        public void Clear() => throw new NotImplementedException();
        public void Clear(TreeBranch<T> branch) => throw new NotImplementedException();
        public bool Contains(T item) => throw new NotImplementedException();
        public bool Contains(TreeBranch<T> branch) => throw new NotImplementedException();
        public bool Contains(TreeBranch<T> branch, T item) => throw new NotImplementedException();
        public bool Contains(TreeBranch<T> branch, TreeBranch<T> childBranch) => throw new NotImplementedException();
        public void CopyTo(T[] array) => throw new NotImplementedException();
        public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();
        public void CopyTo(TreeBranch<T>[] array) => throw new NotImplementedException();
        public void CopyTo(TreeBranch<T>[] array, int arrayIndex) => throw new NotImplementedException();
        public void CopyTo(TreeBranch<T> branch, T[] array) => throw new NotImplementedException();
        public void CopyTo(TreeBranch<T> branch, T[] array, int arrayIndex) => throw new NotImplementedException();
        public void CopyTo(TreeBranch<T> branch, TreeBranch<T>[] array) => throw new NotImplementedException();
        public void CopyTo(TreeBranch<T> branch, TreeBranch<T>[] array, int arrayIndex) => throw new NotImplementedException();
        public TreeBranch<T> Find(T item) => throw new NotImplementedException();
        public TreeBranch<T> Find(TreeBranch<T> branch, T item) => throw new NotImplementedException();
        IEnumerator IEnumerable.GetEnumerator() => this.Root._Children.GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => throw new NotImplementedException();

        /// <summary>
        /// Returns an enumerator that iterates through the child branches contained under the root branch of the <see cref="Tree{T}" />.
        /// </summary>
        /// <returns>An enumerator that iterates through the child branches contained under the root branch of the <see cref="Tree{T}" />.</returns>
        public IEnumerator<TreeBranch<T>> GetEnumerator() => this.Root._Children.GetEnumerator();

        public bool Remove(T item) => throw new NotImplementedException();
        public bool Remove(TreeBranch<T> branch) => throw new NotImplementedException();
        public bool Remove(TreeBranch<T> branch, T item) => throw new NotImplementedException();
        public bool Remove(TreeBranch<T> branch, TreeBranch<T> childBranch) => throw new NotImplementedException();
        public T[] ToArray() => throw new NotImplementedException();
        public T[] ToArray(TreeBranch<T> branch) => throw new NotImplementedException();
        public TreeBranch<T>[] ToBranchArray() => throw new NotImplementedException();
        public TreeBranch<T>[] ToBranchArray(TreeBranch<T> branch) => throw new NotImplementedException();
    }
}