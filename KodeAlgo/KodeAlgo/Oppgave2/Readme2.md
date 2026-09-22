# Oppgave 2

## Oppstart
Slet en del med å komme igang med oppgaven. Valgte å se på C# implementasjonen av Queue. Fant dermed et fint oppsettet for å få satt opp arrayet.
Jeg ville bruke array istedenfor liste som intern lagring siden det virket mest givende og lærerikt.
Løsningen min er i veldig stor grad omskrevet fra Python kode presentert i denne Youtube videoen: https://www.youtube.com/watch?v=HABx2vP-Ee0

## Prinsipp og intern representasjon

Køen er implementert slik at man kan velge hvilken generisk type som skal lagres. Eks. int, string, decimal, double.

Det er lagt til en konstruktør der man kan legge til en start kapasitet. 

```C#
    public CustomQueue(int capacity)
    {
        _array = new T[capacity];
    }
```
I programmet har jeg valgt å sette startkapasiteten i arrayet til 5 elementer.
På denne måten slipper jeg IndexOutOfRangeException når bruker forsøker å legge første element til tomt array.

Om alle disse 5 plassene blir fylt opp med Enque metoden, blir arrayet fult og må utvides om man Enquer enda en gang.
DoubleArray metoden min lager et nytt array med dobbel størrelse og de gamle verdiene skriver over til det nye arrayet
Til slutt settes det gamle arrayet til å være likt det nye: 

```C#
    public void DoubleArray()
    {
        if (_array.Length == IndexFront + 1)
        {
            Size *= 2;

            T[] new_array = new T[Size];

            for (int i = 0; i < _array.Length; i++)
            {
                new_array[i] = _array[i];
            }

            _array = new_array;
        }
    }
```

For å spare lagringsplass er det lagt til en halveringsmetode. Denne blir kalt i Dequeue metoden
Halveringen kjøres kun om halvparten av størrelsen til arrayet er større enn indeksen til det elementet som er fremst i køen: 

```C#
    public void HalfArray()
    {
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
```

Enqueue metoden setter inn elementer bakerst i køen på indeks 0. IndexFront er en egenskap som holder rede på plasseringen til siste element i arrayet.
Hvis det ikke er skrevet noen verdier til arrayet legges elementet til indeks 0. Hvis arrayet derimot har verdier må hele arrayet flyttes oppover.
Dette er siden nye elementer legges bakerst i arrayet på indeks null. En hjelpemetode kalt swap hjelper til med flyttingen.
IndexFront legges til en etter innsettingen for at vi skal ha riktig referanse til siste element i arrayet. 


```C#


    public void Enqueue(T val)
    {
        if (IndexFront < 0)
        {
            _array[0] = val;
            IndexFront += 1;
        }

        else
        {
            DoubleArray();
            int i = IndexFront;

            while (i >= 0)
            {
                Swap(i, i + 1);
                i -= 1;
            }

            _array[0] = val;
            IndexFront += 1;
        }
    }
```

I Dequeue metoden slipper vi forskyvningen vi måtte gjøre med Enqueue siden det tas fra fremst i køen (IndeksFront).
Etter at et element er fjernet tilbakestilles IndeksFront med en indeks for å kompensere for fjerningen. 
Det er lagt til en ArgumentException for å fange opp om bruker forsøker å fjerne fra tom kø.
Denne exceptionen blir fanget opp i klassen MenuOppgave2 for å sikre god programflyt ved at programmet ikke krasjer. 


```C#
    if (IndexFront < 0)
    {
        throw (new ArgumentException("Dequeueing an empty queue is not allowed"));
    }

    var temp = _array[IndexFront];

    Array.Clear(_array, IndexFront, 1);

    IndexFront -= 1;

    HalfArray();

    return temp;
```

Det er lagt til rikelig med kodekommentarer i koden. Dette er mest for min egen del mtp. repetisjon og kodeforståelse.

## Bruksområde
Bruksområde for den egenlagde køen anses for å være begrenset. I fremtidige prosjekt vil jeg bruke Microsoft sin da den er mer utprøvd.
Bruksområdet for køer innenfor programmering på generelt grunnlag er stort. I enkelte tilfeller har man kanakje har belasting på deler av et system.
Ved hjelp av kø kan man da i en del tilfeller spre ut belastingen jevnere. 

## Tids - og plasskompleksitet
### Enqueue
Plasskompleksiteten for Enqueue er i verste tilfelle 0(n) som forekommer når DoubleArray blir kalt. 
Koden viser hvorfor: 
```C#
    for (int i = 0; i < _array.Length; i++)
    {
        new_array[i] = _array[i];
    }
```
Plasskompleksiteten for Enqueue om arrayet ikke må dobles er 0(1) siden kun legger til en verdi i arrayet.  


Tidskompleksiteten for Enqueue er i beste fall O(1). Det er dersom vi setter inn et element på indeks 0. 
For da blir ingen løkker kjørt og vi jobber bare med variabler og enkel sammenligning.

