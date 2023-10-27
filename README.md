# TravelPal
 WPF project made as a final assignment for the course Object oriented programming in C#



# Sammanfattning och analys
Projektet är ett litet program som låter användare lägga till och lagra resor som de planerar att göra. 

I mitt program kan användaren spara ner destinationsstad, destinationsland, antal resenärer, en packlista, vilken typ av resa det är samt specifika attribut beroende på vilken typ av resa det är, beskrivning av syftet av resan eller mötesdetaljer om det handlar om en arbetsresa och om resan har all-inclusive om det är en semesterresa.

Resorna är knytna till den användare som skapade dem, jag åstakom detta genom att ha 2 Listor. En större statisk lista i en statisk manager klass som håller koll på alla tillagda resor, huvudsakligen för att admin ska kunna se och ta bort resor från alla användare, och en lista knyten till varje användare. Nu i efterhand hade jag nog inte gjort på detta viset utan istället bara haft den större listan med alla resor och istället sparat ner användaren som skapade resan som en del av resan.
Detta är för att så som koden är strukturerad nu behöver jag alltid hitta och redigera innehållet i två listor som ligger på separata platser varje gång jag manipulerar dem på något vis.
