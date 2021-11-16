# zombie_materix_dotNet_WPF
.Net WPF application that simulate a spread of infection "zombies" and measure how fast it will be.

# Introduction
This application is a simulation of an imaginary Zombie Vs Human
war or a pandemic tracker. The application at the beginning will ask users
for 2D matrix dimensions and the total number of Humans and Zombies.
Then the application will validate user input and position each Human and
Zombie in a unique and random location within the matrix.
The user have two options to proceed:
● Open War: Where the application will run the simulation till all
Humans become infected and converted to Zombies.
● Battle: Where the war will be based on a given number of iterations
given by the user, or it will stop earlier if no Humans left.
The application will create files that contain tracking information of the start
configuration, movement tracking, infected by and doing the infecting
records and a final result.

![1](https://user-images.githubusercontent.com/65984781/133001569-07a4f1e1-2f9c-4bc9-8a44-0c1e137e3410.png)

# Unique Location
The application make sure the each Human and Zombie positioned in
a unique position by tracking each location by three matrices of the same
size:
● Boolean Matrix: each location filled by Human or Zombie will be
marked as “True”, and the empty locations will be marked as “False”.
● Numeric Matrix: in this matix Human will equal to “1”, Zombie will
equal “2” and empty locations as “3”
● String Matrix: this matrix will hold unique names that will be viewed
on a dynamically created Grid of Textboxes.
A random number generator will pick number between 1 and 3, the
program will iterate according to matrix dimensions, if the random number
is 1 will fill the position with Human and mark “True” in the boolean matrix
and Human name in the string matrix and the same for Zombie but with
number 2. If a position marked as True in the same sized boolean matrix
the program will skip and iterate again. To make sure all Humans and
Zombies are positioned, the program will sum the number of Humans and
Zombies and compare it to Human and Zombie counters till it’s equal.
All Star configurations are saved to IDictionary.


# Tracking
The application depends on Object Oriented Programming, Where
both objects “Human & Zombie” had all the necessary properties to keep
track of all required actions.
1. Movement: to keep track of all coordinates the program uses Points
from .Net System.Drawing class, which facilitate the tracking of each
coordinate X and Y. Each object contains a property that saves a list
of Points and the required setters and getters.
2. Infected By: Human class contains infected by record, which will be
populated just before an infected Human becomes a Zombie.
45
3. Doing Infecting: Zombie class contains an IList property that can
hold all Human names that are infected by this Zombie object.
4. Locations: Both Human and Zombie classes had a currentPosition
Point property which holds the current position and keeps changing
according to object movement. Also, by calling a function that can
compare two Points for all Humans and Zombies, in case of a match
the program will add Human name to Zombie record and Zombie
name to Human record, create new Zombie, give it the current
coordinates of this Human and then delete human Object.

#### *Note: This desing was based on specific requirement for C# .Net midterm project.*

# References
- All class materials
- Dotnet-Bot. (n.d.). Point Constructor (System.Drawing). Retrieved June 29, 2021, from
https://docs.microsoft.com/en-us/dotnet/api/system.drawing.point.-ctor?view=net-5.0
- Dotnet-Bot. (n.d.). IDictionary Interface (System.Collections.Generic). Retrieved June
29, 2021, from
https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2?vie
w=net-5.0
- Dotnet-Bot. (n.d.). IList Interface (System.Collections). Retrieved June 29, 2021, from
https://docs.microsoft.com/en-us/dotnet/api/system.collections.ilist?view=net-5.0
