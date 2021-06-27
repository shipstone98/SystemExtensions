using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    /// <summary>
    /// Represents a branch node of a <see cref="Tree{T}" />.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the item contained in the branch.</typeparam>
    public partial class TreeBranch<T> : IReadOnlyCollection<T>, IReadOnlyCollection<TreeBranch<T>>
    {
        internal readonly ICollection<TreeBranch<T>> _Children;

        /// <summary>
        /// Gets a collection of child branches contained under the <see cref="TreeBranch{T}" />.
        /// </summary>
        /// <value>A collection of child branches contained under the <see cref="TreeBranch{T}" />.</value>
        public IEnumerable<TreeBranch<T>> Children => this._Children;

        /// <summary>
        /// Gets the total number of branches contained in the <see cref="TreeBranch{T}" />.
        /// </summary>
        /// <value>The total number of branches contained in the <see cref="TreeBranch{T}" />.</value>
        public int Count => this._Children.Count;

        /// <summary>
        /// Gets the parent branch of the <see cref="TreeBranch{T}" />.
        /// </summary>
        /// <value>The parent branch of the <see cref="TreeBranch{T}" />. The value is <c>null</c> if the <see cref="TreeBranch{T}" /> has no parent and is the <see cref="Shipstone.System.Collections.Tree{T}.Root" /> of <see cref="Tree{T}" />.</value>
        public TreeBranch<T> Parent { get; }

        /// <summary>
        /// Gets the total number of branches contained in all child branches of the <see cref="TreeBranch{T}" /> recursively.
        /// </summary>
        /// <value>The total number of branches contained in all child branches of the <see cref="TreeBranch{T}" /> recursively.</value>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Gets the <see cref="Tree{T}" /> the <see cref="TreeBranch{T}" /> belongs to.
        /// </summary>
        /// <value>The <see cref="Tree{T}" /> the <see cref="TreeBranch{T}" /> belongs to.</value>
        public Tree<T> Tree { get; internal set; }

        /// <summary>
        /// Gets or sets the value contained in the <see cref="TreeBranch{T}" />.
        /// </summary>
        /// <value>The value contained in the <see cref="TreeBranch{T}" />.</value>
        public T Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeBranch{T}" /> class that contains the specified value.
        /// </summary>
        /// <param name="val">The value contained in the <see cref="TreeBranch{T}" />.</param>
        public TreeBranch(T val)
        {
            this._Children = new LinkedList<TreeBranch<T>>();
            this.Value = val;
        }

        internal TreeBranch(T val, Tree<T> tree) : this(val) => this.Tree = tree;

        internal void DecreaseTotalCount() => throw new NotImplementedException();
        IEnumerator IEnumerable.GetEnumerator() => this._Children.GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => throw new NotImplementedException();
        public IEnumerator<TreeBranch<T>> GetEnumerator() => this._Children.GetEnumerator();
        internal void IncreaseTotalCount() => throw new NotImplementedException();
    }
}