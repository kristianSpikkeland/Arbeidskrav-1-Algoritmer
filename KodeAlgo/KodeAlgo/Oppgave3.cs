using System;
using System.Collections.Generic;
using System.Text;

namespace KodeAlgo
{
    public static class Oppgave3
    {
        public static void QuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                var sortedPivotIndex = Partition(array, low, high);

                QuickSort(array, low, sortedPivotIndex - 1);
                QuickSort(array, sortedPivotIndex + 1, high); 
            }
                        
            
            
        }

        public static int Partition(int[] array, int low, int high)
        {
            int pivotIndex = high;
            int i = low - 1;

            for (int j = low; j < pivotIndex; j++)
            {
                if (array[j] < array[pivotIndex])
                {
                    i++;
                    (array[j], array[i]) = (array[i], array[j]);

                }
            }

            (array[pivotIndex], array[i + 1]) = (array[i + 1], array[pivotIndex]);

            return i + 1;
         
        }


    }
}
