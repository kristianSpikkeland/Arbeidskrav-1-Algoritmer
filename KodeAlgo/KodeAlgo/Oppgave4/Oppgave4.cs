using System;
using System.Collections.Generic;



namespace KodeAlgo.Oppgave4
{
    public class BFSGraph
    {
        // Egenskaper

        public Dictionary<string, List<string>> _adj = new();

        public IEnumerable<string> Nodes => _adj.Keys;

        public void AddNode(string node)
        {
            _adj.TryAdd(node, new List<string>());
        }


        // Metoder

        // Metode for å binde to noder til hveranre i en urettet graf
        public void AddConnection(string nodeA, string nodeB)
        {
            // Legger til nodene som keys
            AddNode(nodeA);
            AddNode(nodeB);

            // Node B legges til node A sin naboliste
            _adj[nodeA].Add(nodeB);

            // Node A legges til node B sin naboliste
            _adj[nodeB].Add(nodeA);
        }


        // Metode for å returnere naboliste for en node
        public IReadOnlyList<string> Neighbors(string node)
        {
            if (_adj.TryGetValue(node, out var list))
            {
                return list;
            }
            // Om node ikke finnes returneres tom liste
            return new List<string> { };
        }



        public List<string> TraverseBFS(Dictionary<string, List<string>> graph, string start)
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


        public int GetShortestPath(string start, string goal)
        {
            if (start == goal)
            {
                throw new ArgumentException("Start and end node must be diffrent");
            }

            var visited = new HashSet<string>() { start };
            var parent = new Dictionary<string, string>();
            var queue = new Queue<string>();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                visited.Add(node);

                if (node == goal)
                {
                    // Når noden vi har tatt ut er lik målet vårt (node 2)
                    // Bygger vi en liste bakover med med metoden GoBack()
                    var pathList = GoBack(start, goal, parent);

                    // Vi returnerer antallet elementer i listen - 1 for å få nodeavstanden
                    return pathList.Count() - 1;
                }

                foreach (var neighbor in Neighbors(node))
                {
                    if (!visited.Contains(neighbor))
                    {
                        // Nodebarn legges som nøkkel, forelder som verdi
                        // En foreldre (node) kan ha flere barn (neighbors)
                        parent[neighbor] = node;

                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
            // Returnerer -1 om goal aldri blir funnet. Det er i så fall ingen sti mellom nodene.
            return -1;
        }

        public List<string> GoBack(string start, string goal, Dictionary<string, string> parent)
        {
            var order = new List<string>();

            // Omgjøring av navn for mer pedagogisk variabelnavn 
            string node = goal;

            // Vi rygger bakover
            while (node != start)
            {
                order.Add(node);

                // Flytter et steg bakover til forelderen til noden
                // Hvert barn har bare en forelder
                node = parent[node];
            }

            // Start legges til slutten av listen
            order.Add(start);

            // Listen reverseres for å få riktig rekkefølge
            order.Reverse();

            return order;
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
