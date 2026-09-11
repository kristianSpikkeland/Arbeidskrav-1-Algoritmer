using System;
using System.Collections.Generic;
using System.Text;

namespace KodeAlgo.Helpers
{
    public static class Parser
    {
        public static int Int(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();

                if (int.TryParse(input, out int num))
                {
                    return num;
                }
                Console.WriteLine("Invalid input");
            }
           

            
        }
    }
}
