using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Structures
{
    partial class Graph<TEdge, TVertex>
    {
        public class Vertex : IReadOnlyCollection<Graph<TEdge, TVertex>.Edge>
        {
            private readonly ICollection<Graph<TEdge, TVertex>.Edge> _Edges;

            public int Count => this._Edges.Count;
            public IEnumerable<Graph<TEdge, TVertex>.Edge> Edges => this._Edges;
            public Graph<TEdge, TVertex> Graph { get; internal set; }
            public TVertex Value { get; set; }

            public Vertex(TVertex val)
            {
                this._Edges = new LinkedList<Graph<TEdge, TVertex>.Edge>();
                this.Value = val;
            }

            public void ConnectTo(Graph<TEdge, TVertex>.Vertex vertex) => throw new NotImplementedException();
            public IEnumerator<Graph<TEdge, TVertex>.Edge> GetEnumerator() => this._Edges.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => this._Edges.GetEnumerator();
        }
    }
}