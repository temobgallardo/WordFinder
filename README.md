<h1 align="center">WordFinder</h1>

<p align="center">
  WordFinder is an algorithm to find words from an array or list of string into a matrix/database (array of string) using a Trie data structure
  which makes the overall search Complexity O(R*S) where S is the length of the string in each row and R the number of rows in the database. 
  Regarding Space Complexity is O(W) where W is the sum of all the words added to the Trie.
</p>

<h1 align="center">How</h1>

This was made into a Worker as a sample of usage. The hearth of the algorith is in the WordFinder.Find method which makes use of the Trie Data Structure service
to help count the words. There are two mock services IRequestMatrixService and IRequiestWOrdsToSearchService that help serve the matrix database 
and the words respectively.

This uses parallelism to process each row in the database matrix (array of string) on different cores to speedup the searching process.

It also contains a Unit Tests project which can be executed using the command below to know how the program works.

![WordFinderDemo-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/7432077a-12e9-4b10-a9c9-68c4d5033fcb)


### Prerequisite
- .NET 8
  
Push the project and open a command line in the directory of the project then execute: 

### Usage
- dotnet run --project .\WordFinder\WordFinder.csproj  

### Run Test  
- dotnet test

### Author
Artemio Banos | Software Engineer who loves developing on PC, Android and iOS with .NET & Java.
-  [Github: temobgallardo](https://github.com/temobgallardo/) 
-  [Linkedin: abanosga](https://www.linkedin.com/in/abanosga/)

<br/>  
