using BookCatalog.DataLayer;
using BookCatalog.DataLayer.FileStorage.Filesystems;
using BookCatalog.DataLayer.FileStorage.Formatting;
using BookCatalog.DataLayer.Repositories;
using BookCatalog.ConsoleApplication.Services;

namespace BookCatalog.ConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileName = "sample-books.json";

            // creation of a new repository with as fileSystem a FileSystem with a JsonFormatter
            IRepository<Book> bookrepository = new GenericRepository<Book>(new FileSystem<Book>(fileName, new JsonFormatter<Book>()));


            // var repositoryFactory = new RepositoryFactory<Book>();
            //IRepository<Book> bookrepository = repositoryFactory.GetDataSystem();

            // creation of a new service with the repository
            BookService bookService = new BookService(bookrepository);

            // start the service
            bookService.Start();
            //ISerialize<Book> formatter = new JsonFormatter<Book>(); 


            // string baseAddressConfig = configuration.GetValue<string>("BaseAddress");



        }
    }
}