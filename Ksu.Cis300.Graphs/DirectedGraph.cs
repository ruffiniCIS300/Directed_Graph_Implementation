/*
 * DirectedGraph.cs
 * Author: Nick Ruffini
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ksu.Cis300.LinkedListLibrary;

namespace Ksu.Cis300.Graphs
{
    /// <summary>
    /// Class that represents the implementation of our graph!
    /// </summary>
    /// <typeparam name="TNode"> Type of each node in our graph </typeparam>
    /// <typeparam name="TEdgeData"> Type of the edge data in our graph </typeparam>
    public class DirectedGraph<TNode, TEdgeData>
    {
        /// <summary>
        /// Dictionary whose keys are node names and values are a linked list of all nodes 
        /// adjacent to this one
        /// </summary>
        private Dictionary<TNode, LinkedListCell<TNode>> _adjList = new Dictionary<TNode, LinkedListCell<TNode>>();
        /// <summary>
        /// Dictionary whose keys are tuples containing the source and destination node, and whose values
        /// are the weight of this edge
        /// </summary>
        private Dictionary<(TNode, TNode), TEdgeData> _edges = new Dictionary<(TNode, TNode), TEdgeData>();
        /// <summary>
        /// Gets the Node names in the graph
        /// </summary>
        public IEnumerable<TNode> Nodes => _adjList.Keys;
        /// <summary>
        /// Returns the number of nodes in the graph
        /// </summary>
        public int NodeCount => _adjList.Count;
        /// <summary>
        /// Returns the number of edges in the graph
        /// </summary>
        public int EdgeCount => _edges.Count;

        /// <summary>
        /// Adds the given node to the graph!
        /// </summary>
        /// <param name="node"> Node being added to the graph </param>
        public void AddNode(TNode node)
        {
            _adjList.Add(node, null);
        }
        /// <summary>
        /// Try to get the data associated with the edge between source and dest!
        /// </summary>
        /// <param name="source"> Source Node of edge </param>
        /// <param name="dest"> Destinatino Node of edge </param>
        /// <param name="value"> Our parameter that stores the data of the edge if found </param>
        /// <returns> A bool whether or not we found an edge </returns>
        public bool TryGetEdge(TNode source, TNode dest, out TEdgeData value)
        {
            return _edges.TryGetValue((source, dest), out value);
        }
        /// <summary>
        /// Determines whether or not the given node is in the graph!
        /// </summary>
        /// <param name="node"> Node we are looking for in the graph </param>
        /// <returns> True or false, depending on whether or not the node is in the graph </returns>
        public bool ContainsNode(TNode node)
        {
            return _adjList.ContainsKey(node);
        }
        /// <summary>
        /// Determines whether or not there is an edge between the 2 given nodes
        /// </summary>
        /// <param name="source"> Source of the edge we are looking for </param>
        /// <param name="dest"> Destination of the edge we are looking for </param>
        /// <returns> Whether or not we found an edge </returns>
        public bool ContainsEdge(TNode source, TNode dest)
        {
            return _edges.ContainsKey((source, dest));
        }

        /// <summary>
        /// Trys to add an edge between the source and destination node with the given value!
        /// </summary>
        /// <param name="source"> Source node we are working with </param>
        /// <param name="dest"> Destination node we are working with </param>
        /// <param name="value"> Value we are associating with this edge </param>
        public void AddEdge(TNode source, TNode dest, TEdgeData value)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentNullException();
            }
            else if (source.Equals(dest))
            {
                throw new ArgumentException();
            }
            else
            {
                _edges.Add((source, dest), value);

                LinkedListCell<TNode> head;
                _adjList.TryGetValue(source, out head);

                LinkedListCell<TNode> tempCell = new LinkedListCell<TNode>();
                tempCell.Data = dest;
                tempCell.Next = head;

                _adjList[source] = tempCell;
            }

            if (ContainsNode(dest) == false)
            {
                AddNode(dest);
            }
        }
        /// <summary>
        /// Implements an enumerator for the outgoing edges from the given node
        /// </summary>
        /// <param name="node"> Node who we are looking at their outgoing edges </param>
        /// <returns> An IEnumerable for all edges leaving this node </returns>
        public IEnumerable<Edge<TNode, TEdgeData>> OutgoingEdges(TNode node)
        {
            LinkedListCell<TNode> head = _adjList[node];

            LinkedListCell<TNode> tempCell = head;

            while (tempCell != null)
            {
                TNode destinationNode = tempCell.Data;
                TEdgeData edgeWeight = _edges[(node, destinationNode)];

                Edge<TNode, TEdgeData> outgoingEdge = new Edge<TNode, TEdgeData>(node, destinationNode, edgeWeight);

                yield return outgoingEdge;

                tempCell = tempCell.Next;
            }

        }
    }
}
