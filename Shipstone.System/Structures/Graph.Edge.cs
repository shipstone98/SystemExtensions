namespace Shipstone.System.Structures
{
    partial class Graph<TEdge, TVertex>
    {
        /// <summary>
        /// Represents an edge connecting two vertices contained in a <see cref="Graph{TEdge, TVertex}" />.
        /// </summary>
        public class Edge
        {
            /// <summary>
            /// Gets the destination, or end, <see cref="Graph{TEdge, TVertex}.Vertex" /> the current <see cref="Graph{TEdge, TVertex}.Edge" /> is connected to.
            /// </summary>
            /// <value>The destination, or end, <see cref="Graph{TEdge, TVertex}.Vertex" /> the current <see cref="Graph{TEdge, TVertex}.Edge" /> is connected to.</value>
            public Graph<TEdge, TVertex>.Vertex Destination { get; internal set; }

            /// <summary>
            /// Gets the <see cref="Graph{TEdge, TVertex}" /> the current <see cref="Graph{TEdge, TVertex}.Edge" /> is contained within.
            /// </summary>
            /// <value>The <see cref="Graph{TEdge, TVertex}" /> the current <see cref="Graph{TEdge, TVertex}.Edge" /> is contained within, or <c>null</c> if it is not connected.</value>
            public Graph<TEdge, TVertex> Graph { get; internal set; }

            /// <summary>
            /// Gets the source, or start, <see cref="Graph{TEdge, TVertex}.Vertex" /> the current <see cref="Graph{TEdge, TVertex}.Edge" /> is connected to.
            /// </summary>
            /// <value>The source, or start, <see cref="Graph{TEdge, TVertex}.Vertex" /> the current <see cref="Graph{TEdge, TVertex}.Edge" /> is connected to.</value>
            public Graph<TEdge, TVertex>.Vertex Source { get; internal set; }

            /// <summary>
            /// Gets or sets the value, or weight, of the current <see cref="Graph{TEdge, TVertex}.Edge" />.
            /// </summary>
            /// <value>The value, or weight, of the current <see cref="Graph{TEdge, TVertex}.Edge" />.</value>
            public TEdge Value { get; set; }

            internal Edge(Graph<TEdge, TVertex>.Vertex source, Graph<TEdge, TVertex>.Vertex dest, Graph<TEdge, TVertex> graph)
            {
                this.Destination = dest;
                this.Graph = graph;
                this.Source = source;
            }

            internal Edge(Graph<TEdge, TVertex>.Vertex source, Graph<TEdge, TVertex>.Vertex dest, Graph<TEdge, TVertex> graph, TEdge val) : this(source, dest, graph) => this.Value = val;
        }
    }
}