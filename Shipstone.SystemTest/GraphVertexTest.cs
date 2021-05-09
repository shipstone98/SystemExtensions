using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Structures;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class GraphVertexTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            const String STRING = "Hello, world!";
            Graph<int, String>.Vertex vertex = new Graph<int, String>.Vertex(STRING);
            Assert.IsNotNull(vertex);
            Assert.AreEqual(0, vertex.Count);
            Assert.IsNotNull(vertex.Edges);
            Assert.AreEqual(0, vertex.Edges.Count());
            Assert.IsNull(vertex.Graph);
            Assert.AreEqual(STRING, vertex.Value);
        }
    }
}