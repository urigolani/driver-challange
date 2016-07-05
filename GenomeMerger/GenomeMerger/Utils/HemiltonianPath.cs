using GenomMerger.Utils.DirectedGraph;
using System.Collections.Generic;
using System.Linq;

namespace GenomMerger.Utils
{
    public static class HemiltonianPath
    {
        public static IEnumerable<TElement> Find<TElement>(DirectedGraph<TElement> graph) where TElement : ILeftToRightRelation<TElement>
        {
            // Create a node which connects to all others, since the path can start in any of the nodes.
            var startNode = new Node<TElement>(default(TElement));
            foreach(var node in graph.Nodes)
            {
                startNode.Attach(node);
            }

            var nodes = graph.Nodes;
            var path = FindPath(startNode, new Dictionary<Node<TElement>, Node<TElement>>(), graph.Nodes.Count());

            if(path != null)
            {
                // Skip the starting node and get the data.
                return path.Skip(1).Select(node => node.Data);
            }

            return null;
        }

        private static LinkedList<Node<TElement>> FindPath<TElement>(Node<TElement> node, Dictionary<Node<TElement>, Node<TElement>> collectedNodes, int pathLength) where TElement : ILeftToRightRelation<TElement>
        {
            if(collectedNodes.Count == pathLength)
            {
                var path = new LinkedList<Node<TElement>>();
                path.AddFirst(node);
                return path;
            }

            foreach(var neighbour in node.Neighbours)
            {
                if(!collectedNodes.ContainsKey(neighbour))
                {
                    collectedNodes.Add(neighbour, neighbour);
                    var path = FindPath(neighbour, collectedNodes, pathLength);
                    collectedNodes.Remove(neighbour);

                    if (path != null)
                    {
                        // Success - found a hemiltonian path
                        path.AddFirst(node);
                        return path;
                    }
                }
            }

            // Failed to find a path
            return null;
        }
    }
}
