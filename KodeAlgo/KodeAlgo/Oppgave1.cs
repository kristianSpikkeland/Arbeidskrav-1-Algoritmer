using System;
using System.Collections.Generic;
using System.Text;

namespace KodeAlgo
{
    public static class Oppgave1
    {
        //Oppgave 1

        public static int LinearSearch(int[] array, int target)
        {
            // Variabel for å telle runder
            var count = 0;

            for (int i = 0; i < array.Length; i++)
            {
                count += 1; 
                Console.WriteLine($"Runde {count}");

                if (target == array[i])
                {
                    return i;
                }             
            }
            return -1;
        }

        public static int BinaryReaderSearch(int[] array, int target)
        {
            // Variabel for å telle runder
            var count = 0;

            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {

                count += 1;
                Console.WriteLine($"Runde {count}");

                int mid = left + (right - left) / 2;

                if (array[mid] == target)
                {
                    return mid;
                }

                if (array[left] <= mid)
                {
                    left = mid + 1;
                }

                else
                {
                    right = mid - 1;
                }
            }
            return -1;
            
        }
    }
}
