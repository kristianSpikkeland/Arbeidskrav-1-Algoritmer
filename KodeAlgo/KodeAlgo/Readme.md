# Oppgave 1

## Linært søk
Tidskompleksiteten for linært søk er O(n) siden vi traverserer gjennom alle emlementer. 
Beste tilfellet er O(k), altså at vi finner objektet vi leter etter på første forsøk.

## Kode
```C#
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
```

### Analyse
Metoden jeg brukte var å legge inn en teller som legger til en for hver for løkke som kjøres.

Antall runder for array:  [8, 3, 11, 7, 2, 9, 5] ved søk etter 7 gav 4 runder. 
Teller man fra venstre mot høyre (og begynner på 1) får man 4.

Antall runder for array: [8, 3, 11, 7, 2, 9, 5] ved søk etter 42 gav 7 runder. 
42 er ikke i listen så hele løkken kjøres og det er totalt 7 tall så det gir mening.

### Tester 
Se eget testprosjekt.


## Binært søk
Tidskompleksiteten for binært søk er O(log n) siden dataen vi jobber med blir halvert for hver runde.
Binær søk krever sorterte data siden vi bruker sammenlingingsfunksjonalitet i algoritmen.
Logisk oppdeling av arrayet krever derfor sorterte data, hvis ikke blir det bare rot. 

Kostnaden ved å sorterer først kan som regel forsvares om vi skal søke gjennom dataene flere ganger. 
Har vi et lite dataset kan man vurdere å droppe sortering først.

### Kode

```C#
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
                if (array[mid] == target && array[mid -1] != target)
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
```

### Analyse
Metoden jeg brukte var å legge til en teller etter while løkken som øker med en for hver runde og dermed 
holder rede på hvor mange ganger dataene blir delt. 

Antall runder for array: 2, 5, 7, 9, 11, 13, 17 ved søk etter 13 gav 2 runder.
Antall runder for array: 2, 5, 7, 9, 11, 13, 17 ved søk etter 4 gav 3 runder.
Hvis vi sammenligner binary search med linear search for verste tilfelle. 
Altså der det vi søker etter ikke finnes. Så ser vi at binary trenger 4 færrre runder.
Men det er viktig å huske at binary krever sorterte data.


### Tester 
Se eget testprosjekt.

# Oppgave 2

Slet en del med å komme igang med oppgaven. Valgte å se på C# implementasjonen av Queue. 
Fant dermed et fint oppsettet for å få satt opp arrayet.
Neste utfordring skjedde når man skulle forsøke å appende til det tomme arrayet. 

# Oppgave 3
## Generelt
Var vant med å skrive Quick sort metoden vist under timen med Ali.  
Slet noen timer med å forsøke å skrive den om til å passe med parameterne som er gitt i oppgavetekst. Men fikk det aldri helt til. 
Tok en titt på nettsiden https://www.geeksforgeeks.org/dsa/quick-sort-algorithm/ for å forstå bedre hva som var meningen.
Lærte meg denne koden utenat og skrev en tilnærmet lik versjon som løsningsforslag. Brukte en litt ann metode for bytte.
Samt at man droppet å skille ut swap som en egen funksjon.

### Forklaringer
Basissteget handler om at vi ønsker å legge dem tallene som er lavere enn pivotverdien til venstre for pivotindeksen 
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
Derfor setter vi opp at hvis low er mindre enn high så kjøres rekrusjonen. 
Dermed får en utgang i det low møter high etter hvert som arrayet blir mer og mer delt. 
Vi henter ut indeksen og får kjørt sortering ved:

```C#
var partitionIndex = Partition(array, low, high);
```

QuickSort kalles da rekrusivt for de tallene som er lavere enn pivotverdien. 
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
bytter plass, spiller det fakrisk er rolle om elementene bører på mer informasjon enn nøkkelen (kilde Claude).

QuickSort er vistnok bra for store data set, men dårlig for små. Kilde: https://www.geeksforgeeks.org/dsa/quick-sort-algorithm/

# Oppgave 4
Jeg begynte først med å tegne opp grafen for å ha en ryddig oversikt:
![Bilde graf](./Oppgave4/BildeGraf.png)

Bygget så en ferdig naboliste og traverserte denne. 

# KI bruk
## Oppgave 1
Prompt:
Har laget en metode som blant annet kaster ArgumentException om en liste er tom.
Hvordan lager man en test som fanger opp at metoden skal kaste ArgumentException. 
Bruker x unit. Svar veldig kort.

Svar:

