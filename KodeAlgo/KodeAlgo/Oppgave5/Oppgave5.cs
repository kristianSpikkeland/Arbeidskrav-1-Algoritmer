using System;
using System.Collections.Generic;
using System.Text;

namespace KodeAlgo.Oppgave5
{
    public class DFSGraph
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


        // Metode som bruker rekrusjon for å traversere graf
        public List<string> TraverseRecursiveDFS(Dictionary<string, List<string>> graph, string start)
        {
            var visited = new HashSet<string>();
            var order = new List<string>();

           
            Dfs(start);
            return order;

            // Metoden kjører til alle noder er besøkt
            void Dfs(string node)
            {
                visited.Add(node);
                order.Add(node);

                
                foreach (var neighbor in graph[node].OrderBy(n => n))
                {
                    // Hvis neigbor ikke finnes i visited
                    if (!visited.Contains(neighbor))
                    {
                        // Blir neighbor ny node som prosesseres med Dfs metoden
                        Dfs(neighbor);
                    }
                }
            }
        }



        // Metode som bruker traverserer graf iterativt ved hjelp av stack
        public List<string> TraverseIterativeDFS(Dictionary<string, List<string>> graph, string start)
        {
            var stack = new Stack<string>();
            stack.Push(start);

            // Start er ikke satt til visited
            var visited = new HashSet<string>();
            var order = new List<string>();

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                // Forsøk å legge noden i visited
                // Hvis noden ligger der allerede, hopp over denne runden
                if (!visited.Add(node))
                {
                    continue;
                }
                order.Add(node);

                // Byttet til OrderByDescending for å få samme rekkefølge som i den rekrusive varianten
                foreach (var neighbor in graph[node].OrderByDescending(n => n))
                {
                    // Hvis naboen er besøkt, gå tilbake til foreach og forsøk med en ny
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    // Hvis naboen ikke er besøkt legges naboen til stacken
                    stack.Push(neighbor);
                }
            }
            return order;
        }




        // Mark on push variant
        public bool TryFindPath(string start, string goal)
        {
            // Start legges til visited med en gang (mark on push)
            var visited = new HashSet<string>() { start };
            var stack = new Stack<string>();

            // Legger startnode til stack
            stack.Push(start);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                // Hvis noden er den samme som målet vårt
                // Da finnes det en vei !
                if (node == goal)
                {
                    return true;
                }

                foreach (var neighbor in Neighbors(node))
                {
                    // Hvis neighbor ikke er lagt til visited fra før
                    // Da legger vi neghbor til visited oversikten
                    if (visited.Add(neighbor))
                    {
                        // Og legger neighbor til stacken
                        stack.Push(neighbor);
                    }
                }
            }
            // Hvis alle noder er besøkt, men ingen målnode er funnet
            // Ja, da finnes det ingen vei
            return false;


        }


        // Metodene TraversAll og DFS brukes for å traversere isolerte noder 
        public List<string> TraverseAll()
        {
            var order = new List<string>();
            var visited = new HashSet<string>();

            // Ytre løkke fanger isolerte
            foreach (string node in Nodes)     
            {
                if (!visited.Contains(node))
                {
                    DFS(node, visited, order);                    
                }
            }
            return order;
        }

        private void DFS(string node, HashSet<string> visited, List<string> order)
        {
            visited.Add(node);
            order.Add(node);

            foreach (string neighbor in _adj[node])
            {
                if (!visited.Contains(neighbor))
                    DFS(neighbor, visited, order);
            }
        }
    }
}
