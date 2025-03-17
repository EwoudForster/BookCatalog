
using BookCatalog.DataLayer.Formatting;
using BookCatalog.DataLayer;

namespace BookCatalog.Tests.Repositories;

[TestClass]
public class FormattingTest
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
    public void JsonFormatterSerializingAndDeSerializingTest()
    {
        // arrange
        var formatter = new JsonFormatter<Book>();
        string booksSerialized;
        List<Book> booksDeserialized = new List<Book>();

        // act
        booksSerialized = formatter.Serializer(_booksList);
        booksDeserialized = formatter.DeSerializer(booksSerialized).ToList();

        // assert
        AssertBooks(booksDeserialized);

    }

    [TestMethod]
    public void CSVFormatterSerializingAndDeSerializingTest()
    {
        // arrange
        var formatter = new CsvFormatter<Book>();
        string booksSerialized;
        List<Book> booksDeserialized = new List<Book>();

        // act
        booksSerialized = formatter.Serializer(_booksList);
        booksDeserialized = formatter.DeSerializer(booksSerialized).ToList();

        // assert
        AssertBooks(booksDeserialized);

    }

    [TestMethod]
    public void CSVFormatterReflectionSerializingAndDeSerializingTest()
    {
        // arrange
        var formatter = new CsvFormatterReflection<Book>();
        string booksSerialized;
        List<Book> booksDeserialized = new List<Book>();

        // act
        booksSerialized = formatter.Serialize(_booksList);
        booksDeserialized = formatter.DeSerialize(booksSerialized).ToList();

        // assert
        AssertBooks(booksDeserialized);
    }

    private void AssertBooks(List<Book> booksDeserialized)
    {
        for (int i = 0; i < _booksList.Count; i++)
        {
            Assert.AreEqual(_booksList[i].Id, booksDeserialized[i].Id);
            Assert.AreEqual(_booksList[i].Title, booksDeserialized[i].Title);
            Assert.AreEqual(_booksList[i].Author, booksDeserialized[i].Author);
            Assert.AreEqual(_booksList[i].PublicationYear, booksDeserialized[i].PublicationYear);
            Assert.AreEqual(_booksList[i].Genre, booksDeserialized[i].Genre);
            Assert.AreEqual(_booksList[i].ISBN, booksDeserialized[i].ISBN);
            Assert.AreEqual(_booksList[i].PageCount, booksDeserialized[i].PageCount);
            Assert.AreEqual(_booksList[i].Price, booksDeserialized[i].Price);
            Assert.AreEqual(_booksList[i].IsAvailable, booksDeserialized[i].IsAvailable);
        }
    }
}
