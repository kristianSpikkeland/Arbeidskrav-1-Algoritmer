
# Oppgave 5
## Forklaringer
### Tidskompleksiteten O(V + E)
Tidskompleksiteten O(V + E) gjelder også for traversing av nabo liste (adjacency list) med DFS. 
V står for vertices(nodene) mens E står for edges (kanter).

Vi får O(V + E) siden:
* O(V): hver node er besøkt en gang
* O(E): hver nabo ligger på en kant og blir besøkt en gang
* Kombinert gir det O(V+E)

### Forskjell rekursiv og iterativ DFS
Med iterativ DFS har man mer kontroll enn med den rekrusive varianten. Den rekrusive varianten kan lage stack overflow om grafen er svært dyp.
Det vil si at stacken blir full og programmet krasjer.
Den iterative varianten ligner mer på BFS enn den rekrusive. 
Om man ikke tar et aktivt valg om å snu besøktrekkefølge, vil de to variantene returnere forskjellig rekkefølge men allikevell være gyldige. 

### Sammenligning BFS, DFS
BFS bruker kø mens DFS bruker stack.
Man kan si at BFS bruker FIFO (first in first out) mens DFS bruker LIFO(last in last out).
DFS står for depth first search som vil si at DFS går i dybden først. BFS står for breadth first search som vil si at BFS går i bredden først.
Iterativ DFS ligner veldig på BFS og ved bytte fra BFS kø til stack har vi faktisk DFS.
BFS passer for å finne korteste vei i uvektet graf.
DFS passer for å utforske sykler og for å finne vei fra start til mål i labyrinter. 
BFS sin minnebruk sliter ved brede grafer mens DFS sliter ved dype grafer. Begge har O(n) plasskompleksitet. 

### Basissteg og rekrusivt steg (rekrusiv variant)

I koden finnes ingen return for basisteget. Men når alle naboer er besøkt vil ikke DFS(neighbor) kjøres mer og vi når slutten av koden.

Foreach løkken leter gjennom naboene og sjekker om naboen er besøkt. Det finnes ikke et uendelig antall nabooer.
OrderBy sørger for at naboene blir alfabetisk sortert.


```C#
foreach (var neighbor in neighbors.OrderBy(n => n))
{
    if (!visited.Contains(neighbor))
    {
        Dfs(neighbor);
    }
}                      
```

Det rekrusive steget er:

```C#
Dfs(neighbor);
```
som fører til at følgende:

```C#

visited.Add(node);
order.Add(node);

                
foreach (var neighbor in neighbors.OrderBy(n => n))
{
    if (!visited.Contains(neighbor))
    {
        Dfs(neighbor);
    }
}  
```
Neighbor har blitt til node og rekrusjonen kjøres helt til det ikke er flere naboer i grafen å besøke.


# KI bruk
Hadde glemt litt bort traversering med DFS. Gikk derfor tilbake å så på et puggeark jeg har jobbet med i sommer.
Legger ved puggearket med nav bfs-dfs puggeark som vedlegg.
Etter litt prompting frem og tilbake viste det seg at den iterative varianten i puggearket returnerte feil DFS rekkefølge.
Puggearket mitt hadde for mye fokus rundt å gjøre BFS og DFS mest mulig lik, 
men klarte ikke å ta hensyn til at DFS er mer sårbar for når besøkt settes.
Problemet var at naboene ble markert som visited for tidlig.

En del spørsmål frem og tilbake med KI rundt DFS og om det finnes en vei.
Syntes koden jeg tidligere hadde fått av KI (se vedlegg) virket litt tungvint. 

Stusset på linjen: 
```if (!visited.Add(current)) continue```

Den virket litt ueffektiv.

Etter litt frem og tilbake med spørsmål fikk jeg denne koden av Claude som har fjernet denne duplikatsjekken:

```C#
public bool HasPath(string start, string goal)
{
    var stack   = new Stack<string>();
    var visited = new HashSet<string> { start };   // ◄ A: start markert med én gang
    stack.Push(start);

    while (stack.Count > 0)
    {
        var current = stack.Pop();
        if (current.Equals(goal)) return true;
                                                   // ◄ B: ingen guard her
        foreach (var neighbor in Neighbors(current))
            if (visited.Add(neighbor))             // ◄ C: markerer OG sjekker
                stack.Push(neighbor);
    }
    return false;
}
```

