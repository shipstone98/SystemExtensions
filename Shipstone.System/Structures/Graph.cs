using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Structures
{
    public partial class Graph<TEdge, TVertex> : ICollection<Graph<TEdge, TVertex>.Vertex>, IReadOnlyCollection<Graph<TEdge, TVertex>.Vertex>
    {
        private readonly ICollection<Graph<TEdge, TVertex>.Edge> _Edges;
        private readonly ICollection<Graph<TEdge, TVertex>.Vertex> _Vertices;

        public int Count => this._Vertices.Count;
        public bool IsDirected { get; }
        bool ICollection<Graph<TEdge, TVertex>.Vertex>.IsReadOnly => false;

        public Graph(bool isDirected)
        {
            this._Edges = new HashSet<Graph<TEdge, TVertex>.Edge>();
            this._Vertices = new HashSet<Graph<TEdge, TVertex>.Vertex>();
            this.IsDirected = isDirected;
        }

        public Graph(Graph<TEdge, TVertex> graph) : this(graph is null ? throw new ArgumentNullException(nameof (graph)) : graph.IsDirected)
        {
            foreach (Graph<TEdge, TVertex>.Vertex vertex in graph._Vertices)
            {
                this._Vertices.Add(vertex);

                foreach (Graph<TEdge, TVertex>.Edge edge in vertex)
                {
                    this._Edges.Add(edge);
                }
            }
        }

        public void Add(Graph<TEdge, TVertex>.Vertex vertex) => throw new NotImplementedException();
        public void AddRange(IEnumerable<Graph<TEdge, TVertex>.Vertex> collection) => throw new NotImplementedException();
        public void Clear() => throw new NotImplementedException();
        public bool Contains(Graph<TEdge, TVertex>.Vertex vertex) => throw new NotImplementedException();
        public bool ContainsRange(IEnumerable<Graph<TEdge, TVertex>.Vertex> collection) => throw new NotImplementedException();
        public void CopyTo(Graph<TEdge, TVertex>.Vertex[] array) => this.CopyTo(array, 0);
        public void CopyTo(Graph<TEdge, TVertex>.Vertex[] array, int arrayIndex) => throw new NotImplementedException();
        public IEnumerator<Graph<TEdge, TVertex>.Vertex> GetEnumerator() => this._Vertices.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
        public bool Remove(Graph<TEdge, TVertex>.Vertex vertex) => throw new NotImplementedException();
        public int RemoveRange(IEnumerable<Graph<TEdge, TVertex>.Vertex> collection) => throw new NotImplementedException();
        public Graph<TEdge, TVertex>.Vertex[] ToArray() => throw new NotImplementedException();
    }
}