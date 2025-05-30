using BookCatalog.DAL;
using BookCatalog.DAL.Storage.FileStorage.Formatting;


namespace BookCatalog.Tests.Storage;


public class FormattingTest
{
    private readonly List<Book> _booksList = new()
     {
         new()
         {
             Id = Guid.NewGuid(),
             Title = "L'amica geniale (Test)",
             Authors = new List<Author>{ new Author {Name = "Elena Ferrante" } },
             PublicationYear = 2011,
             Genres =new List<Genre>{ new Genre {Name =  "Roman" } },
             ISBN = "978-1609450786",
             PageCount = 336 ,
             Publisher = new Publisher{ Name = "Big Company" } ,
             Price = 13.75M,
             IsAvailable = true
         },
         new()
         {
             Id = Guid.NewGuid(),
             Title = "Il Gattopardo (Test)",
             Authors = new List<Author>{ new Author {Name = "Giuseppe Tomasi di Lampedusa" } },
             PublicationYear = 1958,
             Genres = new List<Genre>{ new Genre {Name = "Historische Roman" } },
             ISBN = "978-8845927361",
             PageCount = 440,
             Publisher = new Publisher { Name = "Big Company" } ,
             Price = 14.39M,
             IsAvailable = false
         }
     };
    [Fact]
    public void JsonFormatterSerializingAndDeSerializingTest()
    {
        // arrange
        JsonFormatter<Book> formatter = new();
        string booksSerialized;
        List<Book> booksDeserialized = [];

        // act
        booksSerialized = formatter.Serializer(_booksList);
        booksDeserialized = formatter.DeSerializer(booksSerialized).ToList();

        // assert
        AssertBooks(booksDeserialized);

    }

    [Fact]
    public void CSVFormatterSerializingAndDeSerializingTest()
    {
        // arrange
        var formatter = new CsvFormatter<Book>();
        string booksSerialized;
        List<Book> booksDeserialized = [];

        // act
        booksSerialized = formatter.Serializer(_booksList);
        booksDeserialized = formatter.DeSerializer(booksSerialized).ToList();

        // assert
        AssertBooks(booksDeserialized);

    }

  /*  [Fact]
    public void CSVFormatterReflectionSerializingAndDeSerializingTest()
    {
        // arrange
        CsvFormatterReflection<Book> formatter = new();
        string booksSerialized;
        List<Book> booksDeserialized = [];

        // act
        booksSerialized = formatter.Serialize(_booksList);
        booksDeserialized = formatter.DeSerialize(booksSerialized).ToList();

        // assert
        AssertBooks(booksDeserialized);
    }*/

    private void AssertBooks(List<Book> booksDeserialized)
    {
        for (int i = 0; i < _booksList.Count; i++)
        {
            Assert.Equal(_booksList[i].Id, booksDeserialized[i].Id);
            Assert.Equal(_booksList[i].Title, booksDeserialized[i].Title);
            for (int j = 0; j < booksDeserialized[i].Authors.Count(); j++)
            {
                Assert.Equal(_booksList[i].Authors[j].Id, booksDeserialized[i].Authors[j].Id);
                Assert.Equal(_booksList[i].Authors[j].Name, booksDeserialized[i].Authors[j].Name);

            }
            Assert.Equal(_booksList[i].PublicationYear, booksDeserialized[i].PublicationYear);

            for (int j = 0; j < booksDeserialized[i].Genres.Count(); j++)
            {
                Assert.Equal(_booksList[i].Genres[j].Id, booksDeserialized[i].Genres[j].Id);
                Assert.Equal(_booksList[i].Genres[j].Name, booksDeserialized[i].Genres[j].Name);

            }
            Assert.Equal(_booksList[i].Publisher.Id, booksDeserialized[i].Publisher.Id);
            Assert.Equal(_booksList[i].Publisher.Name, booksDeserialized[i].Publisher.Name);
            Assert.Equal(_booksList[i].ISBN, booksDeserialized[i].ISBN);
            Assert.Equal(_booksList[i].PageCount, booksDeserialized[i].PageCount);
            Assert.Equal(_booksList[i].Price, booksDeserialized[i].Price);
            Assert.Equal(_booksList[i].IsAvailable, booksDeserialized[i].IsAvailable);
        }
    }
}
