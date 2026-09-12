using KodeAlgo.Oppgave5;
using System;
using System.Collections.Generic;


namespace KodeAlgo.Menu
{
    public class MenuOppgave5
    {
        private readonly DFSGraph _dfsGraph;

        public MenuOppgave5(DFSGraph dfsGraph)
        {
            _dfsGraph = dfsGraph;
        }

        public void BuildGraph()
        {
            _dfsGraph.AddConnection("Majorstuen", "Nationaltheatret");
            _dfsGraph.AddConnection("Nationaltheatret", "Stortinget");
            _dfsGraph.AddConnection("Stortinget", "Jernbanetorget");
            _dfsGraph.AddConnection("Jernbanetorget", "Grønland");
            _dfsGraph.AddConnection("Grønland", "Tøyen");
            _dfsGraph.AddConnection("Majorstuen", "Blindern");
            _dfsGraph.AddConnection("Blindern", "Forskningsparken");
            _dfsGraph.AddConnection("Forskningsparken", "Ullevål stadion");
        }


        // DFS Recursive 
        public void DFSTraverseRecursiveFromMajorstuen()
        {
            Console.WriteLine("Recursivly traversing graph with start node Majorstuen...");
            var order = _dfsGraph.TraverseRecursiveDFS(_dfsGraph._adj, "Majorstuen");
            Console.WriteLine(string.Join(", ", order));
            Console.WriteLine();
        }

        // DFS Iterative
        public void DFSTraverseIterativeFromMajorstuen()
        {
            Console.WriteLine("Iterative traversing graph with start node Majorstuen...");
            var order = _dfsGraph.TraverseIterativeDFS(_dfsGraph._adj, "Majorstuen");
            Console.WriteLine(string.Join(", ", order));
            Console.WriteLine();
        }
    }
}
