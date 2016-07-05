using GenomMerger;
using GenomMerger.Utils.DirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GenomMergerTest
{
    [TestClass]
    public class DirectedGraphTest
    {
        [TestMethod]
        public void BuildGraph_BuildGraphOfSmallSet()
        {
            var partA = new GenomePart("ATTAGACCTG");
            var partB = new GenomePart("CCTGCCGGAA");
            var partC = new GenomePart("AGACCTGCCG");
            var partD = new GenomePart("GCCGGAATAC");
            var genomeParts = new List<GenomePart>() { partA, partB, partC, partD };
            var graph = DirectedGraph<GenomePart>.BuildGraph(genomeParts);

            var graphNodes = graph.Nodes.ToList();

            var partANode = graphNodes.Find(node => node.Data.Equals(partA));
            Assert.IsNotNull(partANode, "PartA is null");
            var partANodeNeighbours = partANode.Neighbours.ToList();
            Assert.IsNotNull(partANodeNeighbours.Find(node => node.Data.Equals(partC)), "PartA missing neighbours");
            Assert.AreEqual(1, partANodeNeighbours.Count, "PartA unexpected neighbours count");

            var partBNode = graphNodes.Find(node => node.Data.Equals(partB));
            Assert.IsNotNull(partBNode, "PartB is null");
            var partBNodeNeighbours = partBNode.Neighbours.ToList();
            Assert.IsNotNull(partBNodeNeighbours.Find(node => node.Data.Equals(partD)), "PartB missing neighbours");
            Assert.AreEqual(1, partBNodeNeighbours.Count, "PartB unexpected neighbours count");

            var partCNode = graphNodes.Find(node => node.Data.Equals(partC));
            Assert.IsNotNull(partCNode, "PartC is null");
            var partCNodeNeighbours = partCNode.Neighbours.ToList();
            Assert.IsNotNull(partCNodeNeighbours.Find(node => node.Data.Equals(partB)), "PartC missing neighbours");
            Assert.AreEqual(1, partCNodeNeighbours.Count, "PartC unexpected neighbours count");

            var partDNode = graphNodes.Find(node => node.Data.Equals(partD));
            Assert.IsNotNull(partDNode, "PartD is null");
            var partDNodeNeighbours = partDNode.Neighbours.ToList();
            Assert.AreEqual(0, partDNodeNeighbours.Count, "PartD unexpected neighbours count");
        }
    }
}
