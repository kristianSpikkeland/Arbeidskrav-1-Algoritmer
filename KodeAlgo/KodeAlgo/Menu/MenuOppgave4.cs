using KodeAlgo.Oppgave4;
using System;
using System.Collections.Generic;
using System.Text;
using KodeAlgo.Oppgave4;

namespace KodeAlgo.Menu
{
    public class MenuOppgave4
    {
        private readonly BFSGraph _bfsGraph;
        
        public MenuOppgave4(BFSGraph bfsGraph)
        {
            _bfsGraph = bfsGraph;
        }

        public void BuildGraph()
        {           
            _bfsGraph.AddConnection("Majorstuen", "Nationaltheatret");
            _bfsGraph.AddConnection("Nationaltheatret", "Stortinget");
            _bfsGraph.AddConnection("Stortinget", "Jernbanetorget");
            _bfsGraph.AddConnection("Jernbanetorget", "Grønland");
            _bfsGraph.AddConnection("Grønland", "Tøyen");
            _bfsGraph.AddConnection("Majorstuen", "Blindern");
            _bfsGraph.AddConnection("Blindern", "Forskningsparken");
            _bfsGraph.AddConnection("Forskningsparken", "Ullevål stadion");     
        }


        // BFS
        public void BFSTraverseFromMajorstuen()
        {
            Console.WriteLine("Traversing graph with start node Majorstuen...");
            var order = _bfsGraph.TraverseBFS(_bfsGraph._adj, "Majorstuen");
            Console.WriteLine(string.Join(", ", order));
            Console.WriteLine();
        }

        // BFS
        public void ShortestPathMajorstuenGrønland()
        {
            Console.WriteLine("Calculating number of stops from Majorstuen to Grønland...");

            var shortestPath = _bfsGraph.GetShortestPath("Majorstuen", "Grønland");

            Console.WriteLine($"Shortest number of stops between Majorstuen and Grønland is {shortestPath} stops.");
            Console.WriteLine();
        }

        // BFS
        public void ShortestPathUllevålTøyen()
        {
            Console.WriteLine("Calculating number of stops from Ullevål stadion to Tøyen...");

            var shortestPath = _bfsGraph.GetShortestPath("Ullevål stadion", "Tøyen");

            Console.WriteLine($"Shortest number of stops between Ullevål and Tøyen is {shortestPath} stops.");
            Console.WriteLine();
        }


    }
}
