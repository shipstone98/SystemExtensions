using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    partial class Tree<T>
    {
        private class Enumerator : IEnumerator<T>
        {
            private T _Current;
            private bool _IsDisposed;
            private bool _IsEnd;
            private bool _IsLastMoveUp;
            internal bool _IsModified;
            private readonly Stack<LinkedListNode<Tree<T>.Node>> _Nodes;
            private readonly Tree<T> _Tree;

            public T Current => this._Current;
            Object IEnumerator.Current => this._Current;

            internal Enumerator(Tree<T> tree)
            {
                this._Nodes = new Stack<LinkedListNode<Tree<T>.Node>>();
                this._Tree = tree;
                this._Tree._Enumerators.Add(this);
            }

            private void _Check()
            {
                if (this._IsDisposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                if (this._IsModified)
                {
                    throw new InvalidOperationException("The collection was modified after the enumerator was created.");
                }
            }

            private void _Dispose(bool disposing)
            {
                if (this._IsDisposed)
                {
                    return;
                }

                if (disposing)
                {
                    this._Tree._Enumerators.Remove(this);
                }

                this._IsDisposed = true;
            }

            private bool _MoveNext()
            {
                LinkedListNode<Tree<T>.Node> node;

                if (this._IsLastMoveUp)
                {
                    this._IsLastMoveUp = false;

                    if ((node = this._Nodes.Peek()) is null)
                    {
                        this._Nodes.Pop();
                        node = this._Nodes.Peek();
                        this._IsLastMoveUp = true;
                        return this._MoveNext();
                    }

                    this._Current = node.Value._Value;
                    return true;
                }

                if (this._Nodes.Count == 0)
                {
                    node = this._Tree._RootBranches.First;

                    if (node is null)
                    {
                        return false;
                    }

                    this._Nodes.Push(node);
                    this._Current = node.Value._Value;
                    return true;
                }

                node = this._Nodes.Peek();
                LinkedListNode<Tree<T>.Node> branch = node.Value._ChildBranches.First;

                if (!(branch is null))
                {
                    this._Nodes.Push(branch);
                    this._Current = branch.Value._Value;
                    return true;
                }

                node = this._Nodes.Pop();

                if ((node = node.Next) is null)
                {
                    if (this._Nodes.Count == 0)
                    {
                        this._IsEnd = true;
                        return false;
                    }
                    
                    node = this._Nodes.Pop();
                    node = node.Next;
                    this._IsLastMoveUp = true;
                    return this._MoveNext();
                }

                this._Current = node.Value._Value;
                this._Nodes.Push(node);
                return true;
            }

            public void Dispose()
            {
                this._Dispose(true);
                GC.SuppressFinalize(this);
            }

            public bool MoveNext()
            {
                this._Check();
                return !this._IsEnd && this._MoveNext();
            }

            public void Reset()
            {
                this._Check();
                this._IsEnd = false;
                this._Nodes.Clear();
            }
        }
    }
}