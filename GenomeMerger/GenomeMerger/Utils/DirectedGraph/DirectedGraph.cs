using System.Collections.Generic;
using System.Linq;

namespace GenomMerger.Utils.DirectedGraph
{
    public class DirectedGraph<TElement> where TElement : ILeftToRightRelation<TElement>
    {
        private List<Node<TElement>> _nodes;
        public IEnumerable<Node<TElement>> Nodes { get {
                return _nodes;
            }
        }

        public DirectedGraph()
        {
            _nodes = new List<Node<TElement>>();
        }

        public static DirectedGraph<TElement> BuildGraph(IEnumerable<TElement> elements)
        {
            if(elements == null)
            {
                return null;
            }

            var graph = new DirectedGraph<TElement>();
             
            // Create nodes
            foreach(var element in elements)
            {
                graph._nodes.Add(new Node<TElement>(element));
            }

            graph.Build();

            return graph;
        }        

        private void Build()
        {
            for(int i = 0; i < _nodes.Count; i++)
            {
                for (int j = i + 1; j < _nodes.Count; j++)
                {
                    var nodeA = _nodes[i];
                    var nodeB = _nodes[j];

                    if (nodeA.Data.IsRelate(nodeB.Data))
                    {
                        nodeA.Attach(nodeB);
                    }

                    if(nodeB.Data.IsRelate(nodeA.Data))
                    {
                        nodeB.Attach(nodeA);
                    }
                }
            }
        }       
    }
}
