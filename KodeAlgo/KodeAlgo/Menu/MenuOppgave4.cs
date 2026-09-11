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

        public void TraverseFromMajorstuen()
        {
            var order = _bfsGraph.TraverseBFS(_bfsGraph._adj, "Majorstuen");
            Console.WriteLine(string.Join(", ", order));
        }

        public void ShortestPathMajorstuenGrønland()
        {
            var shortestPath = _bfsGraph.GetShortestPath("Majorstuen", "Grønland");

            Console.WriteLine(string.Join(", ", shortestPath));
            Console.WriteLine($"Shortest number of stops is {shortestPath.Count - 1}");
            Console.WriteLine();
        }

        
    }
}
