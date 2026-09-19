using System;
using System.Collections.Generic;


namespace KodeAlgo.Oppgave2
{
    public class CustomQueue<T>
    {
        public int Size { get; set; }

        // Begynner med indeks negativ indeks siden listen begynnerer tom
        public int IndexFront { get; set; } = -1;


        public T[] _array;

        // Setter opp et array med størrelse som hentes fra Size parameteret
        public CustomQueue()
        {
            _array = new T[Size];
        }

        // Legger til muligheten for at man kan instansiere array med en valgt start kapasitet
        public CustomQueue(int capacity)
        {
            _array = new T[capacity];

            // Må sette Size egenskapen til valgt capacity. Hvis ikke funker ikke dobling av arrayet
            Size = capacity;
        }

        public void Swap(int i, int j)
        {
            var temp = _array[i];
            _array[i] = _array[j];
            _array[j] = temp;
        }

        public void DoubleArray()
        {
            // Hvis arrayet er fult
            if (_array.Length == IndexFront + 1)
            {
                // Doble størrelsen på Size egenskapen
                Size *= 2;

                // Og lag en ny liste med dobbel størrelse
                T[] new_array = new T[Size];

                // Skriv over de verdiene fra den gamle listen til starten av den nye
                for (int i = 0; i < _array.Length; i++)
                {
                    new_array[i] = _array[i];
                }
                // Det gamle arrayet settes til det nye
                _array = new_array;
            }
        }

        public void HalfArray()
        {
            // Hvis alle elementer vil få plass etter halvering
            // Aldri halver om array kun inneholder et element
            if (Size / 2 > IndexFront && Size > 1)
            {
                Size = Size / 2;
                T[] new_array = new T[Size];

                // Viktig! Ikke IndexFront + 1 siden metoden blir kalt av Dequeue som har tilbakestilt indekxFront med en 
                for (int i = 0; i <= IndexFront; i++)
                {
                    new_array[i] = _array[i];
                }
                _array = new_array;

            }
        }

        public void Enqueue(T val)
        {
            // Hvis det ikke er skrevet noen verdier til arrayet er indeks 0 ledig
            if (IndexFront < 0)
            {
                // Setter inn verdien på indeks 0
                _array[0] = val;

                // Oppdaterer til riktig indeksposisjon for IndexFront etter innsettingen
                IndexFront += 1;
            }

            else
            {
                DoubleArray();
                int i = IndexFront;

                // Flytter alle verdier oppover en indeks
                // Dette for å lage plass til elementet vi setter inn på indeks 0
                while (i >= 0)
                {
                    Swap(i, i + 1);
                    i -= 1;
                }
                // Setter inn elementet på indeks 0
                _array[0] = val;

                // Oppdaterer til riktig indeksposisjon for IndexFront etter innsettingen
                IndexFront += 1;
            }
        }

        public T Dequeue()
        {
            // Hvis indeksen er negativ er det ingen elementer og vi returnerer
            if (IndexFront < 0)
            {
                throw (new ArgumentException("Dequeueing an empty queue is not allowed"));
            }

            var temp = _array[IndexFront];

            // Tilbakestiller siste element
            Array.Clear(_array, IndexFront, 1);

            // Flytter indekspekeren for siste element med verdi tilbake
            IndexFront -= 1;

            // Halverer arrayet ved behov
            HalfArray();

            return temp;

        }

        public void Peek()
        {
            // Hvis det finnes elementer
            if (IndexFront > -1)
            {
                var firstInQueue = _array[IndexFront];
                Console.WriteLine($"{firstInQueue} is first in queue");
            }
        }


        public void Print()
        {
            Console.WriteLine("Elements: " + string.Join(", ", _array));
            Console.WriteLine();
        }

        // Takk til : https://www.youtube.com/watch?v=HABx2vP-Ee0

    }
}