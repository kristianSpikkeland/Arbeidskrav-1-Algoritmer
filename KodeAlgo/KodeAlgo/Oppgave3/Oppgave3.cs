using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace KodeAlgo
{
    public class Oppgave3
    {
        public int swapCounter = new();
        public int splitCounter = new();
        public int comparisonCounter = new();

        public void QuickSort(int[] array, int low, int high)
        {

            if (array.Length == 0)
            {
                throw new ArgumentException("Arrray is empty");
            }

            if (array.Length == 1)
            {
                throw new ArgumentException("Arrray only contains 1 element");
            }

            if (low < high)
            {
                splitCounter++;

                var sortedPivotIndex = Partition(array, low, high);

                QuickSort(array, low, sortedPivotIndex - 1);
                QuickSort(array, sortedPivotIndex + 1, high); 
            }           
        }

        public int Partition(int[] array, int low, int high)
        {
            int pivotIndex = high;
            int i = low - 1;

            for (int j = low; j < pivotIndex; j++)
            {
                comparisonCounter++;
                if (array[j] < array[pivotIndex])
                {
                    i++;
                    (array[j], array[i]) = (array[i], array[j]);
                    swapCounter++;

                }
            }

            (array[pivotIndex], array[i + 1]) = (array[i + 1], array[pivotIndex]);
            swapCounter++;

            return i + 1;
         
        }


    }
}
