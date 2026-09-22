using System;
using System.Collections.Generic;
using System.Text;

namespace KodeAlgo.Oppgave1
{
    public static class Oppgave1
    {
        //Oppgave 1

        public static int LinearSearch(int[] array, int target)
        {
            // Legger til validering
            if (array.Length < 1)
            {
                throw new ArgumentException("Array can not be empty");
            }

            if (array.Length == 1)
            {
                throw new ArgumentException("Array should contain more than one element");
            }

            // Variabel for å telle runder
            var count = 0;

            for (int i = 0; i < array.Length; i++)
            {
                count += 1; 
                Console.WriteLine($" --- Count {count}");

                if (target == array[i])
                {
                    return i;
                }             
            }
            return -1;
        }

        public static int BinaryReaderSearch(int[] array, int target)
        {
            // Legger til validering
            if (array.Length < 1)
            {
                throw new ArgumentException("Array can not be empty");
            }

            if (array.Length == 1)
            {
                throw new ArgumentException("Array should contain more than one element");
            }

            // Variabel for å telle runder
            var count = 0;

            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {

                count += 1;
                Console.WriteLine($" --- Count {count}");

                int mid = left + (right - left) / 2;

                // Returner om midten er lik det vi søker etter og det ikke finnes lavere indekser for det vi søker etter
                if (array[mid] == target && (mid == 0 || array[mid -1] != target))
                {
                    return mid;
                }

                // Hvis midten er lik det vi søker etter.
                // Men det derimot finnes lavere indekser med verdien vi søker etter
                if (array[mid] == target && array[mid - 1] == target)
                {
                    int i = mid;
                    while (i > 0 && array[i - 1] == target) i--;
                    return i;
                }
                
                
                // Hvis verdien av midtindeksen er mindre enn målet vårt
                if (array[mid] <= target)
                {
                    // Da vet vi at målet ligger høyere og vi flytter pekeren left oppover
                    left = mid + 1;
                }

                else
                {
                    // Hvis ikke vet vi at målet ligger lavere og vi flytter pekeren right lavere.
                    right = mid - 1;
                }
            }
            return -1;
            
        }

        public static void RunAnalyzeLinearAndBinary()
        {
            var nums1 = new int[] { 8, 3, 11, 7, 2, 9, 5 };
            var nums2 = new int[] { 2, 5, 7, 9, 11, 13, 17 };

            Console.WriteLine(" - Running linear analyze on [8, 3, 11, 7, 2, 9, 5]");
            Console.WriteLine(" -- Searching 7");
            LinearSearch(nums1, 7);
            Console.WriteLine();

            Console.WriteLine(" -- Searching 42");
            LinearSearch(nums1, 42);
            Console.WriteLine();

            Console.WriteLine();

            Console.WriteLine(" - Running binary analyze on [2, 5, 7, 9, 11, 13, 17]");
            Console.WriteLine(" -- Searching 13");
            BinaryReaderSearch(nums2, 13);
            Console.WriteLine();

            Console.WriteLine(" -- Searching 4");
            BinaryReaderSearch(nums2, 4);
            Console.WriteLine();
        }
    }
}
