using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Services;

namespace BookCatalog.Tests;


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

    [Fact]
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
            Assert.Equal(expected[i].Id, orderedListByTitle[i].Id);
            Assert.Equal(expected[i].Title, orderedListByTitle[i].Title);
            Assert.Equal(expected[i].Author, orderedListByTitle[i].Author);
            Assert.Equal(expected[i].PublicationYear, orderedListByTitle[i].PublicationYear);
            Assert.Equal(expected[i].Genre, orderedListByTitle[i].Genre);
            Assert.Equal(expected[i].ISBN, orderedListByTitle[i].ISBN);
            Assert.Equal(expected[i].PageCount, orderedListByTitle[i].PageCount);
            Assert.Equal(expected[i].Price, orderedListByTitle[i].Price);
            Assert.Equal(expected[i].IsAvailable, orderedListByTitle[i].IsAvailable);
        }
    }

    [Fact]
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
            Assert.Equal(expected[i].Id, orderedListByTitle[i].Id);
            Assert.Equal(expected[i].Title, orderedListByTitle[i].Title);
            Assert.Equal(expected[i].Author, orderedListByTitle[i].Author);
            Assert.Equal(expected[i].PublicationYear, orderedListByTitle[i].PublicationYear);
            Assert.Equal(expected[i].Genre, orderedListByTitle[i].Genre);
            Assert.Equal(expected[i].ISBN, orderedListByTitle[i].ISBN);
            Assert.Equal(expected[i].PageCount, orderedListByTitle[i].PageCount);
            Assert.Equal(expected[i].Price, orderedListByTitle[i].Price);
            Assert.Equal(expected[i].IsAvailable, orderedListByTitle[i].IsAvailable);
        }
    }

    [Fact]
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
            Assert.Equal(expected[i].Id, orderedListByTitle[i].Id);
            Assert.Equal(expected[i].Title, orderedListByTitle[i].Title);
            Assert.Equal(expected[i].Author, orderedListByTitle[i].Author);
            Assert.Equal(expected[i].PublicationYear, orderedListByTitle[i].PublicationYear);
            Assert.Equal(expected[i].Genre, orderedListByTitle[i].Genre);
            Assert.Equal(expected[i].ISBN, orderedListByTitle[i].ISBN);
            Assert.Equal(expected[i].PageCount, orderedListByTitle[i].PageCount);
            Assert.Equal(expected[i].Price, orderedListByTitle[i].Price);
            Assert.Equal(expected[i].IsAvailable, orderedListByTitle[i].IsAvailable);
        }
    }

    [Fact]
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
            Assert.Equal(expected[i].Id, orderedListByTitle[i].Id);
            Assert.Equal(expected[i].Title, orderedListByTitle[i].Title);
            Assert.Equal(expected[i].Author, orderedListByTitle[i].Author);
            Assert.Equal(expected[i].PublicationYear, orderedListByTitle[i].PublicationYear);
            Assert.Equal(expected[i].Genre, orderedListByTitle[i].Genre);
            Assert.Equal(expected[i].ISBN, orderedListByTitle[i].ISBN);
            Assert.Equal(expected[i].PageCount, orderedListByTitle[i].PageCount);
            Assert.Equal(expected[i].Price, orderedListByTitle[i].Price);
            Assert.Equal(expected[i].IsAvailable, orderedListByTitle[i].IsAvailable);
        }
    }

    [Fact]
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
            Assert.Equal(expected[i].Id, orderedListByTitle[i].Id);
            Assert.Equal(expected[i].Title, orderedListByTitle[i].Title);
            Assert.Equal(expected[i].Author, orderedListByTitle[i].Author);
            Assert.Equal(expected[i].PublicationYear, orderedListByTitle[i].PublicationYear);
            Assert.Equal(expected[i].Genre, orderedListByTitle[i].Genre);
            Assert.Equal(expected[i].ISBN, orderedListByTitle[i].ISBN);
            Assert.Equal(expected[i].PageCount, orderedListByTitle[i].PageCount);
            Assert.Equal(expected[i].Price, orderedListByTitle[i].Price);
            Assert.Equal(expected[i].IsAvailable, orderedListByTitle[i].IsAvailable);
        }
    }
}
