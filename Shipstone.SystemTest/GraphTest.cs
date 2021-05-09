using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Structures;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void TestConstructor_Boolean()
        {
            Graph<int, String> graph = new Graph<int, String>(true);
            Assert.AreEqual(0, graph.Count);
            Assert.IsNotNull(graph.Edges);
            Assert.AreEqual(0, graph.Edges.Count());
            Assert.IsTrue(graph.IsDirected);
            Assert.IsNotNull(graph.Vertices);
            Assert.AreEqual(0, graph.Vertices.Count());
        }
    }
}