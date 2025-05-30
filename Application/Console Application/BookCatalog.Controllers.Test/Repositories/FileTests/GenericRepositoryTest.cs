using BookCatalog.DAL;
using BookCatalog.DAL.FileStorage.Filesystems;
using BookCatalog.DAL.FileStorage.Formatting;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Repositories.nonasync;
using BookCatalog.DAL.Storage.FileStorage.Filesystems;

namespace BookCatalog.Tests;


public class GenericRepositoryTest
{

    // default booklist for the tests
    private readonly IGeneralLogger _logger = new GeneralLogger();
    // arrange

    List<Book> _booksList = new()
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
    [Fact]
    public void BooksAreAddedInATestJson()
    {

    
        // Create a test.json file, pure for testing
        string fileName = $"TestGenericRepositoryAdding.json";
        ISerialize<Book> serializer = new JsonFormatter<Book>();
        IFileSystem<Book> fileSystem = new FileSystem<Book>(fileName, serializer);
        IRepository<Book> bookrepository = new FileRepository<Book>(fileSystem, _logger);
            

                // Act
                AddingBooksFromList(bookrepository, _booksList);


                // Assert
                for (int i = 0; i < _booksList.Count; i++)
                {
                    var booksOutput = bookrepository.GetById(_booksList[i].Id);
                    Assert.Equal(_booksList[i].Id, booksOutput.Id);
                    Assert.Equal(_booksList[i].Title, booksOutput.Title);
                    Assert.Equal(_booksList[i].Author, booksOutput.Author);
                    Assert.Equal(_booksList[i].PublicationYear, booksOutput.PublicationYear);
                    Assert.Equal(_booksList[i].Genre, booksOutput.Genre);
                    Assert.Equal(_booksList[i].ISBN, booksOutput.ISBN);
                    Assert.Equal(_booksList[i].PageCount, booksOutput.PageCount);
                    Assert.Equal(_booksList[i].Price, booksOutput.Price);
                    Assert.Equal(_booksList[i].IsAvailable, booksOutput.IsAvailable);
                }

                // clean file and delete books that have been added
                CleaningUpTest(bookrepository, _booksList);
            
        
    }

    [Fact]
    public void BooksAreDeletedInATestJson()
    {

        // arrange
        // Create a test.json file, pure for testing
        string fileName = $"TestGenericRepositoryDeleting.json";
        ISerialize<Book> serializer = new JsonFormatter<Book>();
        IFileSystem<Book> fileSystem = new FileSystem<Book>(fileName, serializer);
        IRepository<Book> bookrepository = new FileRepository<Book>(fileSystem, _logger);
        // Act
        AddingBooksFromList(bookrepository, _booksList);
                // Check if they are added
                for (int i = 0; i < _booksList.Count; i++)
                {
                    var booksOutput = bookrepository.GetById(_booksList[i].Id);
                    Assert.Equal(_booksList[i].Id, booksOutput.Id);
                }

                // Delete them

                CleaningUpTest(bookrepository, _booksList);


                // Assert
                // Check if they are gone

                for (int i = 0; i < _booksList.Count; i++)
                {

                    Assert.Equal(null, bookrepository.GetById(_booksList[i].Id));
                }

            
        
    }


    [Fact]
    public void BooksAreUpdatedInATestJson()
    {


        // arrange
        // Create a test.json file, pure for testing
        string fileName = $"TestGenericRepositoryUpdate.json";
        ISerialize<Book> serializer = new JsonFormatter<Book>();
        IFileSystem<Book> fileSystem = new FileSystem<Book>(fileName, serializer);
        IRepository<Book> bookrepository = new FileRepository<Book>(fileSystem, _logger);

        // Act
        AddingBooksFromList(bookrepository, _booksList);
                // Check if they are added
                for (int i = 0; i < _booksList.Count; i++)
                {
                    var booksOutput = bookrepository.GetById(_booksList[i].Id);
                    Assert.Equal(_booksList[i].Id, booksOutput.Id);
                }

                // Update the title and release year of the books
                foreach (var book in _booksList)
                {
                    book.Title = $"Edited title of \"{book.Title}\"";
                    book.PublicationYear = 1990;
                    bookrepository.Update(book);
                }

                // Assert
                // Check if they are updated
                for (int i = 0; i < _booksList.Count; i++)
                {
                    var booksOutput = bookrepository.GetById(_booksList[i].Id);
                    Assert.Equal(_booksList[i].Id, booksOutput.Id);
                    Assert.Equal(_booksList[i].Title, booksOutput.Title);
                    Assert.Equal(_booksList[0].PublicationYear, booksOutput.PublicationYear);
                }


                // clean file and delete books that have been added
                CleaningUpTest(bookrepository, _booksList);

            
        

    }

    [Fact]
    public void GettingABookAndThenUpdatingItTest()
    {

        // arrange
        // Create a test.json file, pure for testing
        string fileName = $"TestGenericRepositoryGetAndUpdate.json";
        ISerialize<Book> serializer = new JsonFormatter<Book>();
        IFileSystem<Book> fileSystem = new FileSystem<Book>(fileName, serializer);
        IRepository<Book> bookrepository = new FileRepository<Book>(fileSystem, _logger);
        // Act
        AddingBooksFromList(bookrepository, _booksList);
                // Check if they are added
                for (int i = 0; i < _booksList.Count; i++)
                {
                    var booksOutput = bookrepository.GetById(_booksList[i].Id);
                    Assert.Equal(_booksList[i].Id, booksOutput.Id);
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
                    Assert.Equal(selectedBook.Id, bookOutput.Id);
                    Assert.Equal(selectedBook.Title, bookOutput.Title);
                    Assert.Equal(selectedBook.PublicationYear, bookOutput.PublicationYear);
                }

                // Assert
                // clean file and delete books that have been added
               CleaningUpTest(bookrepository, _booksList);
            
        
    }

    private void AddingBooksFromList(IRepository<Book> bookrepository, List<Book> _booksList)
    {
        foreach (var book in _booksList)
        {
            bookrepository.Add(book);

        }

    }

    private void CleaningUpTest(IRepository<Book> bookrepository, List<Book> _booksList)
    {
        // clean file and delete books that have been added
        foreach (var book in _booksList)
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
