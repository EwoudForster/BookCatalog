using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalog.DAL;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Models.CalculatedValueModel;

namespace BookCatalog.Tests
{
    public static class RepositoryMocks
    {
        public static readonly List<Book> MockBookList = new()
     {
         new()
         {
             Id = Guid.NewGuid(),
             Title = "The Lord of the Rings",
             Authors = new List<Author>{ new Author { Id = Guid.Parse("5ee34374-4341-4f08-81cb-224776f0c40e"), Name = "J.R.R. Tolkien" } },
             PublicationYear = 1954,
             Genres =new List<Genre>{ new Genre { Id = Guid.Parse("c2943a19-3980-401f-adbe-e9f6721c5ff2"), Name = "Fantasy" } },
             ISBN = "9780261102385",
             PageCount = 1178 ,
             Publisher = new Publisher{Id = Guid.Parse("33fc1964-0f77-44dc-9262-dc3e2f450e53"), Name = "Allen & Unwin" } ,
             PublisherId = Guid.Parse("33fc1964-0f77-44dc-9262-dc3e2f450e53"),
             Price = 19.99m,
             IsAvailable = true
         },
         new()
         {
             Id = Guid.NewGuid(),
             Title = "Harry Potter and the Philosopher's Stone",
             Authors = new List<Author>{ new Author { Id = Guid.Parse("5f209538-474e-4561-9251-11e0353238fb"), Name = "J.K. Rowling" } },
             PublicationYear = 1997,
             Genres = new List<Genre>{ new Genre {Id = Guid.Parse("18e9c11a-2b2c-4393-a9a7-72b1bb26ce6a"), Name = "Fantasy" } },
             ISBN = "9780747532743",
             PageCount = 223,
             Publisher = new Publisher {Id = Guid.Parse("d5da0354-1000-4609-bcb6-57d54821cb81"), Name = "Bloomsbury" } ,
             Price = 9.99m,
             PublisherId = Guid.Parse("d5da0354-1000-4609-bcb6-57d54821cb81"),
             IsAvailable = false
         },
         new()
         {
             Id = Guid.NewGuid(),
             Title = "The Hobbit",
             Authors = new List<Author>{ new Author {Id = Guid.Parse("7191e9ea-a6f8-4c1b-9082-07ad77873e21"),Name = "J.R.R. Tolkien" } },
             PublicationYear = 1937,
             Genres = new List<Genre>{ new Genre {Id= Guid.Parse("1edc79f3-1052-4986-abdd-dae1bfe7c20e"), Name = "Fantasy" } },
             ISBN = "9780261102217",
             PageCount = 310,
             PublisherId = Guid.Parse("1edc79f3-1052-4986-abdd-dae1bfa7c20e"),
             Publisher = new Publisher {Id=Guid.Parse("1edc79f3-1052-4986-abdd-dae1bfa7c20e"), Name = "Allen & Unwin" } ,
             Price = 12.99m,
             IsAvailable = true
         }
     };
       

        // Mocking IBookRepository
        public static Mock<IBookRepository> GetBookRepository()
        {
            var mockBookRepository = new Mock<IBookRepository>();

            // Mock GetAll method
            mockBookRepository.Setup(repo => repo.GetAll()).ReturnsAsync(MockBookList);

            // Mock GetById method
            mockBookRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => MockBookList.FirstOrDefault(book => book.Id == id));

            // Mock Add method
            mockBookRepository.Setup(repo => repo.Add(It.IsAny<Book>()))
                .Returns(Task.CompletedTask);  // Simulate an async add, just returns completed task

            // Mock Update method
            mockBookRepository.Setup(repo => repo.Update(It.IsAny<Book>()))
                .Returns(Task.CompletedTask);  // Simulate an async update, just returns completed task

            // Mock Delete method
            mockBookRepository.Setup(repo => repo.Delete(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);  // Simulate an async delete, just returns completed task

            // Mock GetBookStatistics method
            mockBookRepository.Setup(repo => repo.GetBookStatistics(It.IsAny<BookByStatsOptions>()))
                .ReturnsAsync((BookByStatsOptions option) =>
                    new Dictionary<StatisticsOptions, object>()
                    {
                { StatisticsOptions.StatisticsType, "€" },
                { StatisticsOptions.Average, MockBookList.Average(book => book.Price) },
                { StatisticsOptions.TotalBooks, MockBookList.Count },
                { StatisticsOptions.Max, MockBookList.Max(book => book.Price) },
                { StatisticsOptions.Min, MockBookList.Min(book => book.Price) }
                    });

            // Mock GroupBy method
            mockBookRepository.Setup(repo => repo.GroupBy(It.IsAny<BookByOptions>()))
                .ReturnsAsync(MockBookList.GroupBy(book => book.Genres));

            // Mock OrderBookBy method
            mockBookRepository.Setup(repo => repo.OrderBookBy(It.IsAny<BookByOptions>(), It.IsAny<bool>()))
                .ReturnsAsync((BookByOptions option, bool desc) =>
                    MockBookList.OrderBy(book => book.PublicationYear));

            // Mock Search method
            mockBookRepository.Setup(repo => repo.Search(It.IsAny<string>(), It.IsAny<BookByOptions>(), It.IsAny<bool>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool?>()))
                .ReturnsAsync((string query, BookByOptions Orderby, bool desc, string? Publisher, string? Author, string? Genre, bool? IsAvailable) =>
                    MockBookList.Where(book => book.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));

            return mockBookRepository;
        }

