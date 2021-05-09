using System;

namespace Shipstone.System.Structures
{
    partial class Graph<TEdge, TVertex>
    {
        public class Edge
        {
            public Graph<TEdge, TVertex>.Vertex Destination { get; internal set; }
            public Graph<TEdge, TVertex> Graph { get; internal set; }
            public Graph<TEdge, TVertex>.Vertex Source { get; internal set; }
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