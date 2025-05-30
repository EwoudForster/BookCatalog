using BookCatalog.DAL;
using BookCatalog.DAL.FileStorage.Formatting;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Repositories.nonasync;
using BookCatalog.DAL.Services;
using BookCatalog.DAL.Storage.FileStorage.Filesystems;

namespace BookCatalog
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileName = "sample-books.json";
            var logger = new GeneralLogger();
            // creation of a new repository with as fileSystem a FileSystem with a JsonFormatter
            IRepository<Book> bookrepository = new FileRepository<Book>(new FileSystem<Book>(fileName, new JsonFormatter<Book>()), logger);


             //var repositoryFactory = new RepositoryFactory<Book>();
           //  IRepository<Book> bookrepository = repositoryFactory.GetDataSystem();

            // creation of a new service with the repository
            BookService bookService = new BookService(bookrepository, logger);

            // start the service
            bookService.Start();
            //ISerialize<Book> formatter = new JsonFormatter<Book>(); 


            // string baseAddressConfig = configuration.GetValue<string>("BaseAddress");



        }
    }
}