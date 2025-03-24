using BookCatalog.DataLayer.Logging;
using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;
namespace BookCatalog.ConsoleApplication.Services
    {
    public class BookService{
        // Declaring repository and Logger
        private readonly IRepository<Book> _repository;
        private readonly IGeneralLogger _logger = new Logger();

        // constructor with dependency injection
        public BookService(IRepository<Book> repository)
        {
            // Making a repository to write away data
            _repository = repository;
        }

        // start the catalog system
        public void Start()
        {
            // Header of the start method
            Console.WriteLine("=== WELCOME TO OUR BOOK CATALOG SYSTEM ==\n");
            // Exception handling incase of errors, but also in case of exit of the program
            try
            {
                // While loop to keep the program running till exit
                while (true)
                {
                    // Giving the user options to choose from
                    Console.WriteLine("WHAT OPERATION DO YOU WANT TO DO?");
                    Console.WriteLine("1: SEARCHING A BOOK? (type search)");
                    Console.WriteLine("2: ADDING A BOOK? (type add)");
                    Console.WriteLine("3: UPDATING A BOOK? (type update)");
                    Console.WriteLine("4: DELETING A BOOK? (type delete)");
                    Console.WriteLine("5: STATISTICS ABOUT THE BOOKCATALOG? (type stats)");
                    Console.WriteLine("6: GROUP BY PUBLICATION YEAR (type year)");
                    Console.WriteLine("7: GROUP BY GENRE (type genre)");

                    // Request a choice in the program with string validation
                    var choice = ConsoleHelper.RequestString("Choice", required: true);

                    // Switch to process the coiche of the user, and call the right method
                    // accept answers that look like it or are similar to the choice 
                    switch (choice.ToLower())
                    {
                        // search a book
                        case "1":
                        case var s when choice.Contains("search"):
                        case var g when choice.Contains("get"):
                            // redirect to the search method
                            searchBook();
                            break;

                        // add a book
                        case "2":
                        case var a when choice.Contains("add"):
                        case var n when choice.Contains("new"):
                            // redirect to the add method
                            AddBook();
                            // save the changes
                            break;

                        // update a book
                        case "3":
                        case var up when choice.Contains("update"):
                        case var ed when choice.Contains("edit"):
                        case var mo when choice.Contains("modify"):
                            // redirect to the update method
                            UpdateBook();
                            // save the changes
                            break;

                        // delete a book
                        case "4":
                        case var del when choice.Contains("delete"):
                        case var rem when choice.Contains("remove"):
                        case var era when choice.Contains("erase"):
                            // redirect to the delete method
                            DeleteBook();
                            // save the changes
                            break;

                        // view statistics
                        case "5":
                        case var st when choice.Contains("stats"):
                        case var nu when choice.Contains("numbers"):
                        case var da when choice.Contains("dashboard"):
                            // redirect to the statistics method
                            ViewStatistics();
                            // no save because nothing is changed
                            break;

                        // group by publication year
                        case "6":
                        case var st when choice.Contains("year"):
                            GroupYear();
                            // no save because nothing is changed
                            break;
                        // group by genre
                        case "7":
                        case var st when choice.Contains("genre"):
                            // redirect to the group by genre method
                            GroupGenre();
                            // no save because nothing is changed
                            break;

                        // no matches were found, then the while loop goes again
                        default:
                            Console.WriteLine("NO MATCHES WERE FOUND! TRY AGAIN");
                            break;
                    }
                    Console.Clear();
                }

            }
            // catch if the user wants to exit the program in the string validation
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nOperation canceled.");
            }
            // other exceptions
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        // Method for adding a new book to the catalog
        private void AddBook()
            {
                // clear the console, so the user can see clearly what is happening
                Console.Clear();
                Console.WriteLine("=== ADD NEW BOOK ===\n");

                // Create a new book object to fill in, and give in the repository
                var book = new Book();

                try
                {
                    // Request and validate book data
                    // Title
                    book.Title = ConsoleHelper.RequestString("Title", required: true);

                    // Author
                    book.Author = ConsoleHelper.RequestString("Author", required: true);

                    // Publication year
                    book.PublicationYear = ConsoleHelper.RequestInteger("Publication year",
                    minValue: 1000, maxValue: DateTime.Now.Year);

                    // Genre
                    book.Genre = ConsoleHelper.RequestString("Genre", required: false);

                    // Publisher
                    book.Publisher = ConsoleHelper.RequestString("Publisher", required: false);
                
                    // ISBN
                    book.ISBN = ConsoleHelper.RequestString("ISBN", required: true);

                    // Page count
                    book.PageCount = ConsoleHelper.RequestInteger("Page count", minValue: 1);

                    // Price
                    book.Price = ConsoleHelper.RequestDecimal("Price", minValue: 0);

                    // Availability
                    book.IsAvailable = true; // A new book is always available, but when updating the book you can choose
                    Console.WriteLine();
                    // Add the book to the repository
                    _repository.Add(book);

                    Console.WriteLine("\nBook added successfully!");
                }

                // again if the exit is typed in the string validation, the operationCancledException will be thrown and the user can exit the program
                catch (OperationCanceledException)
                {
                    Console.WriteLine("\nOperation canceled.");
                }

                // catch any other errors
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError while adding the book: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

        private void DeleteBook()
        {
            // clear the console, so the user can see clearly what is happening
            Console.Clear();
            Console.WriteLine("=== DELETE A BOOK ===\n");

            try
            {
                List<Book> result;
                List<Book> finalResult;
                var books = _repository.GetAll();

                do
                {
                    // Declaring the result and finalResult for the search and the search till there is only one result
                    result = new List<Book>();
                    finalResult = new List<Book>();

                    Console.Clear();
                    // Giving the option to look for the book on ID or on TITLE, PUBLISHER, AUTHOR, GENRE
                    Console.WriteLine("=== DO YOU WANT TO LOOK FOR THE BOOK ON ===");
                    Console.WriteLine("1. TITLE, PUBLISHER, AUTHOR, GENRE (name)");
                    Console.WriteLine("2. ID (id)");

                    // Request a choice in the program with string validation
                    var choice = ConsoleHelper.RequestString("Choice", required: true);

                    // Request the search query
                    var query = ConsoleHelper.RequestString("Search", required: true);

                    // applying the choice of the user
                    switch (choice.ToLower())
                    {
                        // search a book on ID
                        case "2":
                        case var s when choice.Contains("id"):
                        case var g when choice.Contains("number"):
                            var id = Guid.Parse(query);
                            // the search method is used to find the book on ID, if the id is not found, the result will be a new book
                            // if the id is not valid it will throw an exception
                            result.Add(books.Search(id));
                            // make them the same, so the while loop will stop
                            finalResult = result;
                            break;

                        // searching a book on TITLE, PUBLISHER, AUTHOR, GENRE
                        default:
                            result = _repository.GetAll().Search(query).ToList();
                            finalResult = result;

                            // loop till there is only one result
                            while (finalResult.Count > 1)
                            {
                                Console.WriteLine("\nRESULTS:\n\n");
                                foreach (var item in finalResult)
                                {
                                    // show the results each time
                                    _logger.Log("Found Book:", item);
                                    Console.WriteLine();
                                }
                                Console.WriteLine("WHICH ONE OF THESE DO YOU WANT TO DELETE?");
                                Console.WriteLine("If you want to go to the full list type: 1");
                                // Request the search query
                                query = ConsoleHelper.RequestString("Search", required: true);
                                if (query.Trim().ToLower() == "1")
                                {
                                    finalResult = new List<Book>();
                                }
                                else {
                                    // search again
                                    finalResult = result.Search(query).ToList();
                                    Console.WriteLine("THIS IS THE SELECTION");
                                    // if there are no results we return that there are no results and let them try again
                                    if (finalResult.Count == 0)
                                    {
                                        Console.WriteLine("THERE WERE NO RESULTS FOUND FOR YOUR QUERY, TRY AGAIN");
                                        Console.ReadKey();
                                    }
                                }
                                
                            }                    
                            break;
                    }

                    // if there are no results, the user can search again
                    if (finalResult.Count == 0)
                    {
                        Console.WriteLine("THERE WERE NO RESULTS FOUND FOR YOUR QUERY, TRY AGAIN");
                        Console.ReadKey();

                    }

                    // if there is only one result, the user can delete the book after confirmation
                    if (finalResult.Count == 1)
                    {
                        Console.WriteLine($"THIS IS THE ITEM THAT YOU WANT TO DELETE:\n{finalResult[0].ToString()}");
                        Console.WriteLine("\nDO YOU WANT TO DELETE THIS?: Y, (n)");
                        Console.WriteLine("Type exit to exit to the home screen");

                        // bool requester, with validation and default value
                        var anwser = ConsoleHelper.RequestBoolean("Answer", false);
                        if (anwser)
                        {
                            // delete the book
                            _repository.Delete(finalResult[0].Id);
                        }
                        else
                        {
                            // if the user does not want to delete the book, the final result will be empty so the user can search again or exit
                            finalResult = new List<Book>();
                        }
                        
                    }
                }
                while (finalResult.Count == 0);
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
            // clear the console, so the user can see clearly what is happening
            Console.Clear();
            Console.WriteLine("=== UPDATE A BOOK ===\n");
            var book = new Book();
            try
            {
                    List<Book> result;
                    List<Book> finalResult;
                    var books = _repository.GetAll();

                    do
                    {
                        // Declaring the result and finalResult for the search and the search till there is only one result
                        result = new List<Book>();
                        finalResult = new List<Book>();

                        Console.Clear();
                        // Giving the option to look for the book on ID or on TITLE, PUBLISHER, AUTHOR, GENRE
                        Console.WriteLine("=== DO YOU WANT TO LOOK FOR THE BOOK ON ===");
                        Console.WriteLine("1. TITLE, PUBLISHER, AUTHOR, GENRE (name)");
                        Console.WriteLine("2. ID (id)");

                        // Request a choice in the program with string validation
                        var choice = ConsoleHelper.RequestString("Choice", required: true);

                        // Request the search query
                        var query = ConsoleHelper.RequestString("Search", required: true);

                        // applying the choice of the user
                        switch (choice.ToLower())
                        {
                            // search a book on ID
                            case "2":
                            case var s when choice.Contains("id"):
                            case var g when choice.Contains("number"):
                                var id = Guid.Parse(query);
                                // the search method is used to find the book on ID, if the id is not found, the result will be a new book
                                // if the id is not valid it will throw an exception
                                result.Add(books.Search(id));
                                // make them the same, so the while loop will stop
                                finalResult = result;
                                break;

                            // searching a book on TITLE, PUBLISHER, AUTHOR, GENRE
                            default:
                                result = _repository.GetAll().Search(query).ToList();
                                finalResult = result;

                                // loop till there is only one result
                                while (finalResult.Count > 1)
                                {
                                    Console.WriteLine("\nRESULTS:\n\n");
                                    foreach (var item in finalResult)
                                    {
                                        // show the results each time
                                        _logger.Log("Found Book:", item);
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("WHICH ONE OF THESE DO YOU WANT TO DELETE?");
                                    Console.WriteLine("If you want to go to the full list type: 1");
                                    // Request the search query
                                    query = ConsoleHelper.RequestString("Search", required: true);
                                    if (query.Trim().ToLower() == "1")
                                    {
                                        finalResult = new List<Book>();
                                    }
                                    else
                                    {
                                        // search again
                                        finalResult = result.Search(query).ToList();
                                        Console.WriteLine("THIS IS THE SELECTION");
                                        // if there are no results we return that there are no results and let them try again
                                        if (finalResult.Count == 0)
                                        {
                                            Console.WriteLine("THERE WERE NO RESULTS FOUND FOR YOUR QUERY, TRY AGAIN");
                                            Console.ReadKey();
                                        }
                                    }

                                }
                                break;
                        }

                        // if there are no results, the user can search again
                        if (finalResult.Count == 0)
                        {
                            Console.WriteLine("THERE WERE NO RESULTS FOUND FOR YOUR QUERY, TRY AGAIN");
                            Console.ReadKey();

                        }

                        // if there is only one result, the user can update the book after confirmation
                        if (finalResult.Count == 1)
                        {
                            Console.WriteLine($"THIS IS THE ITEM THAT YOU WANT TO UPDATE:\n{finalResult[0].ToString()}");
                            Console.WriteLine("\nDO YOU WANT TO UPDATE THIS?: Y, (n)");
                            Console.WriteLine("Type exit to exit to the home screen");

                            // bool requester, with validation and default value
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
                            else
                            {
                                finalResult = new List<Book>();
                            }

                        }
                    }
                    while (finalResult.Count == 0);
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

        // search method to search on ID or on TITLE, PUBLISHER, AUTHOR, GENRE
        private void searchBook()
        {
            // clear the console, so the user can see clearly what is happening
            Console.Clear();
            Console.WriteLine("=== SEARCH ===\n");

            // exception catching
            try
            {
                // Declaring the result and finalResult for the search and the search till there is only one result
                // giving them a zero value because they are only initialized in the do while loop
                List<Book> result = new List<Book>();
                List<Book> finalResult = new List<Book>();
                var books = _repository.GetAll();
                    do
                    {
                    // clear the console, so the user can see clearly what is happening
                    Console.Clear();
                    // Giving the option to look for the book on ID or on TITLE, PUBLISHER, AUTHOR, GENRE
                    Console.WriteLine("=== DO YOU WANT TO LOOK FOR THE BOOK ON ===");
                    Console.WriteLine("1. TITLE, PUBLISHER, AUTHOR, GENRE (name)");
                    Console.WriteLine("2. ID (id)");
                    // Request a choice in the program with string validation
                    var choice = ConsoleHelper.RequestString("Choice", required: true);

                        // Request the search query
                        var query = ConsoleHelper.RequestString("Search", required: true);

                    // applying the choice of the user
                    switch (choice.ToLower())
                        {
                            // search a book on ID
                            case "2":
                            case var s when choice.Contains("id"):
                            case var g when choice.Contains("number"):
                                var id = Guid.Parse(query);
                            // the search method is used to find the book on ID, if the id is not found, the result will be a new book
                            // if the id is not valid it will throw an exception
                            result.Add(books.Search(id));
                            // make them the same, so the while loop will stop
                            finalResult = result;
                                if (finalResult.Count == 0)
                                {
                                    Console.WriteLine("THERE WERE NO RESULTS FOUND FOR YOUR QUERY, TRY AGAIN");
                                Console.ReadKey();

                            }
                            else
                                {
                                // use a foreach loop because it is a list
                                    foreach (var item in finalResult)
                                    {
                                    // show the results each time, which in this case is only 1 because an id is unique
                                    _logger.Log("Found Book:", item);
                                    }
                                }
                                break;

                            // searching a book on TITLE, PUBLISHER, AUTHOR, GENRE
                            default:
                            // search the books on the query, and make the result a list
                            result = _repository.GetAll().Search(query).ToList();
                                Console.WriteLine("\nRESULTS:\n\n");

                            // return each book using the logger
                                foreach (var item in result)
                                {
                                    finalResult.Add(item);
                                    _logger.Log("Found Book:", item);
                                }
                                break;
                            }
                            // if there are no results, the user can search again
                            if (result.Count == 0)
                            {
                                Console.WriteLine("THERE WERE NO RESULTS FOUND FOR YOUR QUERY, TRY AGAIN\n");
                        Console.ReadKey();

                    }
                    // if there are more than 1 result, the user can to keep searching or to stop
                    else
                            {
                                finalResult = result.Search(query).ToList();
                                Console.WriteLine("\nDID YOU FOUND WHAT YOU WERE LOOKING FOR?: Y, (n)");

                                // bool requester, with validation and default value 
                                var anwser = ConsoleHelper.RequestBoolean("Answer", false);
                                if (anwser)
                                {
                                    throw new OperationCanceledException();
                                }
                            }
                        
                    } while (finalResult.Count == 0);
                
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

        // show statistics: average price, total books, max price, min price, average page count
        private void ViewStatistics()
        {
            // get all books
            var books = _repository.GetAll();

            // get the statistics, using the method from the LINQ namespace
            var stats = books.GetBookStatistics();

            // clear the console, so the user can see clearly what is happening
            Console.Clear();
            Console.WriteLine("=== BOOK STATS ===\n");
            Console.WriteLine($"Average Price: {stats.averagePrice:C}");
            Console.WriteLine($"Total Books: {stats.totalBooks}");
            Console.WriteLine($"Max Price: {stats.maxPrice:C}");
            Console.WriteLine($"Min Price: {stats.minPrice:C}");
            Console.WriteLine($"Average Page Count: {books.GetAveragePageCount()} pages");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // group by genre
        private void GroupGenre()
        {
            // get all books
            var books = _repository.GetAll();

            // group the books by genre
            var GenreGrouped = books.GroupByGenre();

            // clear the console, so the user can see clearly what is happening
            Console.Clear();
            Console.WriteLine("=== GROUPED ===\n");
            Console.WriteLine("=== BOOKS BY GENRE ===\n");

            foreach (var group in GenreGrouped)
            {
                // show the genre
                Console.WriteLine($"GENRE: {group.Key}");

                // show the books in this genre
                foreach (var item in group)
                {
                    _logger.Log("Book:", item);
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // group by publication year
        private void GroupYear()
        {
            // get all books
            var books = _repository.GetAll();

            // group the books by publication year
            var YearGrouped = books.GroupByPublicationYear();

            // clear the console, so the user can see clearly what is happening
            Console.Clear();
            Console.WriteLine("=== GROUPED ===\n");
            Console.WriteLine("=== BOOKS BY YEAR ===\n");

            foreach (var group in YearGrouped)
            {
                // show the year
                Console.WriteLine($"YEAR: {group.Key}");

                // Show each book in the year
                foreach (var item in group)
                {
                    _logger.Log("Book:", item);
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

        }
    } 
}