        // Mocking IGenreRepository
        public static Mock<IGenreRepository> GetGenreRepository()
        {
            var mockGenreRepository = new Mock<IGenreRepository>();
            // Mock GetAll method
            mockGenreRepository.Setup(repo => repo.GetAll()).ReturnsAsync(MockBookList.SelectMany(book => book.Genres).Distinct());
            mockGenreRepository
                .Setup(repo => repo.GetAllWithBookStats())
                .ReturnsAsync(
                    MockBookList
                        .SelectMany(book => book.Genres)
                        .GroupBy(genre => genre.Id)
                        .Select(group =>
                        {
                            var books = MockBookList.Where(book => book.Genres.Any(a => a.Id == group.Key)).ToList();
                            return new GenreCalculated
                            {
                                Id = group.Key,
                                Name = group.First().Name,
                                Average = books.Any() ? 3m : 0,
                                Max = books.Any() ? 9m : 0,
                                Min = books.Any() ? 1m : 0
                            };
                        })
                );            // Mock GetById method
            mockGenreRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => MockBookList.SelectMany(book => book.Genres).FirstOrDefault(genre => genre.Id == id));

            mockGenreRepository.Setup(repo => repo.GetStatisticsForBooksPerGenre(It.IsAny<Guid>(), It.IsAny<BookByStatsOptions>()))
                .ReturnsAsync((Guid id, BookByStatsOptions option) =>
                    new Dictionary<StatisticsOptions, object>()
                    {
                { StatisticsOptions.StatisticsType, "€" },
                { StatisticsOptions.Average, 3m },
                { StatisticsOptions.TotalBooks, 5 },
                { StatisticsOptions.Max, 1m},
                { StatisticsOptions.Min, 9m }
            });
            return mockGenreRepository;
        }

        // Mocking IAuthorRepository
        public static Mock<IAuthorRepository> GetAuthorRepository()
        {
            var mockAuthorRepository = new Mock<IAuthorRepository>();
            // Mock GetAll method
            mockAuthorRepository.Setup(repo => repo.GetAll()).ReturnsAsync(MockBookList.SelectMany(a => a.Authors));
            // Mock GetById method
            mockAuthorRepository
                .Setup(repo => repo.GetAllWithBookStats())
                .ReturnsAsync(
                    MockBookList
                        .SelectMany(book => book.Authors)
                        .GroupBy(author => author.Id)
                        .Select(group =>
                        {
                            var books = MockBookList.Where(book => book.Authors.Any(a => a.Id == group.Key)).ToList();
                            return new AuthorCalculated
                            {
                                Id = group.Key,
                                Name = group.First().Name,
                                Average = books.Any() ? 3m : 0,
                                Max = books.Any() ? 9m : 0,
                                Min = books.Any() ? 1m : 0
                            };
                        })
                );

            mockAuthorRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((Guid id) => MockBookList.SelectMany(a => a.Authors).FirstOrDefault(book => book.Id == id));
            mockAuthorRepository.Setup(repo => repo.GetStatisticsForBooksPerAuthor(It.IsAny<Guid>(), It.IsAny<BookByStatsOptions>()))
               .ReturnsAsync((Guid id, BookByStatsOptions option) =>
                   new Dictionary<StatisticsOptions, object>()
                   {
                { StatisticsOptions.StatisticsType, "€" },
                { StatisticsOptions.Average, 3m },
                { StatisticsOptions.TotalBooks, 5 },
                { StatisticsOptions.Max, 1m},
                { StatisticsOptions.Min, 9m }
           });
            return mockAuthorRepository;
            }

        public static Mock<IPublisherRepository> GetPublisherRepository()
        {
            var mockPublisherRepository = new Mock<IPublisherRepository>();
            mockPublisherRepository.Setup(repo => repo.GetAll()).ReturnsAsync(MockBookList.Select(a => a.Publisher));
            mockPublisherRepository
                .Setup(repo => repo.GetAllWithBookStats())
                .ReturnsAsync(
                    MockBookList
                        .Select(book => book.Publisher)
                        .GroupBy(publisher => publisher.Id)
                        .Select(group =>
                        {
                            var books = MockBookList.Where(book => book.Publisher.Id == group.Key).ToList();
                            return new PublisherCalculated
                            {
                                Id = group.Key,
                                Name = group.First().Name,
                                Average = books.Any() ? 3m : 0,
                                Max = books.Any() ? 9m : 0,
                                Min = books.Any() ? 1m : 0
                            };
                        })
                );            // Mock GetById method
            // Mock GetById method
            mockPublisherRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => MockBookList.Select(book => book.Publisher).FirstOrDefault(book => book.Id == id));
            mockPublisherRepository.Setup(repo => repo.GetStatisticsForBooksPerPublisher(It.IsAny<Guid>(), It.IsAny<BookByStatsOptions>()))
               .ReturnsAsync((Guid id, BookByStatsOptions option) =>
                   new Dictionary<StatisticsOptions, object>()
                   {
                { StatisticsOptions.StatisticsType, "€" },
                { StatisticsOptions.Average, 3m },
                { StatisticsOptions.TotalBooks, 5 },
                { StatisticsOptions.Max, 9m},
                { StatisticsOptions.Min, 1m }
           });
            return mockPublisherRepository;

        }
    }
}
