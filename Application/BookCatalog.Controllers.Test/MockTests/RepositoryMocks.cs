using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookCatalog.Tests
{
    public static class RepositoryMocks
    {

        public static List<Book> booksList = new()
            {

                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "The Lord of the Rings",
                    Author = "J.R.R. Tolkien",
                    PublicationYear = 1954,
                    Genre = "Fantasy",
                    ISBN = "9780261102385",
                    Publisher = "Allen & Unwin",
                    PageCount = 1178,
                    Price = 19.99m,
                    IsAvailable = true
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Harry Potter and the Philosopher's Stone",
                    Author = "J.K. Rowling",
                    PublicationYear = 1997,
                    Genre = "Fantasy",
                    ISBN = "9780747532743",
                    Publisher = "Bloomsbury",
                    PageCount = 223,
                    Price = 9.99m,
                    IsAvailable = false
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    PublicationYear = 1937,
                    Genre = "Fantasy",
                    ISBN = "9780261102217",
                    Publisher = "Allen & Unwin",
                    PageCount = 310,
                    Price = 12.99m,
                    IsAvailable = true
                }
            };

        public static Mock<IBookRepository> GetBookRepository()
        {

            var mockBookRepository = new Mock<IBookRepository>();

            mockBookRepository.Setup(repo => repo.GetAll()).Returns(booksList);

            mockBookRepository.Setup(repo => repo.Update(It.IsAny<Book>())).Callback(() => {});

            mockBookRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((Guid id) => booksList.FirstOrDefault(book => book.Id == id));

            mockBookRepository.Setup(repo => repo.Delete(It.IsAny<Guid>())).Callback(() => {});

            mockBookRepository.Setup(repo => repo.GetAverage(It.IsAny<BookByStatsOptions>())).Returns(booksList.Average(book => book.Price));

            mockBookRepository.Setup(repo => repo.GetBookCountBy(It.IsAny<BookByOptions>())).Returns(booksList.Count);

            mockBookRepository.Setup(repo => repo.GetBookStatistics(It.IsAny<BookByStatsOptions>()))
                .Returns((BookByStatsOptions option) => (
                    booksList.Average(book => book.Price),
                    booksList.Count,
                    booksList.Max(book => book.Price),
                    booksList.Min(book => book.Price)
                ));

            mockBookRepository.Setup(repo => repo.GroupBy(It.IsAny<BookByOptions>()))
                .Returns(booksList.GroupBy(book => book.Genre));
            
            mockBookRepository.Setup(repo => repo.OrderBookBy(It.IsAny<BookByOptions>()))
                .Returns(booksList.OrderBy(book => book.PublicationYear));

            mockBookRepository.Setup(repo => repo.Search(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns((Guid query, string? genre) => booksList.FirstOrDefault(book => book.Id == query));

            mockBookRepository.Setup(repo => repo.Search(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string query, string? genre) => booksList.Where(book => book.Title.Contains(query, StringComparison.OrdinalIgnoreCase)));

            return mockBookRepository;
        }
    }
}
