using KodeAlgo.Helpers;
using KodeAlgo.Oppgave2;
using System;
using System.Collections.Generic;
using System.Text;

namespace KodeAlgo.Menu
{
    public static class MenuOppgave2
    {
        public static void QueueMenu()
        {
            var queue = new CustomQueue<int>(5);

            while (true)
            {
                Console.WriteLine("""
                1. Add integer to queue (Enqueue)
                2. Remove (Dequeue)
                3. Show queue
                4. Show the first element in queue
                5. Enrich with 5 elements
                6. Show queue Size property and IndexFront properties
                100. Return to main menu
                """);
                Console.WriteLine();

                int choice = Parser.Int("Your choice: ");
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        {
                            int enqueueChoice = Parser.Int("Integer to add: ");
                            Console.WriteLine();

                            queue.Enqueue(enqueueChoice);
                            break;
                        }

                    case 2:
                        {
                            try
                            {
                                var removed = queue.Dequeue();
                                Console.WriteLine($"{removed} was removed");
                                Console.WriteLine();
                            }

                            catch (ArgumentException)
                            {
                                Console.WriteLine("Removing an element from an empty queue is not allowed");
                                Console.WriteLine();
                            }
                            break;
                        }

                    case 3:
                        {
                            queue.Print(); 
                            break;
                        }

                    case 4:
                        {
                            try
                            {
                                var first = queue.Peek();
                                Console.WriteLine($"{first} is first in queue");
                            }

                            catch (ArgumentException exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                            Console.WriteLine();
                            break;
                        }

                    case 5:
                        {
                            queue.Enqueue(3);
                            queue.Enqueue(7);
                            queue.Enqueue(4);
                            queue.Enqueue(6);
                            queue.Enqueue(2);
                            break;
                        }

                    case 6:
                        {
                            if (queue.IndexFront < 0)
                            {
                                Console.WriteLine("The queue is empty");
                                Console.WriteLine();
                                return;
                            }

                            Console.WriteLine($"Size: {queue.Size}");
                            Console.WriteLine($"IndexFront index: {queue.IndexFront}");
                            Console.WriteLine($"IndexFront value: {queue._array[queue.IndexFront]}");
                            Console.WriteLine();
                            break;
                        }


                    case 100:
                        {
                            return;
                        }
                }
            }
        }
    }
}
