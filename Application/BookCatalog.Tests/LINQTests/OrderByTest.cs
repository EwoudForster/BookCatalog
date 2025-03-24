using BookCatalog.DataLayer;
using BookCatalog.ConsoleApplication.Services;

namespace BookCatalog.Tests;

[TestClass]
public class OrderByTest
{

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
    public void OrderByTitleTest()
    {
        // Arrange
        List<Book> orderedListByTitle = new List<Book>();
        var expected = _booksList.OrderBy(x => x.Title).ToList();
        // Act
        orderedListByTitle = _booksList.OrderBookBy().ToList();


        // Assert
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i].Id, orderedListByTitle[i].Id);
            Assert.AreEqual(expected[i].Title, orderedListByTitle[i].Title);
            Assert.AreEqual(expected[i].Author, orderedListByTitle[i].Author);
            Assert.AreEqual(expected[i].PublicationYear, orderedListByTitle[i].PublicationYear);
            Assert.AreEqual(expected[i].Genre, orderedListByTitle[i].Genre);
            Assert.AreEqual(expected[i].ISBN, orderedListByTitle[i].ISBN);
            Assert.AreEqual(expected[i].PageCount, orderedListByTitle[i].PageCount);
            Assert.AreEqual(expected[i].Price, orderedListByTitle[i].Price);
            Assert.AreEqual(expected[i].IsAvailable, orderedListByTitle[i].IsAvailable);
        }
    }

    [TestMethod]
    public void OrderByPriceTest()
    {
        // Arrange
        List<Book> orderedListByTitle = new List<Book>();
        var expected = _booksList.OrderBy(x => x.Price).ToList();
        // Act
        orderedListByTitle = _booksList.OrderBookBy("PRICE").ToList();


        // Assert
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i].Id, orderedListByTitle[i].Id);
            Assert.AreEqual(expected[i].Title, orderedListByTitle[i].Title);
            Assert.AreEqual(expected[i].Author, orderedListByTitle[i].Author);
            Assert.AreEqual(expected[i].PublicationYear, orderedListByTitle[i].PublicationYear);
            Assert.AreEqual(expected[i].Genre, orderedListByTitle[i].Genre);
            Assert.AreEqual(expected[i].ISBN, orderedListByTitle[i].ISBN);
            Assert.AreEqual(expected[i].PageCount, orderedListByTitle[i].PageCount);
            Assert.AreEqual(expected[i].Price, orderedListByTitle[i].Price);
            Assert.AreEqual(expected[i].IsAvailable, orderedListByTitle[i].IsAvailable);
        }
    }

    [TestMethod]
    public void OrderByPublisherTest()
    {
        // Arrange
        List<Book> orderedListByTitle = new List<Book>();
        var expected = _booksList.OrderBy(x => x.Publisher).ToList();
        // Act
        orderedListByTitle = _booksList.OrderBookBy("PubLiSher").ToList();


        // Assert
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i].Id, orderedListByTitle[i].Id);
            Assert.AreEqual(expected[i].Title, orderedListByTitle[i].Title);
            Assert.AreEqual(expected[i].Author, orderedListByTitle[i].Author);
            Assert.AreEqual(expected[i].PublicationYear, orderedListByTitle[i].PublicationYear);
            Assert.AreEqual(expected[i].Genre, orderedListByTitle[i].Genre);
            Assert.AreEqual(expected[i].ISBN, orderedListByTitle[i].ISBN);
            Assert.AreEqual(expected[i].PageCount, orderedListByTitle[i].PageCount);
            Assert.AreEqual(expected[i].Price, orderedListByTitle[i].Price);
            Assert.AreEqual(expected[i].IsAvailable, orderedListByTitle[i].IsAvailable);
        }
    }

    [TestMethod]
    public void OrderByPublicationYearTest()
    {
        // Arrange
        List<Book> orderedListByTitle = new List<Book>();
        var expected = _booksList.OrderBy(x => x.PublicationYear).ToList();
        // Act
        orderedListByTitle = _booksList.OrderBookBy("PubliCationYear").ToList();


        // Assert
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i].Id, orderedListByTitle[i].Id);
            Assert.AreEqual(expected[i].Title, orderedListByTitle[i].Title);
            Assert.AreEqual(expected[i].Author, orderedListByTitle[i].Author);
            Assert.AreEqual(expected[i].PublicationYear, orderedListByTitle[i].PublicationYear);
            Assert.AreEqual(expected[i].Genre, orderedListByTitle[i].Genre);
            Assert.AreEqual(expected[i].ISBN, orderedListByTitle[i].ISBN);
            Assert.AreEqual(expected[i].PageCount, orderedListByTitle[i].PageCount);
            Assert.AreEqual(expected[i].Price, orderedListByTitle[i].Price);
            Assert.AreEqual(expected[i].IsAvailable, orderedListByTitle[i].IsAvailable);
        }
    }

    [TestMethod]
    public void OrderByPageCountTest()
    {
        // Arrange
        List<Book> orderedListByTitle = new List<Book>();
        var expected = _booksList.OrderBy(x => x.PageCount).ToList();
        // Act
        orderedListByTitle = _booksList.OrderBookBy("PageCount").ToList();


        // Assert
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i].Id, orderedListByTitle[i].Id);
            Assert.AreEqual(expected[i].Title, orderedListByTitle[i].Title);
            Assert.AreEqual(expected[i].Author, orderedListByTitle[i].Author);
            Assert.AreEqual(expected[i].PublicationYear, orderedListByTitle[i].PublicationYear);
            Assert.AreEqual(expected[i].Genre, orderedListByTitle[i].Genre);
            Assert.AreEqual(expected[i].ISBN, orderedListByTitle[i].ISBN);
            Assert.AreEqual(expected[i].PageCount, orderedListByTitle[i].PageCount);
            Assert.AreEqual(expected[i].Price, orderedListByTitle[i].Price);
            Assert.AreEqual(expected[i].IsAvailable, orderedListByTitle[i].IsAvailable);
        }
    }
}
