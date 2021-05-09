using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Structures
{
    /// <summary>
    /// Represents a graph containing objects represented by vertices and connected by edges.
    /// </summary>
    /// <typeparam name="TEdge">The type of objects contained in edges contained in the graph.</typeparam>
    /// <typeparam name="TVertex">The type of objects contained in vertices contained in the graph.</typeparam>
    public partial class Graph<TEdge, TVertex> : ICollection<Graph<TEdge, TVertex>.Vertex>, IReadOnlyCollection<Graph<TEdge, TVertex>.Vertex>
    {
        private readonly ICollection<Graph<TEdge, TVertex>.Edge> _Edges;
        private readonly ICollection<Graph<TEdge, TVertex>.Vertex> _Vertices;

        /// <summary>
        /// Gets the number of vertices contained in the graph.
        /// </summary>
        /// <value>The number of vertices contained in the graph.</value>
        public int Count => this._Vertices.Count;

        /// <summary>
        /// Gets a collection containing the edges connecting vertices contained in the graph.
        /// </summary>
        /// <value>A collection containing the edges connecting vertices contained in the graph.</value>
        public IEnumerable<Graph<TEdge, TVertex>.Edge> Edges => this._Edges;

        /// <summary>
        /// Gets a value indicating whether the graph is directed. A directed graph is one whose edges may only be traversed from the source to the destination vertices.
        /// </summary>
        /// <value><c>true</c> if the graph is directed (i.e. uni-directional); otherwise, <c>false</c> if the graph is bi-directional.</value>
        public bool IsDirected { get; }

        bool ICollection<Graph<TEdge, TVertex>.Vertex>.IsReadOnly => false;

        /// <summary>
        /// Gets a collection containing vertices contained in the graph.
        /// </summary>
        /// <value>A collection containing vertices contained in the graph.</value>
        public IEnumerable<Graph<TEdge, TVertex>.Vertex> Vertices => this._Vertices;

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{TEdge, TVertex}" /> class that may be directed or not.
        /// </summary>
        /// <param name="isDirected"><c>true</c> if the graph is directed (i.e. uni-directional); otherwise, <c>false</c> if the graph is bi-directional.</param>
        public Graph(bool isDirected)
        {
            this._Edges = new HashSet<Graph<TEdge, TVertex>.Edge>();
            this._Vertices = new List<Graph<TEdge, TVertex>.Vertex>();
            this.IsDirected = isDirected;
        }

        public Graph(Graph<TEdge, TVertex> graph) => throw new NotImplementedException();

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