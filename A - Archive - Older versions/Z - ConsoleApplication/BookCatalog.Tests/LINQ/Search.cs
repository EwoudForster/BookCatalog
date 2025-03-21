using BookCatalog.DataLayer;
using BookCatalog.Services;

namespace BookCatalog.Tests;

[TestClass]
public class Search
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
    public void SearchTitleTest()
    {
        // Arrange
        List<Book> orderedListByTitle = new List<Book>();
        var expected = _booksList.Where(x => x.Title.Contains("Il Gattopardo")).ToList();
        // Act
        orderedListByTitle = _booksList.Search("Il Gattopardo").ToList();


        // Assert
        CheckEqual(orderedListByTitle, expected);

    }

    [TestMethod]
    public void SearchPublisherTest()
    {
        // Arrange
        List<Book> orderedListByTitle = new List<Book>();
        var expected = _booksList.Where(x => x.Publisher.Contains("Giuseppe")).ToList();
        // Act
        orderedListByTitle = _booksList.Search("Giuseppe").ToList();


        // Assert
        CheckEqual(orderedListByTitle, expected);

    }

    [TestMethod]
    public void SearchGenreTest()
    {
        // Arrange
        List<Book> orderedListByTitle = new List<Book>();
        var expected = _booksList.Where(x => x.Genre.Contains("Roman")).ToList();
        // Act
        orderedListByTitle = _booksList.Search("Roman").ToList();


        // Assert
        CheckEqual(orderedListByTitle, expected);

    }

    [TestMethod]
    public void SearchAuthorTest()
    {
        // Arrange
        List<Book> orderedListByTitle = new List<Book>();
        var expected = _booksList.Where(x => x.Author.Contains("Elena")).ToList();

        // Act
        orderedListByTitle = _booksList.Search("elena").ToList();

        // Assert
        CheckEqual(orderedListByTitle, expected);
    }

    private static void CheckEqual(List<Book> orderedListByTitle, List<Book> expected)
    {
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
