using BookCatalog.DAL;
using BookCatalog.DAL.Storage.FileStorage.Filesystems;
using BookCatalog.DAL.Storage.FileStorage.Formatting;

namespace BookCatalog.Tests.Storage;


public class FileSystemTest
{
    private readonly string _fileNameJson = "TestFileSystemJson.json";
    private readonly string _fileNameCsv = "TestFileSystemCsv.csv";
    private readonly string _fileNameCsvReflection ="TestFileSystemCsvReflection.csv";

    private readonly List<Book> _booksList = new()
     {
         new()
         {
             Id = Guid.NewGuid(),
             Title = "L'amica geniale (Test)",
             Authors = new List<Author>{ new Author {Name = "Elena Ferrante" } },
             PublicationYear = 2011,
             Genres = new List<Genre>{ new Genre { Name = "Roman" } },
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
    public void FileSystemSavingAndReadingJsonTest()
    {
        // arrange
        var formatter = new JsonFormatter<Book>();
        List<Book> booksDeserialized = new();

        var fileSystem = new FileSystem<Book>(_fileNameJson, formatter);
        // act
        fileSystem.Save(_booksList);
        booksDeserialized = fileSystem.Read().ToList();

        // assert
        AssertBooks(booksDeserialized);
    }


    [Fact]
    public void FileSystemSavingAndReadingCsvTest()
    {
        // arrange
        var formatter = new CsvFormatter<Book>();
        List<Book> booksDeserialized = [];

        var fileSystem = new FileSystem<Book>(_fileNameCsv, formatter);
        // act
        fileSystem.Save(_booksList);
        booksDeserialized = fileSystem.Read().ToList();

        // assert
        AssertBooks(booksDeserialized);

    }

    [Fact]
    public void FileSystemSavingAndReadingCsvReflectionTest()
    {
        // arrange
        var formatter = new CsvFormatter<Book>();
        List<Book> booksDeserialized = [];

        var fileSystem = new FileSystem<Book>(_fileNameCsvReflection, formatter);
        // act
        fileSystem.Save(_booksList);
        booksDeserialized = fileSystem.Read().ToList();

        // assert
        AssertBooks(booksDeserialized);
    }

    private void AssertBooks(List<Book> booksDeserialized)
    {
        for (int i = 0; i < _booksList.Count; i++)
        {
            Assert.Equal(_booksList[i].Id, booksDeserialized[i].Id);
            Assert.Equal(_booksList[i].Title, booksDeserialized[i].Title);
            for (int j = 0; j<booksDeserialized[i].Authors.Count(); j++)
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
