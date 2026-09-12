using KodeAlgo.Menu;
using KodeAlgo.Oppgave5;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestAlgo
{
    public class TestOppgave5
    {

        [Fact]
        public void RecursiveDFS_Majorstuen_ShouldReturnSpesificOrder()
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
        public void IterativeDFS_Majorstuen_ShouldReturnSpesificOrder()
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


        //[Fact]
        //public void TraverseBFS_AddingIsolatedNode_ShouldContain1()
        //{
        //    // Arrange
        //    var graph = new DFSGraph();
        //    var graphHelper = new MenuOppgave5(graph);

        //    graphHelper.BuildGraph();
        //    graph.AddNode("NewStation");

        //    // Act
        //    var order = graph.TraverseBFS(graph._adj, "NewStation");

        //    // Assert
        //    Assert.Single(order);
        //}

        //[Fact]
        //// This test checks that adding a isolated node will not connect the isolated node to the rest of the graph
        //public void TraversBFS_AddingIsolatedNode_TraversingExistingShouldStillContain9()
        //{
        //    // Arrange
        //    var graph = new DFSGraph();
        //    var graphHelper = new MenuOppgave5(graph);

        //    graphHelper.BuildGraph();
        //    graph.AddNode("NewStation");

        //    // Act
        //    // Traversing the part of the graph co
        //    var order = graph.TraverseBFS(graph._adj, "Tøyen");


        //    // Assert
        //    // There are 10 nodes in total. 9 of them (including Tøyen) is connected
        //    Assert.Equal(9, order.Count());
        //}
    }
}
