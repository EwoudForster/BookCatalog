using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Filesystems;
using BookCatalog.DataLayer.Formatting;

namespace BookCatalog.Tests.Repositories;

[TestClass]
public class FileSystemTest
{
    private readonly string _fileNameJson = "Tests-data/TestFileSystemJson.json";
    private readonly string _fileNameCsv = "Tests-data/TestFileSystemCsv.csv";
    private readonly string _fileNameCsvReflection = "Tests-data/TestFileSystemCsvReflection.csv";


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
    public void FileSystemSavingAndReadingJsonTest()
    {
        // arrange
        var formatter = new JsonFormatter<Book>();
        List<Book> booksDeserialized = new List<Book>();

        var fileSystem = new FileSystem<Book>(_fileNameJson, formatter);
        // act
        fileSystem.Save(_booksList);
        booksDeserialized = fileSystem.Read().ToList();

        // assert
        AssertBooks(booksDeserialized);
    }


    [TestMethod]
    public void FileSystemSavingAndReadingCsvTest()
    {
        // arrange
        var formatter = new CsvFormatter<Book>();
        List<Book> booksDeserialized = new List<Book>();

        var fileSystem = new FileSystem<Book>(_fileNameCsv, formatter);
        // act
        fileSystem.Save(_booksList);
        booksDeserialized = fileSystem.Read().ToList();

        // assert
        AssertBooks(booksDeserialized);

    }

    [TestMethod]
    public void FileSystemSavingAndReadingCsvReflectionTest()
    {
        // arrange
        var formatter = new CsvFormatter<Book>();
        List<Book> booksDeserialized = new List<Book>();

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
