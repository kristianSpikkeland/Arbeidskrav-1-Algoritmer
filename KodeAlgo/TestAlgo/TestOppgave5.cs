using KodeAlgo.Menu;
using KodeAlgo.Oppgave4;
using KodeAlgo.Oppgave5;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestAlgo
{
    public class TestOppgave5
    {

        [Fact]
        public void RecursiveDFS_FromMajorstuen_ShouldReturnSpesificOrder()
        {
            // Arrange
            var graph = new DFSGraph();
            var graphHelper = new MenuOppgave5(graph);
            graphHelper.BuildGraph();

            var expectedOrder = new List<string>
            {"Majorstuen", "Blindern", "Forskningsparken", "Ullevål stadion", "Nationaltheatret",
                "Stortinget", "Jernbanetorget", "Grønland", "Tøyen"};

            // Act
            var actualOrder = graph.TraverseRecursiveDFS(graph._adj, "Majorstuen");


            // Assert
            Assert.Equal(expectedOrder, actualOrder);
        }

        [Fact]
        public void IterativeDFS_FromMajorstuen_ShouldReturnSpesificOrder()
        {
            // Arrange
            var graph = new DFSGraph();
            var graphHelper = new MenuOppgave5(graph);
            graphHelper.BuildGraph();

            var expectedOrder = new List<string>
            {"Majorstuen", "Blindern", "Forskningsparken", "Ullevål stadion", "Nationaltheatret",
                "Stortinget", "Jernbanetorget", "Grønland", "Tøyen"};


            // Act
            var actualOrder = graph.TraverseIterativeDFS(graph._adj, "Majorstuen");


            // Assert
            Assert.Equal(expectedOrder, actualOrder);
        }


        // The iterative version uses OrderByDecending for adjacency list, while the recursive usees OrderBy
        [Fact]
        public void IterativeAndRecursiveDfs_FromMajorstuen_ShouldReturnSameOrder()
        {
            // Arrange
            var graph = new DFSGraph();
            var graphHelper = new MenuOppgave5(graph);
            graphHelper.BuildGraph();

            // Act 
            var iterativeOrder = graph.TraverseIterativeDFS(graph._adj, "Majorstuen");
            var recursiveOrder = graph.TraverseRecursiveDFS(graph._adj, "Majorstuen");

            // Assert
            Assert.Equal(iterativeOrder, recursiveOrder);
        }


        [Fact]
        public void TryFindPath_MajorstuenToIsolatedNode_ShouldReturnFalse()
        {
            // Arrange
            var graph = new DFSGraph();
            var graphHelper = new MenuOppgave5(graph);
            graphHelper.BuildGraph();

            // Act
            graph.AddNode("NyNode");
            bool isPath = graph.TryFindPath("Majorstuen", "NyNode");

            // Assert
            Assert.False(isPath);
        }


        // This test checks that IterativeDFS returns a different path than BFS 
        [Fact]
        public void Traversing_IterativeDfsAndBfs_TShouldReturnDiffrentOrder()
        {
            // Arrange
            var dfsGraph = new DFSGraph();
            var bfsGraph = new BFSGraph();

            var graphHelperDfs = new MenuOppgave5(dfsGraph);
            graphHelperDfs.BuildGraph();

            var graphHelperBfs = new MenuOppgave4(bfsGraph);
            graphHelperBfs.BuildGraph();

            // Act
            var orderDfsIterative = dfsGraph.TraverseIterativeDFS(dfsGraph._adj, "Majorstuen");
            var orderBfs = bfsGraph.TraverseBFS(bfsGraph._adj, "Majorstuen");

            // Assert
            Assert.NotEqual(orderDfsIterative, orderBfs);
        }


        // This test checks that RecursiveDFS returns a different path than BFS 
        [Fact]
        public void Traversing_RecursiveDfs_Bfs_ShouldReturnDiffrentOrder()
        {
            // Arrange
            var dfsGraph = new DFSGraph();
            var bfsGraph = new BFSGraph();

            var graphHelperDfs = new MenuOppgave5(dfsGraph);
            graphHelperDfs.BuildGraph();

            var graphHelperBfs = new MenuOppgave4(bfsGraph);
            graphHelperBfs.BuildGraph();

            // Act
            var orderDfsRecursive = dfsGraph.TraverseRecursiveDFS(dfsGraph._adj, "Majorstuen");
            var orderBfs = bfsGraph.TraverseBFS(bfsGraph._adj, "Majorstuen");

            // Assert
            Assert.NotEqual(orderDfsRecursive, orderBfs);
        }

        // There are 10 nodes in total. 9 of them is connected
        [Fact]
        public void TraverseAll_WhenIsolatedNode_ShouldReturnAll()
        {
            // Arrrange
            var dfsGraph = new DFSGraph();
            var graphHelperDfs = new MenuOppgave5(dfsGraph);
            graphHelperDfs.BuildGraph();

            // Act
            dfsGraph.AddNode("NewStation");
            var orderList = dfsGraph.TraverseAll();

            // Assert
                // Checks that the unconected node exists in orderList
                Assert.Equal(10, orderList.Count);

        }

    }
}
