using BookCatalog.DataLayer.Logging;
using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;

namespace BookCatalog.Services
{
    public class BookService{
        private readonly IRepository<Book> _repository;
        private readonly ILogger _logger = new Logger();

        public BookService(IRepository<Book> repository)
        {
            // Making a repository to write away data
            _repository = repository;
        }

        // start the catalog system
        public void Start()
        {
            Console.WriteLine("=== WELCOME TO OUR BOOK CATALOG SYSTEM ==\n");
            try
            {
                while (true)
                {
                    Console.WriteLine("WHAT OPERATION DO YOU WANT TO DO?");
                    Console.WriteLine("1: SEARCHING A BOOK? (type search)");
                    Console.WriteLine("2: ADDING A BOOK? (type add)");
                    Console.WriteLine("3: UPDATING A BOOK? (type update)");
                    Console.WriteLine("4: DELETING A BOOK? (type delete)");

                    // Request a choice in the program
                    var choice = ConsoleHelper.RequestString("Choice", required: true);
                    switch (choice.ToLower())
                    {
                        case "1":
                        case var s when choice.Contains("search"):
                        case var g when choice.Contains("get"):
                            searchBook();
                            break;

                        case "2":
                        case var a when choice.Contains("add"):
                        case var n when choice.Contains("new"):
                            AddBook();
                            _repository.Save();
                            break;

                        case "3":
                        case var up when choice.Contains("update"):
                        case var ed when choice.Contains("edit"):
                        case var mo when choice.Contains("modify"):
                            UpdateBook();
                            _repository.Save();
                            break;

                        case "4":
                        case var del when choice.Contains("delete"):
                        case var rem when choice.Contains("remove"):
                        case var era when choice.Contains("erase"):
                            DeleteBook();
                            _repository.Save();
                            break;

                        default:
                            Console.WriteLine("NO MATCHES WERE FOUND! TRY AGAIN");
                            break;
                    }
                    Console.Clear();
                }

            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nOperation canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        private void AddBook()
            {
                Console.Clear();
                Console.WriteLine("=== ADD NEW BOOK ===\n");

                var book = new Book();

                try
                {
                    // Request and validate book data
                    book.Title = ConsoleHelper.RequestString("Title", required: true);
                    book.Author = ConsoleHelper.RequestString("Author", required: true);
                    book.PublicationYear = ConsoleHelper.RequestInteger("Publication year",
                        minValue: 1000, maxValue: DateTime.Now.Year);
                    book.Genre = ConsoleHelper.RequestString("Genre", required: false);
                    book.Publisher = ConsoleHelper.RequestString("Publisher", required: false);
                    book.ISBN = ConsoleHelper.RequestString("ISBN", required: true);
                    book.PageCount = ConsoleHelper.RequestInteger("Page count", minValue: 1);
                    book.Price = ConsoleHelper.RequestDecimal("Price", minValue: 0);
                    book.IsAvailable = true; // A new book is always available
                    Console.WriteLine();
                    // Add the book to the repository
                    _repository.Add(book);

                    Console.WriteLine("\nBook added successfully!");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("\nOperation canceled.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError while adding the book: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

        private void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine("=== DELETE A BOOK ===\n");

            try
            {
                // Request Id
                Console.WriteLine("=== WHAT IS THE ID, PUBLISHER, TITLE, GENRE ===");
                List<Book> result;
                List<Book> finalResult;
                do
                {
                var query = ConsoleHelper.RequestString("Search", required: true);
                result = _repository.GetAll().Search(query).ToList();
                finalResult= result;
                while (finalResult.Count > 1) {
                    Console.WriteLine("\nRESULTS:\n\n");
                    foreach (var item in finalResult)
                    {
                            _logger.Log("Found Book:", item);
                    }
                    Console.WriteLine("WHICH ONE OF THESE DO YOU WANT TO DELETE?");
                    query = ConsoleHelper.RequestString("Search", required: true);
                    finalResult = result.Search(query).ToList();
                }
                Console.WriteLine("THIS IS THE SELECTION");
                if (finalResult.Count == 0)
                {
                        Console.WriteLine("THERE WERE NO RESULTS FOUND FOR YOUR QUERY, TRY AGAIN");
                }
                } while (finalResult.Count == 0 );

                if (finalResult.Count == 1)
                {
                    Console.WriteLine($"THIS IS THE ITEM THAT YOU WANT TO DELETE:\n{finalResult[0].ToString()}");
                    Console.WriteLine("\nDO YOU WANT TO DELETE THIS?: Y, (n)");

                    var anwser = ConsoleHelper.RequestBoolean("Answer", false);
                    if (anwser)
                    {
                        _repository.Delete(finalResult[0].Id);
                        
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nOperation canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError while adding the book: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void UpdateBook()
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE A BOOK ===\n");
            var book = new Book();
            try
            {
                // Request Id
                Console.WriteLine("=== WHAT IS THE ID, PUBLISHER, TITLE, GENRE ===");
                List<Book> result;
                List<Book> finalResult;
                do
                {
                    var query = ConsoleHelper.RequestString("Search", required: true);
                    result = _repository.GetAll().Search(query).ToList();
                    finalResult = result;
                    while (finalResult.Count > 1)
                    {
                        Console.WriteLine("\nRESULTS:\n\n");
                        foreach (var item in finalResult)
                        {
                            _logger.Log("Found Book:", item);
                        }
                        Console.WriteLine("WHICH ONE OF THESE DO YOU WANT TO UPDATE?");
                        query = ConsoleHelper.RequestString("Search", required: true);
                        finalResult = result.Search(query).ToList();
                    }
                    Console.WriteLine("\nTHIS IS THE SELECTION");
                    if (finalResult.Count == 0)
                    {
                        Console.WriteLine("\nTHERE WERE NO RESULTS FOUND FOR YOUR QUERY, TRY AGAIN");
                    }
                } while (finalResult.Count > 1 || finalResult.Count == 0);

                if (finalResult.Count == 1)
                {
                    book = finalResult[0];
                    Console.WriteLine($"THIS IS THE ITEM THAT YOU WANT TO UPDATE:\n\n{finalResult[0].ToString()}");
                    Console.WriteLine("\nDO YOU WANT TO UPDATE THIS?: Y, (n)");

                    var anwser = ConsoleHelper.RequestBoolean("Answer", false);
                    if (anwser)
                    {
                        // Request and validate book data
                        book.Title = ConsoleHelper.RequestString("Title", required: true);
                        book.Author = ConsoleHelper.RequestString("Author", required: true);
                        book.PublicationYear = ConsoleHelper.RequestInteger("Publication year",
                            minValue: 1000, maxValue: DateTime.Now.Year);
                        book.Genre = ConsoleHelper.RequestString("Genre", required: false);
                        book.Publisher = ConsoleHelper.RequestString("Publisher", required: false);
                        book.ISBN = ConsoleHelper.RequestString("ISBN", required: true);
                        book.PageCount = ConsoleHelper.RequestInteger("Page count", minValue: 1);
                        book.Price = ConsoleHelper.RequestDecimal("Price", minValue: 0);
                        book.IsAvailable = ConsoleHelper.RequestBoolean("Available?", true); // A new book is always available
                        Console.WriteLine();
                        // Add the book to the repository
                        _repository.Update(book);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nOperation canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError while adding the book: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void searchBook()
        {
            Console.Clear();
            Console.WriteLine("=== SEARCH ===\n");

            try
            {
                // Request Id
                Console.WriteLine("=== WHAT IS THE ID, PUBLISHER, TITLE, GENRE ===");
                List<Book> result;
                List<Book> finalResult = new List<Book>();
                bool found = false;
                while (!found)
                {
                    do
                    {
                        var query = ConsoleHelper.RequestString("Search", required: true);
                        result = _repository.GetAll().Search(query).ToList();
                        Console.WriteLine("\nRESULTS:\n\n");
                        foreach (var item in result)
                        {
                            finalResult.Add(item);
                            _logger.Log("Found Book:", item);
                        }
                        if (result.Count == 0)
                        {
                            Console.WriteLine("THERE WERE NO RESULTS FOUND FOR YOUR QUERY, TRY AGAIN\n");
                        }
                        else
                        {
                            finalResult = result.Search(query).ToList();
                            Console.WriteLine("\nDID YOU FOUND WHAT YOU WERE LOOKING FOR?: Y, (n)");
                            
                            var anwser = ConsoleHelper.RequestBoolean("Answer", false);
                            if (anwser)
                            {
                                throw new OperationCanceledException();
                            }
                        }
                    } while (finalResult.Count == 0);
                }
            }

            catch (OperationCanceledException)
            {
                Console.WriteLine("\nOperation canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError while adding the book: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    } 
}
