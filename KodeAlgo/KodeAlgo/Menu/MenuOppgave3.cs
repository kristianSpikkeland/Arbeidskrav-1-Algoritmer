using System;
using System.Collections.Generic;
using System.Text;

namespace KodeAlgo.Menu
{
    public static class MenuOppgave3
    {
        public static void RunAnalyze()
        {
            // Quicksort random numbers
            Console.WriteLine("Dataset 1: Random numbers");
            var nums = new int[] { 64, 34, 25, 12, 22, 11, 90 };
            Console.WriteLine("Inital list: " + string.Join(", ", nums));
            Oppgave3.QuickSort(nums, 0, nums.Length - 1);
            Console.WriteLine("Sorted list: " + string.Join(", ", nums));
            Console.WriteLine();

            // Quicksort random numbers
            Console.WriteLine("Dataset 2: Allready sorted numbers");
            var nums2 = new int[] { 1, 2, 3, 4, 5 };
            Console.WriteLine("Inital list: " + string.Join(", ", nums2));
            Oppgave3.QuickSort(nums2, 0, nums2.Length - 1);
            Console.WriteLine("Sorted list: " + string.Join(", ", nums2));
            Console.WriteLine();

            // Quicksort high to low
            Console.WriteLine("Dataset 3: High to low sorted");
            var nums3 = new int[] { 9, 8, 7, 6, 5 };
            Console.WriteLine("Inital list: " + string.Join(", ", nums3));
            Oppgave3.QuickSort(nums3, 0, nums3.Length - 1);
            Console.WriteLine("Sorted list: " + string.Join(", ", nums3));
            Console.WriteLine();

            // Quicksort duplicate numbers
            Console.WriteLine("Dataset 4: Duplicates");
            var nums4 = new int[] { 10, 10, 10, 100, 100, 5, 5, 2, 2 };
            Console.WriteLine("Inital list: " + string.Join(", ", nums4));
            Oppgave3.QuickSort(nums4, 0, nums4.Length - 1);
            Console.WriteLine("Sorted list: " + string.Join(", ", nums4));
            Console.WriteLine();

            // Quicksort only 1 number
            Console.WriteLine("Dataset 5: Only 1 element");
            var nums5 = new int[] { 90 };
            Console.WriteLine("Inital list: " + string.Join(", ", nums5));
            Oppgave3.QuickSort(nums5, 0, nums5.Length - 1);
            Console.WriteLine("Sorted list: " + string.Join(", ", nums5));
            Console.WriteLine();

            // Quicksort empty
            Console.WriteLine("Dataset 6: Empty");
            var nums6 = new int[] { };
            Console.WriteLine("Inital list: " + string.Join(", ", nums6));
            Oppgave3.QuickSort(nums6, 0, nums6.Length - 1);
            Console.WriteLine("Sorted list: " + string.Join(", ", nums6));
            Console.WriteLine();

        }
    }
}
