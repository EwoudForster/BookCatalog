using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Filesystems;
using BookCatalog.DataLayer.Formatting;
using BookCatalog.DataLayer.Repositories;
using BookCatalog.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var fileName = "sample-books.json";

        IRepository<Book> bookrepository = new GenericRepository<Book>(new FileSystem<Book>(fileName, new JsonFormatter<Book>()));
        BookService bookService = new BookService(bookrepository);
        bookService.Start();

        //var repositoryFactory = new RepositoryFactory<Book>();
        //IRepository<Book> reader = repositoryFactory.GetDataSystem();

        //ISerialize<Book> formatter = new JsonFormatter<Book>(); 


        // string baseAddressConfig = configuration.GetValue<string>("BaseAddress");



    }
}