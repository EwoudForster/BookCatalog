using BookCatalog.DataLayer;
using BookCatalog.DataLayer.FileStorage.Filesystems;
using BookCatalog.DataLayer.FileStorage.Formatting;

namespace BookCatalog.Tests;


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
            Assert.Equal(_booksList[i].Author, booksDeserialized[i].Author);
            Assert.Equal(_booksList[i].PublicationYear, booksDeserialized[i].PublicationYear);
            Assert.Equal(_booksList[i].Genre, booksDeserialized[i].Genre);
            Assert.Equal(_booksList[i].ISBN, booksDeserialized[i].ISBN);
            Assert.Equal(_booksList[i].PageCount, booksDeserialized[i].PageCount);
            Assert.Equal(_booksList[i].Price, booksDeserialized[i].Price);
            Assert.Equal(_booksList[i].IsAvailable, booksDeserialized[i].IsAvailable);
        }
    }

}
