using System;
using System.Collections.Generic;
using System.Text;

namespace KodeAlgo
{
    public static class Oppgave3
    {
        public static void QuickSort(int[] array, int low, int high)
        {
            if (low <= high)
            {
                var partitionIndex = Partition(array, low, high);

                QuickSort(array, low, partitionIndex - 1);
                QuickSort(array, partitionIndex + 1, high); 
            }
                        
            
            
        }

        public static int Partition(int[] array, int low, int high)
        {
            int pivot = high;
            int i = low - 1;

            for (int j = low; j < pivot; j++)
            {
                if (array[j] < array[pivot])
                {
                    i++;
                    (array[j], array[i]) = (array[i], array[j]);

                }
            }

            (array[pivot], array[i + 1]) = (array[i + 1], array[pivot]);

            return i + 1;
         
        }


    }
}
