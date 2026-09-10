using System;
using System.Collections.Generic;



namespace KodeAlgo.Oppgave4
{
    public class BFSGraph
    {
        public Dictionary<string, List<string>> _adj = new();

        public IEnumerable<string> Nodes => _adj.Keys;

      

        public void AddNode(string node)
        {
            _adj.TryAdd(node, new List<string>());
        }

        public void AddConnection(string nodeA, string nodeB)
        {
            AddNode(nodeA);
            AddNode(nodeB);
            _adj[nodeA].Add(nodeB);
            _adj[nodeB].Add(nodeA);
        }

        public IReadOnlyList<string> Neighbors(string node)
        {
            if (_adj.TryGetValue(node, out var list))
            {
                Console.WriteLine(string.Join(", ", list));
                return list;
            }
            return Array.Empty<string>();
            
        }

        

        public static List<string> TraverseBFS(Dictionary<string, List<string>> graph, string start)
        {
            var visited = new HashSet<string>() { start };
            var order = new List<string>();

            var queue = new Queue<string>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                order.Add(node);

                if (graph.TryGetValue(node, out var neighbors))
                {
                    foreach (var neighbor in neighbors)
                    {
                        if (visited.Contains(neighbor))
                        {
                            continue;
                        }
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
                              
            }
            return order;
        }



        public void BuildTraverseGraph()
        {
            
            var graph = new BFSGraph();
            graph.AddConnection("Majorstuen", "Nationaltheatret");
            graph.AddConnection("Nationaltheatret", "Stortinget");
            graph.AddConnection("Stortinget", "Jernbanetorget");
            graph.AddConnection("Jernbanetorget", "Grønland");
            graph.AddConnection("Grønland", "Tøyen");
            graph.AddConnection("Majorstuen", "Blindern");
            graph.AddConnection("Blindern", "Forskningsparken");
            graph.AddConnection("Forskningsparken", "Ullevål stadion");

            // graph._adj og ikke kun adj da det kun vil medføre en tom liste
            var order = TraverseBFS(graph._adj, "Majorstuen");


            
            Console.WriteLine(string.Join(", ", order));
        }



        // Metode ikke i bruk. Brukt for å teste traversering.
        public Dictionary<string, List<string>> BuildTestGraph()
        {
            var testGraph = new Dictionary<string, List<string>>
            {
                ["Majorstuen"] = new() { "Nationaltheatret", "Blindern" },
                ["Nationaltheatret"] = new() { "Stortinget", "Majorstuen" },
                ["Stortinget"] = new() { "Jernbanetorget", "Nationaltheatret" },
                ["Jernbanetorget"] = new() { "Grønland", "Stortinget" },
                ["Grønland"] = new() { "Tøyen", "Jernbanetorget" },
                ["Majorstuen"] = new() { "Blindern", "Nationaltheatret" },
                ["Blindern"] = new() { "Forskningsparken", "Majorstuen" },
                ["Forskningsparken"] = new() { "Ullevål stadion", "Blindern" },
                ["Ullevål stadion"] = new() { "Forskningsparken" }
            };

            return testGraph;
        }


    }
    
    
}
