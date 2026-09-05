# Oppgave 1

## Linært søk
Tidskompleksiteten for linært søk er O(n) siden vi traverserer gjennom alle emlementer. 
Beste tilfellet er O(k), altså at vi finner objektet vi leter etter på første forsøk.

## Binært søk
Tidskompleksiteten for binært søk er O(log n) siden dataen vi jobber med blir halvert for hver runde.
Binær søk krever sorterte data siden vi bruker sammenlingingsfunksjonalitet i algoritmen.
Logisk oppdeling av arrayet krever derfor sorterte data, hvis ikke blir det bare rot. 

Kostnaden ved å sorterer først kan som regel forsvares om vi skal søke gjennom dataene flere ganger. 
Har vi et lite dataset kan man vurdere å droppe sortering først. 