using BookCatalog.DataLayer;
using BookCatalog.DataLayer.FileStorage.Filesystems;
using BookCatalog.DataLayer.FileStorage.Formatting;
using BookCatalog.DataLayer.Repositories;
using System.Runtime.Serialization;

namespace BookCatalog.Tests;

[TestClass]
public class GenericRepositoryTest
{

    // default booklist for the tests

    [TestMethod]
    public void BooksAreAddedInATestJson()
    {

        // arrange

        List<Book> booksList = new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "L'amica geniale (Test)",
                Author = "Elena Ferrante",
                PublicationYear = 2011,
                Genre = "Roman",
                ISBN = "978-1609450786",
                PageCount = 336 ,
                Publisher = "Big Company" ,
                Price = 13.75M,
                IsAvailable = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Il Gattopardo (Test)",
                Author = "Giuseppe Tomasi di Lampedusa",
                PublicationYear = 1958,
                Genre = "Historische Roman",
                ISBN = "978-8845927361",
                PageCount = 440,
                Publisher = "Big Company" ,
                Price = 14.39M,
                IsAvailable = false
            }
        };
        // Create a test.json file, pure for testing
        string fileName = $"TestGenericRepositoryAdding.json";
        ISerialize<Book> serializer = new JsonFormatter<Book>();
        IFileSystem<Book> fileSystem = new FileSystem<Book>(fileName, serializer);
        IRepository<Book> bookrepository = new GenericRepository<Book>(fileSystem);
            

                // Act
                AddingBooksFromList(bookrepository, booksList);


                // Assert
                for (int i = 0; i < booksList.Count; i++)
                {
                    var booksOutput = bookrepository.GetById(booksList[i].Id);
                    Assert.AreEqual(booksList[i].Id, booksOutput.Id);
                    Assert.AreEqual(booksList[i].Title, booksOutput.Title);
                    Assert.AreEqual(booksList[i].Author, booksOutput.Author);
                    Assert.AreEqual(booksList[i].PublicationYear, booksOutput.PublicationYear);
                    Assert.AreEqual(booksList[i].Genre, booksOutput.Genre);
                    Assert.AreEqual(booksList[i].ISBN, booksOutput.ISBN);
                    Assert.AreEqual(booksList[i].PageCount, booksOutput.PageCount);
                    Assert.AreEqual(booksList[i].Price, booksOutput.Price);
                    Assert.AreEqual(booksList[i].IsAvailable, booksOutput.IsAvailable);
                }

                // clean file and delete books that have been added
                CleaningUpTest(bookrepository, booksList);
            
        
    }

    [TestMethod]
    public void BooksAreDeletedInATestJson()
    {

        // arrange
        List<Book> booksList = new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "L'amica geniale (Test)",
                Author = "Elena Ferrante",
                PublicationYear = 2011,
                Genre = "Roman",
                ISBN = "978-1609450786",
                PageCount = 336 ,
                Publisher = "Big Company" ,
                Price = 13.75M,
                IsAvailable = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Il Gattopardo (Test)",
                Author = "Giuseppe Tomasi di Lampedusa",
                PublicationYear = 1958,
                Genre = "Historische Roman",
                ISBN = "978-8845927361",
                PageCount = 440,
                Publisher = "Big Company" ,
                Price = 14.39M,
                IsAvailable = false
            }
        };
        // Create a test.json file, pure for testing
        string fileName = $"TestGenericRepositoryDeleting.json";
        ISerialize<Book> serializer = new JsonFormatter<Book>();
        IFileSystem<Book> fileSystem = new FileSystem<Book>(fileName, serializer);
        IRepository<Book> bookrepository = new GenericRepository<Book>(fileSystem);
        // Act
        AddingBooksFromList(bookrepository, booksList);
                // Check if they are added
                for (int i = 0; i < booksList.Count; i++)
                {
                    var booksOutput = bookrepository.GetById(booksList[i].Id);
                    Assert.AreEqual(booksList[i].Id, booksOutput.Id);
                }

                // Delete them

                CleaningUpTest(bookrepository, booksList);


                // Assert
                // Check if they are gone

                for (int i = 0; i < booksList.Count; i++)
                {

                    Assert.AreEqual(null, bookrepository.GetById(booksList[i].Id));
                }

            
        
    }


    [TestMethod]
    public void BooksAreUpdatedInATestJson()
    {


        // arrange
        List<Book> booksList = new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "L'amica geniale (Test)",
                Author = "Elena Ferrante",
                PublicationYear = 2011,
                Genre = "Roman",
                ISBN = "978-1609450786",
                PageCount = 336 ,
                Publisher = "Big Company" ,
                Price = 13.75M,
                IsAvailable = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Il Gattopardo (Test)",
                Author = "Giuseppe Tomasi di Lampedusa",
                PublicationYear = 1958,
                Genre = "Historische Roman",
                ISBN = "978-8845927361",
                PageCount = 440,
                Publisher = "Big Company" ,
                Price = 14.39M,
                IsAvailable = false
            }
        };
        // Create a test.json file, pure for testing
        string fileName = $"TestGenericRepositoryUpdate.json";
        ISerialize<Book> serializer = new JsonFormatter<Book>();
        IFileSystem<Book> fileSystem = new FileSystem<Book>(fileName, serializer);
        IRepository<Book> bookrepository = new GenericRepository<Book>(fileSystem);

        // Act
        AddingBooksFromList(bookrepository, booksList);
                // Check if they are added
                for (int i = 0; i < booksList.Count; i++)
                {
                    var booksOutput = bookrepository.GetById(booksList[i].Id);
                    Assert.AreEqual(booksList[i].Id, booksOutput.Id);
                }

                // Update the title and release year of the books
                foreach (var book in booksList)
                {
                    book.Title = $"Edited title of \"{book.Title}\"";
                    book.PublicationYear = 1990;
                    bookrepository.Update(book);
                }

                // Assert
                // Check if they are updated
                for (int i = 0; i < booksList.Count; i++)
                {
                    var booksOutput = bookrepository.GetById(booksList[i].Id);
                    Assert.AreEqual(booksList[i].Id, booksOutput.Id);
                    Assert.AreEqual(booksList[i].Title, booksOutput.Title);
                    Assert.AreEqual(booksList[0].PublicationYear, booksOutput.PublicationYear);
                }


                // clean file and delete books that have been added
                CleaningUpTest(bookrepository, booksList);

            
        

    }

    [TestMethod]
    public void GettingABookAndThenUpdatingItTest()
    {

        // arrange
        List<Book> booksList = new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "L'amica geniale (Test)",
                Author = "Elena Ferrante",
                PublicationYear = 2011,
                Genre = "Roman",
                ISBN = "978-1609450786",
                PageCount = 336 ,
                Publisher = "Big Company" ,
                Price = 13.75M,
                IsAvailable = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Il Gattopardo (Test)",
                Author = "Giuseppe Tomasi di Lampedusa",
                PublicationYear = 1958,
                Genre = "Historische Roman",
                ISBN = "978-8845927361",
                PageCount = 440,
                Publisher = "Big Company" ,
                Price = 14.39M,
                IsAvailable = false
            }
        };
        // Create a test.json file, pure for testing
        string fileName = $"TestGenericRepositoryGetAndUpdate.json";
        ISerialize<Book> serializer = new JsonFormatter<Book>();
        IFileSystem<Book> fileSystem = new FileSystem<Book>(fileName, serializer);
        IRepository<Book> bookrepository = new GenericRepository<Book>(fileSystem);
        // Act
        AddingBooksFromList(bookrepository, booksList);
                // Check if they are added
                for (int i = 0; i < booksList.Count; i++)
                {
                    var booksOutput = bookrepository.GetById(booksList[i].Id);
                    Assert.AreEqual(booksList[i].Id, booksOutput.Id);
                }

                // Update the title and release year of 1 book that is read out of the list

                var books = bookrepository.GetAll();
                var selectedBook = books.FirstOrDefault(item => item.PublicationYear > 2000);
                if (selectedBook != null)
                {
                    selectedBook.IsAvailable = true;
                    selectedBook.Title = $"One Book Edited: \"{selectedBook.Title}\"";
                    bookrepository.Update(selectedBook);

                    var bookOutput = bookrepository.GetById(selectedBook.Id);
                    Assert.AreEqual(selectedBook.Id, bookOutput.Id);
                    Assert.AreEqual(selectedBook.Title, bookOutput.Title);
                    Assert.AreEqual(selectedBook.PublicationYear, bookOutput.PublicationYear);
                }

                // Assert
                // clean file and delete books that have been added
               CleaningUpTest(bookrepository, booksList);
            
        
    }

    private void AddingBooksFromList(IRepository<Book> bookrepository, List<Book> booksList)
    {
        foreach (var book in booksList)
        {
            bookrepository.Add(book);

        }

    }

    private void CleaningUpTest(IRepository<Book> bookrepository, List<Book> booksList)
    {
        // clean file and delete books that have been added
        foreach (var book in booksList)
        {
            bookrepository.Delete(book.Id);        
        }
    }

    private void DeleteFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }

}
