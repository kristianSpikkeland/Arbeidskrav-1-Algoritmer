# Oppgave 1

## Linært søk
Tidskompleksiteten for linært søk er O(n) siden vi traverserer gjennom alle emlementer. 
Beste tilfellet er O(1), altså at vi finner objektet vi leter etter på første forsøk.

### Analyse
Metoden jeg brukte var å legge inn en teller som legger til en for hver for løkke som kjøres.

Antall runder for array:  [8, 3, 11, 7, 2, 9, 5] ved søk etter 7 gav 4 runder. 
Teller man fra venstre mot høyre (og begynner på 1) får man 4.

Antall runder for array: [8, 3, 11, 7, 2, 9, 5] ved søk etter 42 gav 7 runder. 
42 er ikke i listen så hele løkken kjøres og det er totalt 7 tall så det gir mening.

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




## Binært søk
Tidskompleksiteten for binært søk er O(log n) siden dataen vi jobber med blir halvert for hver runde.
Binær søk krever sorterte data siden vi bruker sammenlingingsfunksjonalitet i algoritmen.
Logisk oppdeling av arrayet krever derfor sorterte data, hvis ikke blir det bare rot. 

Kostnaden ved å sorterer først kan som regel forsvares om vi skal søke gjennom dataene flere ganger. 
Har vi et lite dataset kan man vurdere å droppe sortering først.

### Analyse
Metoden jeg brukte var å legge til en teller etter while løkken som øker med en for hver runde og dermed 
holder rede på hvor mange ganger dataene blir delt. 

Antall runder for array: 2, 5, 7, 9, 11, 13, 17 ved søk etter 13 gav 2 runder.
Antall runder for array: 2, 5, 7, 9, 11, 13, 17 ved søk etter 4 gav 3 runder.
Hvis vi sammenligner binary search med linear search for verste tilfelle. 
Altså der det vi søker etter ikke finnes. Så ser vi at binary trenger 4 færrre runder.
Men det er viktig å huske at binary krever sorterte data.

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

## KI bruk
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



### Tester 
```C#
 public class TestOppgave1
    {
        [Fact]
        public void LinearSearch_Value7Exists_ShouldReturnIndex3()
        {
            // Arrange
            var nums = new int[] { 8, 3, 11, 7, 2, 9, 5 };

            // Act
            var result = Oppgave1.LinearSearch(nums, 7);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void LinearSearch_Value42NotExists_ShouldReturnNegativeIndex()
        {
            // Arrange
            var nums = new int[] { 8, 3, 11, 7, 2, 9, 5 };

            // Act
            var result = Oppgave1.LinearSearch(nums, 42);

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void LinearSearch_EmptyArray_ShouldThrowArgumentException()
        {
            // Arrange
            var nums = new int[] {};

            // Assert
            Assert.Throws<ArgumentException>(() => Oppgave1.LinearSearch(nums, 7));

        }

        [Fact]
        public void LinearSearch_OneElementInArray_ShouldThrowArgumentException()
        {
            // Arrange
            var nums = new int[] { 5 };

            // Assert
            Assert.Throws<ArgumentException>(() => Oppgave1.LinearSearch(nums, 5));

        }

        [Fact]
        public void LinearSearch_SameNumbers_ShouldReturnFirstIndex()
        {
            // Arrange
            var nums = new int[] { 2, 5, 4, 4, 4, 4, 3 };

            // Act
            var result = Oppgave1.LinearSearch(nums, 4);

            Assert.Equal(2, result);
        }

        [Fact]
        public void BinarySearch_Value13Exists_ShouldReturnIndex5()
        {
            // Arrange
            var nums = new int[] { 2, 5, 7, 9, 11, 13, 17 };

            // Act
            var result = Oppgave1.BinaryReaderSearch(nums, 13);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void BinarySearch_Value4NotExists_ShouldReturnNegativeIndex()
        {
            // Arrange
            var nums = new int[] { 2, 5, 7, 9, 11, 13, 17 };

            // Act
            var result = Oppgave1.BinaryReaderSearch(nums, 4);

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void BinarySearch_EmptyArray_ShouldThrowArgumentException()
        {
            // Arrange
            var nums = new int[] { };

            // Assert
            Assert.Throws<ArgumentException>(() => Oppgave1.BinaryReaderSearch(nums, 13));

        }

        [Fact]
        public void BinarySearch_OneElementInArray_ShouldThrowArgumentException()
        {
            // Arrange
            var nums = new int[] { 5 };

            // Assert
            Assert.Throws<ArgumentException>(() => Oppgave1.BinaryReaderSearch(nums, 5));

        }

        [Fact]
        public void BinarySearch_SameNumbers_ShouldReturnFirstIndex()
        {
            // Arrange
            var nums = new int[] { 2, 5, 4, 4, 4, 4, 3 };

            // Act
            var result = Oppgave1.BinaryReaderSearch(nums, 4);

            Assert.Equal(2, result);
        }
    }

```