Claude kaller den for mark on push.
Men får forklart av Claude at Mark on push bare egner som om rekkefølgen ikke spiller noen rolle.

Videre får man forklart at for riktig sortering og syklusdeteksjon må vi ha den guarden/dulikatsjekken.
Da må vi ha Mark on pop som Claude kaller det og begynne med tom visited liste.


Bilde under viser mark on pop:

```C#
public List<string> IterativeDFS(string start, Dictionary<string, List<string>> graph)
{
    var stack = new Stack<string>();
    stack.Push(start);

    var visited = new HashSet<string>();
    var order = new List<string>();

    while (stack.Count > 0)
    {
        var node = stack.Pop();
        if (!visited.Add(node)) continue;
        order.Add(node);

        foreach (var neighbor in graph[node].OrderBy(n => n))
            if (!visited.Contains(neighbor))
                stack.Push(neighbor);
    }
    return order;
}
```

Prompt: Hva menes med håndtering av isolerte noder:

Svar: 
En isolert node har ingen kanter, eller den er ikke koblet til resten av grafen.
Problemet er at hvis du bare starter søket fra én node, finner du aldri de andre. En node uten forbindelse blir stående igjen.
Løsningen er å kjøre løkka over alle noder, ikke bare starte ett sted:

```
foreach (Node node in alleNoder)
{
    if (!visited.Contains(node))
    {
        DFS(node, visited);
    }
}
```

Prompt:
Gi meg et fullverdig pedagogisk eksempel på traversering med DFS som håndterer isolerte noder på en god måte.
Hvordan bør besøktliste bygges i dette tilfelle. Svar kort.

Svar (tok kun med det som var relevant):

```
    public void TraverseAll()
    {
        var visited = new HashSet<string>();

        foreach (string node in adj.Keys)      // ytre løkke = fanger isolerte
        {
            if (!visited.Contains(node))
            {
                Console.WriteLine($"-- ny komponent fra {node} --");
                DFS(node, visited);
            }
        }
    }

    private void DFS(string node, HashSet<string> visited)
    {
        visited.Add(node);                     // merk MED ÉN GANG
        Console.WriteLine(node);

        foreach (string neighbor in adj[node])
        {
            if (!visited.Contains(neighbor))
                DFS(neighbor, visited);
        }
    }
```

Hvordan besøktlisten bør bygges:

Den skal ligge utenfor DFS og sendes inn, slik at den overlever på tvers av komponentene. Lages den inni DFS, nullstilles den ved hvert kall.
Noden merkes først i metoden, før løkka, ikke etter. Merker du for sent, rekker en syklus å kalle tilbake før noden er registrert.
Bruk HashSet, ikke List. Oppslag blir O(1) i stedet for O(n).
Og nodene må ligge i adj selv om de er isolerte. AddNode sørger for det, ellers ser ikke den ytre løkka dem.



Litt spørsmål rundt om det er vanlig at det er vanlig om rekrusiv og iterativ DFS returnerer forskjellig rekkefølge.
Fikk til svar at om jeg ville ha stigende (ABC) så skal jeg har OrderBy i den rekrusive og OrderByDecending i den iterative.

## Kode

```C#
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
        if (!Nodes.Contains(start))
        {
            throw new ArgumentException("Cannot traverse node that does not exist");
        }

        var visited = new HashSet<string>();
        var order = new List<string>();

           
        Dfs(start);
        return order;

        // Metoden kjører til alle noder er besøkt
        void Dfs(string node)
        {
            visited.Add(node);
            order.Add(node);

            // Bruker TryGetValue for å unngå exception om starnode ikke finnes i graf
            if (graph.TryGetValue(node, out var neighbors))
            {
                foreach (var neighbor in neighbors.OrderBy(n => n))
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
    }



    // Metode som bruker traverserer graf iterativt ved hjelp av stack
    public List<string> TraverseIterativeDFS(Dictionary<string, List<string>> graph, string start)
    {
        if (!Nodes.Contains(start))
        {
            throw new ArgumentException("Cannot traverse node that does not exist");
        }

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

            // Bruker TryGetValue for å unngå exception om starnode ikke finnes i graf
            if (graph.TryGetValue(node, out var neighbors))
            {
                // Byttet til OrderByDescending for å få samme rekkefølge som i den rekrusive varianten
                foreach (var neighbor in neighbors.OrderByDescending(n => n))
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
```

## Tester
```C#
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

```