```C#
[Fact]
public void MinMetode_TomListe_KasterArgumentException()
{
    var liste = new List<int>();

    Assert.Throws<ArgumentException>(() => MinKlasse.MinMetode(liste));
}
```
Assert.Throws<T> krever eksakt type. 
Skal subklasser (f.eks. ArgumentNullException) også godtas, bruk Assert.ThrowsAny<ArgumentException>. 
For async: await Assert.ThrowsAsync<ArgumentException>(() => ...)

Prompt: Hvorfor vil ikke i-- kjøre i denne koden. Svar veldig kort:


Prompt:
Skal rygge tilbake og hente ut første indeks av et tall i en liste. Hva er feil i koden. Svar veldig kort:  

```C#
// Hvis midten er lik det vi søker etter.
// Men det derimot finnes lavere indekser med verdien vi søker etter
if (array[mid] == target && array[mid - 1] == target)
{
    // Da "rygger" vi til venstre for å se hvor langt de like tallene går
    for (int i = mid; i > 0; i--)
    {
        // Hvis vi får treff for at tallene er ulike vet vi at vi må legge til en for å få første treff
        if (array[i] != array[mid])
        {
            return i + 1;
        }

    }
}
```
Svar:

Tre feil:

1. array[mid - 1] leses uten å sjekke mid > 0 → out of bounds når mid == 0.
2. Løkka stopper på i > 0, så indeks 0 testes aldri. Hvis alle like helt til start, faller du ut uten å returnere noe.
3. array[mid] som sammenligning: fungerer, men bruk target for klarhet.

Enkleste fiks:

```C#
int i = mid;
while (i > 0 && array[i - 1] == target) i--;
return i;
```

## Oppgave 2


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



### Bakgrunnsinformasjon
Fikk også svar på hva som menes med lite API i oppgave2 da man ikke helt skjønte hva som var meningen. 
Svaret var basicly å sette metodene som public.

Var usikker på om Queue bare var for arrays, men fikk til svar at den er genric og også skal funke for linked lists.


## Oppgave 3

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

## Oppgave 4
Prompt: 
Holder på med BFS for en graf med et endepunkt uten naboer. Får KeyNotFoundException for endepunktet. 
Hvordan kan man unngå det. Svar veldig kort.

Svar: 
Bruk TryGetValue i stedet for indekseringsoperatoren:
```C#
if (graph.TryGetValue(node, out var naboer))
{
    foreach (var n in naboer) { /* ... */ }
}
```
Alternativt: sørg for at hver node får en tom liste når du bygger grafen, så finnes nøkkelen alltid. 
Det er som regel den reneste løsningen.

Slet veldig med med: 
"Implementer void AddConnection(string stationA, string stationB)". 
Etter å ha holdt på noen timer med å prøve å forstå hvordan det skulle angripes, fant jeg ut at jeg hadde noen kunnskapshull
knyttet til hvordan man lager toveisforbindelse mellom to noder samt hvordan man finner korteste vei mellom to noder. 
Videre så jeg videre på oppgave 5 der man skal undersøke om det finnes en rute mellom to noder og oppdage syklus i urettede graf.
Hadde ingen erfaring med dette. Kunne kun traversering av grafer.
Har tidligere god erfaring å la Claude lage pedagogisk opplegg til meg så jeg promptet:

Prompt:
Lag et pedagogisk opplegg som lærer meg følgende om grafer i c#:

For BFS
* Hvordan man kan legge til toveisforbindelse mellom to noder med metode:  void AddConnection(string nodeA, string nodeB).
* Finner korteste vei mellom to noder.

For DFS
* Undersøke om det finnes en rute mellom to noder
* Oppdage syklus i urettede graf

Lag pedagogsike forlaringer og kode som er puggbar slik at alle konseptene sitter.
Kan traversering fra før av. Lag PDF.

Brukte deretter KI en del for å utdype forkllaringene i PDFen.

Svar:
Se vedlagt PDF

Litt spørsmål rundt markdown og innliming av bilde. Jeg hadde mellomrom i bildenavnet som skapte litt trøbbel. 
Fikk også repetert hvordan man lager kulepunkt med * 

## Generelt 
Litt spørsmål rundt plassering av .gitignore fil.
Videre spurte man om navngivning av tester.
Spørsmål om å hente frem youtube videoer som bakgrunnsinformasjon før man begynte på oppgaven.
Føler man lærer lite om man spinner i ring for lenge.


## Andre kilder
### Komme igang
https://www.youtube.com/watch?v=VXSqNCso3fA
https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository

### Oppgave 2
https://www.youtube.com/watch?v=aNTDJ9bnRU4

### Oppgave 3
https://www.youtube.com/watch?v=MZaf_9IZCrc
https://www.geeksforgeeks.org/dsa/time-and-space-complexity-analysis-of-quick-sort/
https://www.geeksforgeeks.org/dsa/quick-sort-algorithm/