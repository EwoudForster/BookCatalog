using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Filesystems;
using BookCatalog.DataLayer.Formatting;
using BookCatalog.DataLayer.Repositories;

namespace BookCatalog.Tests;

[TestClass]
public class GenericRepositoryTest
{
   
    // default booklist for the tests

    private List<Book> _booksList = new List<Book>
        {
            new Book()
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
            new Book()
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

    [TestMethod]
    public void BooksAreAddedInATestJson()
    {
        // arrange
        // Create a test.json file, pure for testing
        string _fileName = "Tests-data/TestGenericRepositoryAdding.json";
        IRepository<Book> bookrepository = new GenericRepository<Book>(new FileSystem<Book>(_fileName, new JsonFormatter<Book>()));

        // Act
        AddingBooksFromList(bookrepository);


        // Assert
        for (int i = 0; i < _booksList.Count; i++)
        {
            var booksOutput = bookrepository.GetId(_booksList[i].Id);
            Assert.AreEqual(_booksList[i].Id, booksOutput.Id);
            Assert.AreEqual(_booksList[i].Title, booksOutput.Title);
            Assert.AreEqual(_booksList[i].Author, booksOutput.Author);
            Assert.AreEqual(_booksList[i].PublicationYear, booksOutput.PublicationYear);
            Assert.AreEqual(_booksList[i].Genre, booksOutput.Genre);
            Assert.AreEqual(_booksList[i].ISBN, booksOutput.ISBN);
            Assert.AreEqual(_booksList[i].PageCount, booksOutput.PageCount);
            Assert.AreEqual(_booksList[i].Price, booksOutput.Price);
            Assert.AreEqual(_booksList[i].IsAvailable, booksOutput.IsAvailable);
        }

        // clean file and delete books that have been added
        CleaningUpTest(bookrepository);
    }

    [TestMethod]
    public void BooksAreDeletedInATestJson()
    {
        // arrange
        // Create a test.json file, pure for testing
        string _fileName = "Tests-data/TestGenericRepositoryDeleting.json";
        IRepository<Book> bookrepository = new GenericRepository<Book>(new FileSystem<Book>(_fileName, new JsonFormatter<Book>()));

        // Act
        AddingBooksFromList(bookrepository);
        // Check if they are added
        for (int i = 0; i < _booksList.Count; i++)
        {
            var booksOutput = bookrepository.GetId(_booksList[i].Id);
            Assert.AreEqual(_booksList[i].Id, booksOutput.Id);
        }

        // Delete them

        CleaningUpTest(bookrepository);


        // Assert
        // Check if they are gone

        for (int i = 0; i < _booksList.Count; i++)
        {
            
            Assert.AreEqual(null, bookrepository.GetId(_booksList[i].Id));
        }
    }


    [TestMethod]
    public void BooksAreUpdatedInATestJson()
    {
        // arrange
        // Create a test.json file, pure for testing
        string _fileName = "Tests-data/TestGenericRepositoryUpdate.json";
        IRepository<Book> bookrepository = new GenericRepository<Book>(new FileSystem<Book>(_fileName, new JsonFormatter<Book>()));

        // Act
        AddingBooksFromList(bookrepository);
        // Check if they are added
        for (int i = 0; i < _booksList.Count; i++)
        {
            var booksOutput = bookrepository.GetId(_booksList[i].Id);
            Assert.AreEqual(_booksList[i].Id, booksOutput.Id);
        }

        // Update the title and release year of the books
        foreach (var book in _booksList)
        {
            book.Title = $"Edited title of \"{book.Title}\"";
            book.PublicationYear = 1990;
            bookrepository.Update(book);
            bookrepository.Save();
        }

        // Assert
        // Check if they are updated
        for (int i = 0; i < _booksList.Count; i++)
        {
            var booksOutput = bookrepository.GetId(_booksList[i].Id);
            Assert.AreEqual(_booksList[i].Id, booksOutput.Id);
            Assert.AreEqual(_booksList[i].Title, booksOutput.Title);
            Assert.AreEqual(_booksList[0].PublicationYear, booksOutput.PublicationYear);
        }


        // clean file and delete books that have been added
        CleaningUpTest(bookrepository);

    }

    [TestMethod]
    public void GettingABookAndThenUpdatingItTest()
    {
        // arrange
        // Create a test.json file, pure for testing
        string _fileName = "Tests-data/TestGenericRepositoryGetAndUpdate.json";
        IRepository<Book> bookrepository = new GenericRepository<Book>(new FileSystem<Book>(_fileName, new JsonFormatter<Book>()));

        // Act
        AddingBooksFromList(bookrepository);
        // Check if they are added
        for (int i = 0; i < _booksList.Count; i++)
        {
            var booksOutput = bookrepository.GetId(_booksList[i].Id);
            Assert.AreEqual(_booksList[i].Id, booksOutput.Id);
        }

        // Update the title and release year of 1 book that is read out of the list

        var books = bookrepository.GetAll();
        var selectedBook = books.FirstOrDefault(item => item.PublicationYear > 2000);
        if (selectedBook != null)
        {
            selectedBook.IsAvailable = true;
            selectedBook.Title = $"One Book Edited: \"{selectedBook.Title}\"";
            bookrepository.Update(selectedBook);
            bookrepository.Save();

            var bookOutput = bookrepository.GetId(selectedBook.Id);
            Assert.AreEqual(selectedBook.Id, bookOutput.Id);
            Assert.AreEqual(selectedBook.Title, bookOutput.Title);
            Assert.AreEqual(selectedBook.PublicationYear, bookOutput.PublicationYear);
        }

        // Assert
        // clean file and delete books that have been added
        CleaningUpTest(bookrepository);
    }

    private void AddingBooksFromList(IRepository<Book> bookrepository)
    {
        foreach (var book in _booksList)
        {
            bookrepository.Add(book);
            bookrepository.Save();

        }
    }

    private void CleaningUpTest(IRepository<Book> bookrepository)
    {
        // clean file and delete books that have been added
        foreach (var book in _booksList)
        {
            bookrepository.Delete(book.Id);        
            bookrepository.Save();

        }            

    }

}
