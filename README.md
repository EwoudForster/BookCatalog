<center><img width=200 src="images/LogoCegeka.png"></center>

# Bookcatalog Console .Net Application

In this repository you find the application I made for my internship at Cegeka

## What does the application do
The application is a used as a management system for books. You can add, update, delete, search for certain books as well as viewing statistics of the specific book.

## Setup
### How to setup the application
To run this application, first you download the folder "PublishedApplication" or "PublishedApplication.zip". You now unzipping the folder.

## Usage
### Start the application
you go to the "PublishedApplication" Folder and start the "BookCatalog.exe" file. The Application will start.
![Application Exe file](images/ExeFile.png "Application file")

### Options
You now get this screen opened in a terminal. Given 5 options, you make your decision, you type in the word given in the console. You could also use the number.
![Application Options screen](images/Options.png "Application Options screen")

### Search
When typing 1, or search, you get the search screen displayed. now you are able to choose between searching for a book with the Title, Publisher, Author and genre, or using the ID
![Application screen searching ID](images/SearchID.png)
![Application screen searching name or other information](images/SearchName.png.png)

### Adding
When typing 2, or adding, you get again a clean console window for adding a new book. in this case you need to fill in every field of the book, using valid data, as you can see in the picture there is validation on the fields. If you use wrong data you will need to fill in that specific field again, or you can type exit when you want to cancel the operation.
![Application Adding screen](images/Adding.png "Application Adding screen")

### Updating
When typing 3, or update, you can search for any book in the book catalog, using again the ID, Title, Publisher, Author and genre. You can keep looking till you have found the 1 specific book you are looking for. You need to choose only 1 specific book. You are not able to choose more then 1. If you found 1 book, you need to confirm that you want to modify this specific book. 
![Application Update Confirmation screen](images/UpdateConfirmation.png "Application Update Confirmation screen")

After doing this you need to fill in every field again. At the end you get the updated book returned and now if you look for that specific book it has been changed, but the ID is still the same.
![Application Result Update Screen](images/ResultUpdatedItem.png "Application Result Update Screen")

### Delete
When typing 4, or delete, you can delete an item by searching for the ID, Title, Publisher, Author and genre. When you found the book you want to delete a confirmation message comes again. 
![Application Result Delete Screen](images/Delete.png "Application Result Delete Screen")

### Statistics
When typing 5, or stats, you get a view on the statistics about all the books in the Book catalog
![Application Result Statistics Screen](image.png "Application Result Statistics Screen")

### Grouping
When typing 6, or year, you group all the books by the year of publication
the same for typing 7, or genre, only here everything gets grouped by the genre.

![Application options grouping](images/Groupingoptions.png "Application options grouping")

here you see the result, the groupkey is mentioned at the top, following with more information about the books. 
![Application Result grouping](images/Groupingresult.png "Application Result grouping")

## About the code of the application
### Modify
If you want to make modifications you need to go into the application folder, open the IDE you want to use, best Visual Studio. When you make your modifications, and you are ready, you can publish the application again. Now you again go to the PublishedApplication folder and you are done.

### Tests
In the solution there is a project dedicated to tests, in here the specific functionalities are tested, there are 21 tests and they are all successful
There are tests for following functionalities:
- Genericrepository:
    - Searching 
    - Adding
    - Updating
    - Deleting
- LINQ:
    - OrderByTitle
    - OrderBy Price
    - OrderByPublisher
    - OrderByPublicationYear
    - OrderByPageCount
    - Search By Title
    - Search By Test
    - Search By Genre
    - Search By Author
- Logger
    - String output
    - Exception output
- Generic File System, you can choose the file format (CSV, JSON, CSV by reflection):
    - Saving
    - Reading

![Successful tests](images/Tests.png)

## Principles Used
### Single Responsibility Principle (SRP)
I put every responsibility in a separate file, so 1 file only has 1 specific responsibility.
Also was I making a RepositoryFactory, which I will definitely fix in the future. 

#### Dependency injection
I use dependency injection on every place a class relies on another instance of a class, I don't let the class create the instance itself, that's why I pass it in the constructor as a parameter in a lot of places to minimize the responsibility of a class.

###	Interface Segregation Principle
Every interface that is implemented is implemented fully. There are no implemented interfaces with unused

### Reflection
I've used reflection for trying to make the RepositoryFactory. As well as showing the properties of a specific book. Reflection is also used for getting the file path where the JSON file needs to be saved in.

### Generic Repository structure
The repository is generic, you can use this on any project without changing a thing.

## File Structure
I tried to make a clear and easy to navigate solution structure. Here you can find the structure.
```
BookCatalog
|
├───BookCatalog (Solution)
│   ├───BookCatalog
│   │   ├───Program.cs
│   │   └───Services
│   │           ├───BookService.cs
│   │           ├───ConsoleHelper.cs
│   │           └───LINQ
│   │   
│   ├───BookCatalog.DataLayer
│       ├───Formatting
│       │   │───CsvFormatter.cs
│       │   ├───CsvFormatterReflection.cs
│       │   │───ISerialize.cs
│       │   └───JsonFormatter.cs
│       │
│       ├───Models
│       │   ├───Book.cs
│       │   │───EntityBase.cs
│       │   │───IEntity.cs
│       │   └───Logging
│       │       ├───ILogger.cs
│       │       └───Logger.cs
│       │ 
│       ├───Repositories
│       │   │───GenericRepository.cs
│       │   ├───IReadRepository.cs
│       │   │───IRepository.cs
│       │   └───IWriteRepository.cs
│       │  
│       └───RepositoryFactory
│           │───LoadContext.cs
│           └───RepositoryFactory.cs
│   
└───BookCatalog.Tests
    ├───Logging
    │   └───LoggingTest.cs
    │
    ├───LINQ
    │   ├───Formatting.cs 
    │   └───Search.cs
    │
    └───Repositories
        ├───FileSystemTest.cs 
        ├───Formatting.cs 
        └───GenericRepository.cs   

```

Author: Ewoud Forster