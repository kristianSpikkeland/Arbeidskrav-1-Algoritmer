using KodeAlgo.Menu;
using KodeAlgo.Oppgave4;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace TestAlgo
{
    public class TestOppgave4
    {

        [Fact]
        public void TraverseBFS_Majorstuen_ShouldReturnSpesificOrder()
        {
            // Arrange
            var graph = new BFSGraph();
            var graphHelper = new MenuOppgave4(graph);

            var expectedOrder = new List<string>
            {"Majorstuen", "Nationaltheatret", "Blindern", "Stortinget", "Forskningsparken",
                "Jernbanetorget", "Ullevål stadion", "Grønland", "Tøyen"};

            graphHelper.BuildGraph();

            // Act
            var actualOrder = graph.TraverseBFS(graph._adj, "Majorstuen");


            // Assert
            Assert.Equal(expectedOrder, actualOrder);
        }

        
        [Fact]
        public void TraverseBFS_AddingIsolatedNode_ShouldContain1()
        {
            // Arrange
            var graph = new BFSGraph();
            var graphHelper = new MenuOppgave4(graph);

            graphHelper.BuildGraph();
            graph.AddNode("NewStation");

            // Act
            var order = graph.TraverseBFS(graph._adj, "NewStation");
          
            // Assert
            Assert.Single(order);
        }

        [Fact]
        // This test checks that adding a isolated node will not connect the isolated node to the rest of the graph
        public void TraversBFS_AddingIsolatedNode_TraversingExistingShouldStillContain9()
        {
            // Arrange
            var graph = new BFSGraph();
            var graphHelper = new MenuOppgave4(graph);

            graphHelper.BuildGraph();
            graph.AddNode("NewStation");

            // Act
            // Traversing the part of the graph co
            var order = graph.TraverseBFS(graph._adj, "Tøyen");


            // Assert
            // There are 10 nodes in total. 9 of them (including Tøyen) is connected
            Assert.Equal(9, order.Count());
        }

        [Fact]
        public void GetShortestPath_CheckMissingPathMajorstuenToNyNode_ShouldReturn0()
        {
            // Arrange
            var graph = new BFSGraph();
            var graphHelper = new MenuOppgave4(graph);

            graphHelper.BuildGraph();
            graph.AddNode("NewStation");

            // Act
            var shortestPath = graph.GetShortestPath("Majorstuen", "NewStation");

            // Assert
            Assert.Equal(0, shortestPath);
            
        }
    }
        
}