Om DoubleArray blir kalt må vi kjøre for løkke for å skrive verdier til den nye listen så tidskmpleksiteten blir O(n).

Om DoubleArray ikke kjøres samt at vi ikke setter inn på indeks 0 må verdiene allikevell flyttes oppover med while løkke.
Tidskompleksiteten blir da 0(n). Der n er antall bytter oppover.
```C#
    while (i >= 0)
    {
        Swap(i, i + 1);
        i -= 1;
    }
```

### Dequeue
Plasskompleksiteten for Dequeue er i verste tilfelle 0(n) som forekommer når arrayet halveres. Da lages en ny liste med halvparten av Size som størrelse.
Beste tilfelle for plasskompleksitet forekommer når array ikke må halveres og vi får O(1).

Beste tidskompleksitet for Dequeue er O(1) som intreffer når ingen halvering kjøres og vi bare nullstiller fra fremst i køen.
Verste tilfelle er ved halvering da får vi O(n). Om vi sammenligner like situasjoner kan tidskompleksiteten ikke bli bedre enn plasskompleksiteten.


### Peek
Enkel sammenligning og jobbing med variabel. Tid - og plasskompleksitet O(1).


## KI bruk


Prompt: 
Sliter litt med at jeg skal appende til et tomt array i C#. 
Forklar veldig kort hvordan dette kan gjøres. 
Lages det en kopi av arrayet med append så man må tenke annerledes?

Svar: 
Kort svar: ja, arrays i C# har fast størrelse. Du kan aldri legge til et element — du lager et nytt, større array og kopierer over. .
Append() fra LINQ ser ut som appending, men den returnerer bare en lat IEnumerable<int>; originalen er urørt, og du må kalle .ToArray() for å materialisere den. 
Det er en kopi hver gang.

```C#
int[] arr = Array.Empty<int>();
arr = arr.Append(5).ToArray();        // LINQ — ny allokering hver gang
Array.Resize(ref arr, arr.Length + 1); // kopierer også, bare mer eksplisitt
arr[^1] = 7;
```
Begge er O(n) per element, så i en løkke blir det O(n²). Bruk List<int> i stedet — den vokser amortisert O(1) ved å doble en intern buffer:

```C#
var list = new List<int>();
list.Add(5);
list.Add(7);
int[] result = list.ToArray();  // konverter til slutt hvis du må ha array
```

Tommelfingerregel: kjenner du størrelsen på forhånd, bruk array. Vokser den underveis, bruk List<T>.


## Kode
```C#
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

        public T Peek()
        {
            // Hvis det finnes elementer
            if (IndexFront > -1)
            {
                T firstInQueue = _array[IndexFront];
                return firstInQueue;
            }

            throw new ArgumentException("The queue is empty");
        }


        public void Print()
        {
            Console.WriteLine("Elements: " + string.Join(", ", _array));
            Console.WriteLine();
        }
    }
```

## Tester
```C#
public class TestOppgave2
{

    [Fact]
    public void Dequeue_RemovingFromEmptyArray_ShouldThrowException()
    {
        // Arrange
        var queue = new CustomQueue<int>();

        // Assert
        Assert.Throws<ArgumentException>(() => queue.Dequeue());
    }


    [Fact]
    public void Enqueue_AddingElementToFullArray_ShouldExpandArray()
    {
        // Arrange
            // Creates a queue with 5 slots in array
            var queue = new CustomQueue<int>(5);

        // Act
            // Adding 5 elements
            queue.Enqueue(3);
            queue.Enqueue(7);
            queue.Enqueue(4);
            queue.Enqueue(6);
            queue.Enqueue(2);

        // Adding element number 6 should double array  
        queue.Enqueue(9);

        // Assert
        Assert.Equal(10, queue.Size);
    }

    [Fact]
    public void Dequeue_RemovingElement_ShouldRemoveEmptySpace()
    {
        // Arrange
            // Creates a queue with 5 slots in array
            var queue = new CustomQueue<int>(5);

        // Act
            // Adding 5 elements
            queue.Enqueue(3);
            queue.Enqueue(7);
            queue.Enqueue(4);
            queue.Enqueue(6);
            queue.Enqueue(2);

        // Adding element number 6 sets queue Size to 10  
        queue.Enqueue(9);

 
        queue.Dequeue();

        // Assert
        Assert.Equal(5, queue.Size);
    }

    [Fact]
    public void Dequeue_RemovingElement_ShouldUseFIFO()
    {
        // Arrange
        var queue = new CustomQueue<int>(3);

        // Act
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        var fifo = queue.Dequeue();

        // Assert
        Assert.Equal(1, fifo);
    }

    [Fact]
    public void Peek_ShouldShowFirstInQueue()
    {
        // Arrange
        var queue = new CustomQueue<int>(3);

        // Act
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        var fifo = queue.Peek();

        // Assert
        Assert.Equal(1, fifo);
    }

}
```