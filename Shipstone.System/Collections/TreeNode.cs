using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    partial class Tree<T>
    {
        public class Node : IReadOnlyCollection<Tree<T>.Node>
        {
            internal readonly ICollection<Tree<T>.Node> _Branches;
            internal Tree<T>.Node _Root;
            internal Tree<T> _Tree;
            private T _Value;

            public IEnumerable<Tree<T>.Node> Branches => this._Branches;
            public int Count => throw new NotImplementedException();
            public Tree<T>.Node Root => this._Root;
            public Tree<T> Tree => this._Tree;

            public T Value
            {
                get => this._Value;
                set => this._Value = value;
            }

            public Node(T val)
            {
                this._Branches = new LinkedList<Tree<T>.Node>();
                this._Value = val;
            }

            public IEnumerator<Tree<T>.Node> GetEnumerator() => this.GetEnumerator(false);
            public IEnumerator<Tree<T>.Node> GetEnumerator(bool recursive) => throw new NotImplementedException();
            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator(false);
            public override String ToString() => this.ToString(false);
            public String ToString(bool recursive) => throw new NotImplementedException();
        }
    }
}