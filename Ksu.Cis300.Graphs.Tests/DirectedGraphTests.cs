/* DirectedGraphTests.cs
 * Author: Rod Howell
 */
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Ksu.Cis300.Graphs.Tests
{
    /// <summary>
    /// Unit tests for the DirectedGraph class.
    /// </summary>
    [TestFixture]
    public class DirectedGraphTests
    {
        /// <summary>
        /// Tests that the Nodes property of an empty graph gives an empty collection.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAEmptyNodesEnumerator()
        {
            DirectedGraph<string, int> g = new DirectedGraph<string, int>();
            List<string> results = new List<string>();
            foreach (string s in g.Nodes)
            {
                results.Add(s);
            }
            Assert.That(results, Is.Empty);
        }

        /// <summary>
        /// Tests that NodeCount is a property with no set accessor.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestANodeCountNoSet()
        {
            Type type = new DirectedGraph<string, string>().GetType();
            Assert.That(type.GetProperty("NodeCount").SetMethod, Is.Null);
        }

        /// <summary>
        /// Tests that EdgeCount is a property with no set accessor.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAEdgeCountNoSet()
        {
            Type type = new DirectedGraph<int, decimal>().GetType();
            Assert.That(type.GetProperty("EdgeCount").SetMethod, Is.Null);
        }

        /// <summary>
        /// Tests that Nodes is a property with no set accessor.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestANodesNoSet()
        {
            Type type = new DirectedGraph<string, float>().GetType();
            Assert.That(type.GetProperty("Nodes").SetMethod, Is.Null);
        }

        /// <summary>
        /// Tests that NodeCount returns 0 when the graph is empty.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAEmptyNodeCount()
        {
            DirectedGraph<string, string> g = new DirectedGraph<string, string>();
            Assert.That(g.NodeCount, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that ContainsEdge returns false when the graph is empty.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAEmptyEdgeCount()
        {
            DirectedGraph<int, string> g = new DirectedGraph<int, string>();
            Assert.That(g.EdgeCount, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that a graph contains a node after it has been added.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBAddNodeLookItUp()
        {
            DirectedGraph<int, string> g = new DirectedGraph<int, string>();
            bool before = g.ContainsNode(0);
            g.AddNode(0);
            Assert.Multiple(() =>
            {
                Assert.That(before, Is.False);
                Assert.That(g.ContainsNode(0), Is.True);
            });
        }

        /// <summary>
        /// Tests that there are 3 nodes after 3 nodes are added.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBAddThreeNodesCount()
        {
            DirectedGraph<string, string> g = new DirectedGraph<string, string>();
            g.AddNode("add");
            g.AddNode("three");
            g.AddNode("nodes");
            Assert.That(g.NodeCount, Is.EqualTo(3));
        }

        /// <summary>
        /// Tests that the Nodes property iterates through three added nodes.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBAddThreeNodesIterate()
        {
            DirectedGraph<int, string> g = new DirectedGraph<int, string>();
            g.AddNode(1);
            g.AddNode(2);
            g.AddNode(5);
            List<int> nodes = new List<int>();
            foreach (int i in g.Nodes)
            {
                nodes.Add(i);
            }
            Assert.That(nodes, Is.EquivalentTo(new int[] { 1, 2, 5 }));
        }

        /// <summary>
        /// Tests that both the source and destination nodes of an added edge are
        /// added to the graph.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCAddsBothNodes()
        {
            int[] nodes = new int[] { 1, 2 };
            DirectedGraph<int, string> g = new DirectedGraph<int, string>();
            g.AddEdge(nodes[0], nodes[1], null);
            Assert.That(g.Nodes, Is.EquivalentTo(nodes));
        }

        /// <summary>
        /// Tests that an ArgumentNullException is thrown and the graph is unchanged 
        /// if AddEdge is called with a null source node.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCAddNullSource()
        {
            DirectedGraph<string, int> g = new DirectedGraph<string, int>();
            g.AddEdge("a", "b", 7);
            Exception e = null;
            try
            {
                g.AddEdge(null, "c", 3);
            }
            catch (Exception ex)
            {
                e = ex;
            }
            Assert.Multiple(() =>
            {
                Assert.That(e, Is.Not.Null.And.TypeOf(typeof(ArgumentNullException)));
                Assert.That(g.Nodes, Is.EquivalentTo(new string[] { "a", "b" }));
            });
        }

        /// <summary>
        /// Tests that an ArgumentException is thrown and the graph is unchanged 
        /// if an edge from a node to itself is added.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCAddEqualNodes()
        {
            DirectedGraph<int, int> g = new DirectedGraph<int, int>();
            g.AddEdge(1, 2, 10);
            Exception e = null;
            try
            {
                g.AddEdge(3, 3, 3);
            }
            catch (Exception ex)
            {
                e = ex;
            }
            Assert.Multiple(() =>
            {
                Assert.That(e, Is.Not.Null.And.TypeOf(typeof(ArgumentException)));
                Assert.That(g.Nodes, Is.EquivalentTo(new int[] { 1, 2 }));
            });
        }

        /// <summary>
        /// Tests that a single edge can be added and retrieved.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestDRetrieveOneEdge()
        {
            DirectedGraph<int, int> g = new DirectedGraph<int, int>();
            g.AddEdge(1, 2, 3);
            bool found = g.TryGetEdge(1, 2, out int data);
            Assert.Multiple(() =>
            {
                Assert.That(found, Is.True);
                Assert.That(data, Is.EqualTo(3));
            });
        }

        /// <summary>
        /// Tests that ContainsEdge can find an edge that has been added.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestDFindOneEdge()
        {
            DirectedGraph<int, string> g = new DirectedGraph<int, string>();
            g.AddEdge(1, 2, "three");
            Assert.That(g.ContainsEdge(1, 2), Is.True);
        }

        [Test, Timeout(1000)]
        public void TestDAddOneEdgeIterateOutgoing()
        {
            DirectedGraph<int, int> g = new DirectedGraph<int, int>();
            g.AddEdge(1, 2, 3);
            List<Edge<int, int>> results = new List<Edge<int, int>>();
            foreach (Edge<int, int> e in g.OutgoingEdges(1))
            {
                results.Add(e);
            }
            Assert.That(results, Is.EquivalentTo(new Edge<int, int>[] { new Edge<int, int>(1, 2, 3) }));
        }

        /// <summary>
        /// Tests that an ArgumentNullException is thrown and the graph is unchanged 
        /// if AddEdge is called with a null destination node.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEAddNullDestination()
        {
            DirectedGraph<string, int> g = new DirectedGraph<string, int>();
            g.AddEdge("a", "b", 7);
            Exception e = null;
            try
            {
                g.AddEdge("a", null, 3);
            }
            catch (Exception ex)
            {
                e = ex;
            }
            Assert.Multiple(() =>
            {
                Assert.That(e, Is.Not.Null.And.TypeOf(typeof(ArgumentNullException)));
                Assert.That(g.OutgoingEdges("a"), Is.EquivalentTo(new Edge<string, int>[] { new Edge<string, int>("a", "b", 7) }));
            });
        }

        /// <summary>
        /// Tests that adding an edge that already exists throws an ArgumentException and
        /// leaves the graph unchanged.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEAddDuplicateEdge()
        {
            DirectedGraph<int, string> g = new DirectedGraph<int, string>();
            Exception e = null;
            g.AddEdge(1, 2, "a");
            try
            {
                g.AddEdge(1, 2, "b");
            }
            catch (Exception ex)
            {
                e = ex;
            }
            Assert.Multiple(() =>
            {
                Assert.That(e, Is.Not.Null.And.TypeOf(typeof(ArgumentException)));
                Assert.That(g.OutgoingEdges(1), Is.EquivalentTo(new Edge<int, string>[] { new Edge<int, string>(1, 2, "a") }));
            });
        }

        /// <summary>
        /// Tests that multiple edges from the same node may be added and iterated through.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEMultipleEdgesFromOneNode()
        {
            DirectedGraph<int, int> g = new DirectedGraph<int, int>();
            Edge<int, int>[] edges = new Edge<int, int>[]
            {
                new Edge<int, int>(1, 2, 20),
                new Edge<int, int>(1, 3, 30),
                new Edge<int, int>(1, 4, 40)
            };
            foreach (Edge<int, int> e in edges)
            {
                g.AddEdge(e.Source, e.Destination, e.Data);
            }
            Assert.Multiple(() =>
            {
                Assert.That(g.OutgoingEdges(1), Is.EquivalentTo(edges));
                Assert.That(g.EdgeCount, Is.EqualTo(edges.Length));
            });
        }

        /// <summary>
        /// Tests adding an edge to an existing node.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFAddEdgeToExistingNode()
        {
            DirectedGraph<int, int> g = new DirectedGraph<int, int>();
            Edge<int, int>[] edges = new Edge<int, int>[]
            {
                new Edge<int, int>(1, 2, 10),
                new Edge<int, int>(0, 1, 20)
            };
            foreach (Edge<int, int> e in edges)
            {
                g.AddEdge(e.Source, e.Destination, e.Data);
            }
            List<Edge<int, int>> result = new List<Edge<int, int>>();
            foreach (int node in g.Nodes)
            {
                foreach (Edge<int, int> e in g.OutgoingEdges(node))
                {
                    result.Add(e);
                }
            }
            Assert.That(result, Is.EquivalentTo(edges));
        }

        /// <summary>
        /// Tests adding and iterating through multiple edges.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestHSeveralEdges()
        {
            DirectedGraph<int, int> g = new DirectedGraph<int, int>();
            Edge<int, int>[] edges = new Edge<int, int>[]
            {
                new Edge<int, int>(1, 2, 10),
                new Edge<int, int>(2, 3, 20),
                new Edge<int, int>(1, 4, 30),
                new Edge<int, int>(4, 1, 40),
                new Edge<int, int>(3, 4, 50),
                new Edge<int, int>(1, 5, 60)
            };
            foreach (Edge<int, int> e in edges)
            {
                g.AddEdge(e.Source, e.Destination, e.Data);
            }
            List<Edge<int, int>> result = new List<Edge<int, int>>();
            foreach (int node in g.Nodes)
            {
                foreach (Edge<int, int> e in g.OutgoingEdges(node))
                {
                    result.Add(e);
                }
            }
            Assert.That(result, Is.EquivalentTo(edges));
        }

        /// <summary>
        /// Tests the graph's performance by adding 1000000 edges from the same node, then
        /// iterating through them.
        /// </summary>
        [Test, Timeout(5000)]
        public void TestIPerformance()
        {
            DirectedGraph<int, int> g = new DirectedGraph<int, int>();
            for (int i = 1; i <= 1000000; i++)
            {
                g.AddEdge(0, i, i);
            }
            long sum = 0;
            foreach (Edge<int, int> e in g.OutgoingEdges(0))
            {
                sum += e.Data;
            }
            Assert.That(sum, Is.EqualTo(500000500000L));
        }
    }
}