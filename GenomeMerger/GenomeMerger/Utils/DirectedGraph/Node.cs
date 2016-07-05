using System;
using System.Collections.Generic;

namespace GenomMerger.Utils.DirectedGraph
{
    public class Node<TElement> where TElement : ILeftToRightRelation<TElement>
    {
        /// <summary>
        /// The list of nodes the current attaches to.
        /// </summary>
        private List<Node<TElement>> _neighbours;

        public TElement Data { get; private set; }
        public IEnumerable<Node<TElement>> Neighbours
        {
            get
            {
                return _neighbours;
            }
        }
        
        public Node(TElement data)
        {
            Data = data;
            _neighbours = new List<Node<TElement>>();
        }

        public void Attach(Node<TElement> nodeB)
        {
            if(nodeB == null)
            {
                throw new ArgumentNullException("Cannot attach null nodes");
            }

            _neighbours.Add(nodeB);
        }
    }
}