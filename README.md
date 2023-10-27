# TravelPal
 WPF project made as a final assignment for the course Object oriented programming in C#



# Sammanfattning och analys
Projektet är ett litet program som låter användare lägga till och lagra resor som de planerar att göra. 

I mitt program kan användaren spara ner destinationsort, destinationsland, antal resenärer, en packlista, vilken typ av resa det är samt specifika attribut beroende på vilken typ av resa det är, beskrivning av syftet av resan eller mötesdetaljer om det handlar om en arbetsresa och om resan har all-inclusive om det är en semesterresa.

Resorna är knutna till den användare som skapade dem, jag åstadkom detta genom att ha 2 Listor. En större statisk lista i en statisk managersklass som håller koll på alla tillagda resor, huvudsakligen för att admin ska kunna se och ta bort resor från alla användare, och en lista knuten till varje användare. Nu i efterhand hade jag nog inte gjort på detta viset utan istället bara haft den större listan med alla resor och istället sparat ner användaren som skapade resan som en del av resan.
Detta är för att så som koden är strukturerad nu behöver jag alltid hitta och redigera innehållet i två listor som ligger på separata platser varje gång jag manipulerar dem på något vis. Programmet har också alltid tillgång till den inloggade användaren när resor skapas eller redigeras så det hade varit väldigt lätt att skicka med den informationen till konstruktören.

Som tidigare skrivet använder jag en managersklass för resor, detta gör jag även för min lista av användare. Detta är så jag inte behöver skicka med instanserna av listorna mellan varje fönster då detta hade komplicerat koden och kodandet samt ökat risken för buggar. Jag kunde göra detta för att programmet aldrig behöver separata listor av användare eller mer än en lista med alla resor. 
Bägge typer av listor är också listor av interfacet  rese typerna och typen av användare(admin och user) ärver av. Detta minimerar logiken som krävs för att manipulera och leta i listorna.
I efterhand hade jag omfaktoriserat en del av min kod som ligger i code-behinden på details och add fönstret då de delar mycket logik och metoder, denna logik hade nog varit bättre att lägga i någon av manager klasserna eller i en annan separat klass för att bättre följa DRY.

Andra funktioner som jag hade kunnat tänka mig lägga till är, några sortering och filtreringsmetoder, både för användaren och resorna som den har och för att admin ska kunna filtrera mellan alla resorna tillagda av alla de olika användarna.
