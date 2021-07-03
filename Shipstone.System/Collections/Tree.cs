using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    /// <summary>
    /// Represents a tree capable of containing a variable number of branches.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the vals contained in branches in the tree.</typeparam>
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

        public Tree(T rootValue, IEnumerable<T> collection) => throw new NotImplementedException();
        public Tree(T rootValue, IEnumerable<TreeBranch<T>> collection) => throw new NotImplementedException();
        public Tree(TreeBranch<T> root) => throw new NotImplementedException();
        public Tree(Tree<T> tree) => throw new NotImplementedException();

        /// <summary>
        /// Adds a new branch containing the specified value to the root branch of the <see cref="Tree{T}" />.
        /// </summary>
        /// <param name="val">The value to add to the root branch of the <see cref="Tree{T}" />.</param>
        /// <returns>The new <see cref="TreeBranch{T}" /> containing the specified value.</returns>
        public TreeBranch<T> Add(T val) => this.Add(this.Root, val);

        void ICollection<T>.Add(T val) => this.Add(this.Root, val);

        /// <summary>
        /// Adds the specified new branch to the root branch of the <see cref="Tree{T}" />.
        /// </summary>
        /// <param name="branch">The <see cref="TreeBranch{T}" /> to add to the root branch of the <see cref="Tree{T}" />.</param>
        /// <exception cref="ArgumentNullException"><c><paramref name="branch" /></c> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><c><paramref name="branch" /></c> belongs to another <see cref="Tree{T}" />.</exception>
        public void Add(TreeBranch<T> branch)
        {
            try
            {
                this.Add(this.Root, branch);
            }

            catch (InvalidOperationException)
            {
                throw new InvalidOperationException($"{nameof (branch)} belongs to another Tree<T>.");
            }
        }

        /// <summary>
        /// Adds a new branch containing the specified value to the specified <see cref="TreeBranch{T}" /> contained in the <see cref="Tree{T}" />.
        /// </summary>
        /// <param name="branch">The <see cref="TreeBranch{T}" /> to add the specified value to.</param>
        /// <param name="val">The value to add to <c><paramref name="branch" /></c>.</param>
        /// <returns>The new <see cref="TreeBranch{T}" /> containing the specified value.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="branch" /></c> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><c><paramref name="branch" /></c> does not belong to the current <see cref="Tree{T}" />.</exception>
        public TreeBranch<T> Add(TreeBranch<T> branch, T val)
        {
            TreeBranch<T> child = new TreeBranch<T>(val);
            this.Add(branch, child);
            return child;
        }

        /// <summary>
        /// Adds the specified new branch to the specified <see cref="TreeBranch{T}" /> contained in the <see cref="Tree{T}" />.
        /// </summary>
        /// <param name="branch">The <see cref="TreeBranch{T}" /> to add <c><paramref name="childBranch" /></c> to.</param>
        /// <param name="childBranch">The <see cref="TreeBranch{T}" /> to add to <c><paramref name="branch" /></c>.</param>
        /// <exception cref="ArgumentNullException"><c><paramref name="branch" /></c> is <c>null</c> -or- <c><paramref name="childBranch" /></c> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><c><paramref name="branch" /></c> does not belong to the current <see cref="Tree{T}" /> -or- <c><paramref name="childBranch" /></c> belongs to another <see cref="Tree{T}" />.</exception>
        public void Add(TreeBranch<T> branch, TreeBranch<T> childBranch)
        {
            if (branch is null)
            {
                throw new ArgumentNullException(nameof (branch));
            }

            if (childBranch is null)
            {
                throw new ArgumentNullException(nameof (childBranch));
            }

            if (!Object.ReferenceEquals(this, branch.Tree))
            {
                throw new InvalidOperationException($"{nameof (branch)} does not belong to the current Tree<T>.");
            }

            if (!(childBranch.Tree is null))
            {
                throw new InvalidOperationException($"{nameof (childBranch)} belongs to another Tree<T>.");
            }

            branch._Children.Add(childBranch);
            branch.IncreaseTotalCount();
            childBranch.Parent = branch;
            childBranch.Tree = this;
        }

        public void Clear() => this.Clear(this.Root);
        public void Clear(TreeBranch<T> branch) => throw new NotImplementedException();

        /// <summary>
        /// Determines whether the specified value is found under the root branch of the <see cref="Tree{T}" />.
        /// </summary>
        /// <param name="val">The value to locate under the root branch of the <see cref="Tree{T}" />. The value can be <c>null</c> for reference types.</param>
        /// <returns><c>true</c> if <c><paramref name="val" /></c> is found under the root branch; otherwise, <c>false</c>.</returns>
        public bool Contains(T val)
        {
            this.Find(this.Root, val, out int count);
            return count > 0;
        }

        public bool Contains(TreeBranch<T> branch) => this.Contains(this.Root, branch);

        /// <summary>
        /// Determines whether the specified value is found under the specified <see cref="TreeBranch{T}" /> contained in the <see cref="Tree{T}" />.
        /// </summary>
        /// <param name="branch">The <see cref="TreeBranch{T}" /> contained in the <see cref="Tree{T}" /> to search.</param>
        /// <param name="val">The value to locate under <c><paramref name="branch" /></c>. The value can be <c>null</c> for reference types.</param>
        /// <returns><c>true</c> if <c><paramref name="val" /></c> is found under the root branch; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="branch" /></c> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><c><paramref name="branch" /></c> does not belong to the current <see cref="Tree{T}" />.</exception>
        public bool Contains(TreeBranch<T> branch, T val)
        {
            this.Find(branch, val, out int count);
            return count > 0;
        }

        public bool Contains(TreeBranch<T> branch, TreeBranch<T> childBranch) => throw new NotImplementedException();
        public void CopyTo(T[] array) => this.CopyTo(this.Root, array, 0);
        public void CopyTo(T[] array, int arrayIndex) => this.CopyTo(this.Root, array, arrayIndex);
        public void CopyTo(TreeBranch<T>[] array) => this.CopyTo(this.Root, array, 0);
        public void CopyTo(TreeBranch<T>[] array, int arrayIndex) => this.CopyTo(this.Root, array, arrayIndex);
        public void CopyTo(TreeBranch<T> branch, T[] array) => throw new NotImplementedException();
        public void CopyTo(TreeBranch<T> branch, T[] array, int arrayIndex) => throw new NotImplementedException();
        public void CopyTo(TreeBranch<T> branch, TreeBranch<T>[] array) => throw new NotImplementedException();
        public void CopyTo(TreeBranch<T> branch, TreeBranch<T>[] array, int arrayIndex) => throw new NotImplementedException();

        /// <summary>
        /// Finds all branches under the root node in the <see cref="Tree{T}" /> that contain the specified value.
        /// </summary>
        /// <param name="val">The value to locate under the root branch of the <see cref="Tree{T}" />.</param>
        /// <returns>A collection containing all branches under the root node in the <see cref="Tree{T}" /> that contain the specified value.</returns>
        public IEnumerable<TreeBranch<T>> Find(T val) => this.Find(this.Root, val, out int count);
        
        /// <summary>
        /// Finds all branches under the specified <see cref="TreeBranch{T}" /> in the <see cref="Tree{T}" /> that contain the specified value.
        /// </summary>
        /// <param name="branch">The parent <see cref="TreeBranch{T}" /> contained in the <see cref="Tree{T}" /> to search.</param>
        /// <param name="val">The value to locate under <c><paramref name="branch" /></c>.</param>
        /// <returns>A collection containing all branches under the specified <see cref="TreeBranch{T}" /> in the <see cref="Tree{T}" /> that contain the specified value.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="branch" /></c> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><c><paramref name="branch" /></c> does not belong to the current <see cref="Tree{T}" />.</exception>
        public IEnumerable<TreeBranch<T>> Find(TreeBranch<T> branch, T val) => this.Find(branch, val, out int count);

        private IEnumerable<TreeBranch<T>> Find(TreeBranch<T> branch, T val, out int count)
        {
            if (branch is null)
            {
                throw new ArgumentNullException(nameof (branch));
            }

            if (!Object.ReferenceEquals(this, branch.Tree))
            {
                throw new InvalidOperationException($"{nameof (branch)} does not belong to the current Tree<T>.");
            }

            ICollection<TreeBranch<T>> matches = new List<TreeBranch<T>>();

            if (val == null)
            {
                foreach (TreeBranch<T> child in branch)
                {
                    if (child.Value == null)
                    {
                        matches.Add(child);
                    }
                }
            }

            else
            {
                foreach (TreeBranch<T> child in branch)
                {
                    if (val.Equals(child.Value))
                    {
                        matches.Add(child);
                    }
                }
            }

            count = matches.Count;
            return matches;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.Root._Children.GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => throw new NotImplementedException();

        /// <summary>
        /// Returns an enumerator that iterates through the child branches contained under the root branch of the <see cref="Tree{T}" />.
        /// </summary>
        /// <returns>An enumerator that iterates through the child branches contained under the root branch of the <see cref="Tree{T}" />.</returns>
        public IEnumerator<TreeBranch<T>> GetEnumerator() => this.Root._Children.GetEnumerator();

        public bool Remove(T val) => this.Remove(this.Root, val);
        public bool Remove(TreeBranch<T> branch) => this.Remove(this.Root, branch);
        public bool Remove(TreeBranch<T> branch, T val) => throw new NotImplementedException();
        public bool Remove(TreeBranch<T> branch, TreeBranch<T> childBranch) => throw new NotImplementedException();
        public T[] ToArray() => this.ToArray(this.Root);
        public T[] ToArray(TreeBranch<T> branch) => throw new NotImplementedException();
        public TreeBranch<T>[] ToBranchArray() => this.ToBranchArray(this.Root);
        public TreeBranch<T>[] ToBranchArray(TreeBranch<T> branch) => throw new NotImplementedException();
    }
}