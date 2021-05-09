using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Structures
{
    partial class Graph<TEdge, TVertex>
    {
        /// <summary>
        /// Represents a vertex containing zero or more edges contained in a <see cref="Graph{TEdge, TVertex}" />.
        /// </summary>
        public class Vertex : IReadOnlyCollection<Graph<TEdge, TVertex>.Edge>
        {
            private readonly ICollection<Graph<TEdge, TVertex>.Edge> _Edges;

            /// <summary>
            /// Gets the number of edges connected to the current <see cref="Graph{TEdge, TVertex}.Vertex" />.
            /// </summary>
            /// <value>The number of edges connected to the current <see cref="Graph{TEdge, TVertex}.Vertex" />.</value>
            public int Count => this._Edges.Count;

            /// <summary>
            /// Gets a collection containing the edges connected to the current <see cref="Graph{TEdge, TVertex}.Vertex" />.
            /// </summary>
            /// <value>A collection containing the edges connected to the current <see cref="Graph{TEdge, TVertex}.Vertex" />.</value>
            public IEnumerable<Graph<TEdge, TVertex>.Edge> Edges => this._Edges;

            /// <summary>
            /// Gets the <see cref="Graph{TEdge, TVertex}" /> the current <see cref="Graph{TEdge, TVertex}.Vertex" /> is contained within.
            /// </summary>
            /// <value>The <see cref="Graph{TEdge, TVertex}" /> the current <see cref="Graph{TEdge, TVertex}.Vertex" /> is contained within, or <c>null</c> if it is not connected.</value>
            public Graph<TEdge, TVertex> Graph { get; internal set; }

            /// <summary>
            /// Gets or sets the value of the current <see cref="Graph{TEdge, TVertex}.Vertex" />.
            /// </summary>
            /// <value>The value of the current <see cref="Graph{TEdge, TVertex}.Vertex" />.</value>
            public TVertex Value { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Graph{TEdge, TVertex}.Vertex" /> class that contains the specified value and is unconnected from any <see cref="Graph{TEdge, TVertex}" />.
            /// </summary>
            /// <param name="val">The value for the <see cref="Graph{TEdge, TVertex}.Vertex" />.</param>
            public Vertex(TVertex val)
            {
                this._Edges = new LinkedList<Graph<TEdge, TVertex>.Edge>();
                this.Value = val;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Graph{TEdge, TVertex}.Vertex" /> class that contains the value of the specified <c><paramref name="vertex" /></c> and is unconnected from any <see cref="Graph{TEdge, TVertex}" />.
            /// </summary>
            /// <param name="vertex">The <see cref="Graph{TEdge, TVertex}.Vertex" /> to copy the value from.</param>
            /// <exception cref="ArgumentNullException"><c><paramref name="vertex" /></c> is <c>null</c>.</exception>
            public Vertex(Graph<TEdge, TVertex>.Vertex vertex) : this(vertex is null ? throw new ArgumentNullException(nameof (vertex)) : vertex.Value) { }

            public void ConnectTo(Graph<TEdge, TVertex>.Vertex vertex) => throw new NotImplementedException();
            public bool DisconnectFrom(Graph<TEdge, TVertex>.Vertex vertex) => throw new NotImplementedException();
            public IEnumerator<Graph<TEdge, TVertex>.Edge> GetEnumerator() => this._Edges.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => this._Edges.GetEnumerator();
            public bool IsConnected(Graph<TEdge, TVertex> vertex) => throw new NotImplementedException();
        }
    }
}