using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    partial class Tree<T>
    {
        partial class Node
        {
            internal class Enumerator : IEnumerator<T>
            {
                private readonly Tree<T>.Node _Node;

                internal Enumerator(Tree<T>.Node node)
                {
                    this._Node = node;
                }
            }
        }
    }
}