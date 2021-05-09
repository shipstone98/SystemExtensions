using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Structures;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class GraphTest
    {
        private const bool _UseDirected = true;

        private Graph<int, String> _Graph;

        private void AssertGraph(int edgeCount, int vertexCount, bool isDirected) => GraphTest.AssertGraph(this._Graph, edgeCount, vertexCount, isDirected);

        private static void AssertGraph<TEdge, TVertex>(Graph<TEdge, TVertex> graph, int edgeCount, int vertexCount, bool isDirected)
        {
            Assert.AreEqual(vertexCount, graph.Count);
            Assert.IsNotNull(graph.Edges);
            Assert.AreEqual(edgeCount, graph.Edges.Count());
            Assert.AreEqual(isDirected, graph.IsDirected);
            Assert.IsNotNull(graph.Vertices);
            Assert.AreEqual(vertexCount, graph.Vertices.Count());
        }

        [TestInitialize]
        public void Initialize() => this._Graph = new Graph<int, String>(GraphTest._UseDirected);

        [TestMethod]
        public void TestConstructor_Boolean() => this.AssertGraph(0, 0, GraphTest._UseDirected);

#region Add method
        [TestMethod]
        public void TestAdd_ContainedAlready()
        {
            const String STRING = "Hello, world!";
            Graph<int, String>.Vertex vertex = new Graph<int, String>.Vertex(STRING);
            this._Graph.Add(vertex);
            Exception ex = Assert.ThrowsException<InvalidOperationException>(() => this._Graph.Add(vertex));
            Assert.AreEqual("vertex belongs to another Graph<TEdge, TVertex>.", ex.Message);
            this.AssertGraph(0, 1, GraphTest._UseDirected);
        }

        [TestMethod]
        public void TestAdd_ContainedInOther()
        {
            const String STRING = "Hello, world!";
            Graph<int, String>.Vertex vertex = new Graph<int, String>.Vertex(STRING);
            this._Graph.Add(vertex);
            Graph<int, String> newGraph = new Graph<int, String>(this._Graph.IsDirected);
            Exception ex = Assert.ThrowsException<InvalidOperationException>(() => newGraph.Add(vertex));
            Assert.AreEqual("vertex belongs to another Graph<TEdge, TVertex>.", ex.Message);
            this.AssertGraph(0, 1, GraphTest._UseDirected);
            GraphTest.AssertGraph(newGraph, 0, 0, GraphTest._UseDirected);
        }

        [TestMethod]
        public void TestAdd_NotContained()
        {
            const String STRING = "Hello, world!";
            Graph<int, String>.Vertex vertex = new Graph<int, String>.Vertex(STRING);
            this._Graph.Add(vertex);
            this.AssertGraph(0, 1, GraphTest._UseDirected);
        }

        [TestMethod]
        public void TestAdd_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => this._Graph.Add(null));
            this.AssertGraph(0, 0, GraphTest._UseDirected);
        }
#endregion
    }
}