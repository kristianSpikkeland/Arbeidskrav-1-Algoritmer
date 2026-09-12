using KodeAlgo;
using KodeAlgo.Helpers;
using KodeAlgo.Menu;
using KodeAlgo.Oppgave2;
using KodeAlgo.Oppgave4;
using KodeAlgo.Oppgave5;
using System.Collections;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {

        while (true)
        {
            Console.WriteLine("""
                1. Linear and Binary search
                2. Custom Queue
                3. QuickSort
                4. BFS
                5. DFS
                """);
            Console.WriteLine();

            int choice = Parser.Int("Your choice: ");
            Console.WriteLine();
            
            switch (choice)
            {
                case 1:
                    {
                        Oppgave1.RunAnalyzeLinearAndBinary();
                        break;
                    }

                case 2:
                    {
                        Console.WriteLine("Not implemented yet");
                        break;
                    }

                case 3:
                    {
                        MenuOppgave3.RunAnalyze();
                        break;
                    }

                case 4:
                    {
                        var BFS = new BFSGraph();
                        var menuOppgave4 = new MenuOppgave4(BFS);

                        menuOppgave4.BuildGraph();
                        menuOppgave4.BFSTraverseFromMajorstuen();
                        menuOppgave4.ShortestPathMajorstuenGrønland();
                        menuOppgave4.ShortestPathUllevålTøyen();


                        break;
                    }

                case 5:
                    {
                        var DFS = new DFSGraph();
                        var menuOppgave5 = new MenuOppgave5(DFS);

                        menuOppgave5.BuildGraph();
                        menuOppgave5.DFSTraverseRecursiveFromMajorstuen();
                        menuOppgave5.DFSTraverseIterativeFromMajorstuen();
                        var isPath = DFS.TryFindPath("Majorstuen", "Tøyn");
                        Console.WriteLine(isPath);


                        break;
                    }
            }

        }
        


       

   







    }

}



