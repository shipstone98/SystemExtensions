using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    partial class Tree<T>
    {
        /// <summary>
        /// Represents a branch node in a <see cref="Tree{T}" />.
        /// </summary>
        public class Node : IReadOnlyCollection<Tree<T>.Node>
        {
            internal readonly LinkedList<Tree<T>.Node> _ChildBranches;
            internal int _Count;
            internal Tree<T>.Node _RootBranch;
            internal Tree<T> _Tree;
            internal T _Value;

            /// <summary>
            /// Gets a collection containing all child branch nodes in the <see cref="Tree{T}.Node" />.
            /// </summary>
            /// <value>A collection containing all child branch nodes in the <see cref="Tree{T}.Node" />.</value>
            public IEnumerable<Tree<T>.Node> ChildBranches => this._ChildBranches;

            /// <summary>
            /// Gets the number of branches in the <see cref="Tree{T}.Node" />.
            /// </summary>
            /// <value>The number of branches in the <see cref="Tree{T}.Node" />. The value is recursive.</value>
            public int Count => this._Count;

            /// <summary>
            /// Gets the root branch of the <see cref="Tree{T}.Node" />.
            /// </summary>
            /// <value>The root branch of the <see cref="Tree{T}.Node" />, or <c>null</c> if the node is at the top of the tree.</value>
            public Tree<T>.Node RootBranch => this._RootBranch;

            /// <summary>
            /// Gets the <see cref="Tree{T}" /> the <see cref="Tree{T}.Node" /> is a branch of.
            /// </summary>
            /// <value>The <see cref="Tree{T}" /> the <see cref="Tree{T}.Node" /> is a branch of.</value>
            public Tree<T> Tree => this._Tree;

            /// <summary>
            /// Gets or sets the value of the <see cref="Tree{T}.Node" />.
            /// </summary>
            /// <value>The value of the <see cref="Tree{T}.Node" />.</value>
            public T Value
            {
                get => this._Value;
                set => this._Value = value;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Tree{T}.Node" /> class that contains the default value for type <c><typeparamref name="T" /></c> with no child branches.
            /// </summary>
            public Node() : this(default(T)) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="Tree{T}.Node" /> class that contains the specified value with no child branches.
            /// </summary>
            public Node(T val)
            {
                this._ChildBranches = new LinkedList<Tree<T>.Node>();
                this._Value = val;
            }

            public IEnumerator<Tree<T>.Node> GetEnumerator() => new Tree<T>.Node.Enumerator(this);
            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
            public override String ToString() => this.ToString(false);
            public String ToString(bool recursive) => throw new NotImplementedException();
        }
    }
}