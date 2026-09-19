# Oppgave 3
## Generelt
Var vant med å skrive Quick sort metoden vist under timen med Ali.  
Slet noen timer med å forsøke å skrive den om til å passe med parameterne som er gitt i oppgavetekst. Men fikk det aldri helt til. 
Tok en titt på nettsiden https://www.geeksforgeeks.org/dsa/quick-sort-algorithm/ for å forstå bedre hva som var meningen.
Lærte meg denne koden utenat og skrev en tilnærmet lik versjon som løsningsforslag. Brukte en litt ann metode for bytte.
Samt at man droppet å skille ut swap som en egen funksjon.

### Forklaringer
QuickSort handler om at vi ønsker å legge dem tallene som er lavere enn pivotverdien til venstre for pivotindeksen 
mens dem som er høyere legger vil til høyre for pivotverdien. 
Pivotindeksen kan være hvilket som helst tall, men jeg valgte å sette pivotindeksen til det siste tallet. 
Vi setter opp to pekere f.eks med navn i og j. i starter bak j i min kode.
Videre settes det igang en for løkke med j som starter på parameteret low og kjører til pivotindeksen nås, men den tas ikke med. 
Om verdien for j er lavere enn verdien for pivot, økes pekeren i med en og i verdien og j verdien bytter plass. 
Dette sørger for at de tallene som er lavere enn pivot havner til venstre.
Men verdien for pivot ligger feilplassert etter at løkken er kjørt.
i +1 verdien er siste tall som er høyere en pivotverdien. Derfor bytter disse verdiene plass.
Pivotverdien er nå det eneste tallet som er garantert riktig plassert etter at basissteget er kjørt.
Pivotindeksen har jeg hentet ut i metoden Partition. 

For at resten av tallene skal bli sortert kalles QuickSort rekrusivt. En metode som kaller seg selv kan kjøres uendelig.
Derfor setter vi opp at hvis low er mindre enn high så kjøres rekrusjonen. Det er dette som er basissteget. 
Dermed får en utgang i det low møter high etter hvert som arrayet blir mer og mer delt. 
Vi henter ut indeksen ved:

```C#
var sortedPivotIndex = Partition(array, low, high);
```

QuickSort kalles så rekrusivt for de tallene som er lavere enn pivotverdien. 
Deretter kaller QuickSort seg selv for de tallene som er høyere enn pivotverdien.
Legg merke til at pivot verdien ikke er med i de rekrusive kallene.  
Dette er greit å få til siden Partion metoden returnerer pivot indeksen.

### Tidskompleksitet, plasskompleksitet
Forventet tidskompleksitet for beste tilfelle samt gjennomsnitt er O (n log n). Verste tilfelle er 0 (n^2).
QuickSort kan ikke garantere O (n log n) slik som merge sort kan. 

I forhold til plasskompleksitet er verste tilfelle (n)
I beste tilfelle er plasskompleksiteten O(log n)

Kilde https://www.geeksforgeeks.org/dsa/time-and-space-complexity-analysis-of-quick-sort/

### Fordeler, ulemper
Jeg har forstått det slik at merge sort ble oppfunnet før QuickSort. Da er det litt rart at QuickSort har blitt så populær
med dårligere garantert tidskompleksitet. Grunnet skal være at QuickSort har bedre plasskompleksitet. 

Betydningen av inputdata har mye å si for QuickSort. Den liker ikke at ting er tilnærmet ferdig sortert og valg av pivot har mye å si.
Vi vil ha en pivot som deler arrayet i to tilnærmet like store deler. 

En ulempe med QuickSort er at den blir regnet som ustabil. Med det menes det at to like verdier kan få byttet sin plass med hverandre.
Etter litt drøfting med KI kommer det fram at hvis verdiene er helt like gjør det ingenting. Men om to elementer med lik sorteringsnøkkel 
bytter plass, spiller det faktisk er rolle om elementene bører på mer informasjon enn nøkkelen (kilde Claude).

QuickSort er vistnok bra for store data set, men dårlig for små. Kilde: https://www.geeksforgeeks.org/dsa/quick-sort-algorithm/

## KI Bruk

Prompt:
```C#
void QuickSort(int[] array, int low, int high)
```
Hva er grunnen til int low og int high som parameter? Funksjonen funker utmerket uten. Kan det være for å avgrense søkeområdet? Svar veldig kort.


Svar:
Ja, nettopp. Quicksort er rekursiv og må sortere delområder av samme array. Uten low/high måtte du kopiert ut nye delarrays for hvert kall — dyrt og unødvendig. 
Parameterne lar deg si «sorter kun fra indeks low til high» i den originale arrayen.

Kaller du den kun én gang på hele arrayen, ser de overflødige ut — men de rekursive kallene inni funksjonen trenger dem.

Ellers en del drodling med KI for å skjønne QuickSort koden jeg fant på geeksforgeeks sine sider skikkelig.
Basissteg var et nytt begrep for min del. Fikk det forklart på en grei måte av KI.

## Kode

```C#
    public class Oppgave3
    {
        public int swapCounter = new();
        public int splitCounter = new();

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
```

### Tester

```C#
   public class TestOppgave3
    {
        [Fact]
        public void QuickSort_IfSizeIs0_ShouldThrowArgumentException()
        {
            // Arrange
            var nums = new int[] { };
            var sut = new Oppgave3();

            // Assert
            Assert.Throws<ArgumentException>(() => sut.QuickSort(nums, 0, nums.Length - 1));
        }


        [Fact]
        public void QuickSort_IfSizeIs1_ShouldThrowArgumentException()
        {
            // Arrange
            var nums = new int[] { 1 };
            var sut = new Oppgave3();

            // Assert
            Assert.Throws<ArgumentException>(() => sut.QuickSort(nums, 0, nums.Length - 1));
        }

        [Fact]
        public void QuickSort_ShouldSort_Ascending()
        {
            // Arrange
            var nums = new int[] { 6, 2, 9, 0, 3 };
            var sut = new Oppgave3();

            var expectedOrder = new int[] { 0, 2, 3, 6, 9 }; 

            // Act
            sut.QuickSort(nums, 0, nums.Length - 1);

            // Assert
            Assert.Equal(expectedOrder, nums);
        }
    }
```
