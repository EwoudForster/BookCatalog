using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;
using Moq;

namespace BookCatalog.Tests.Repositories.Mock
{
    class RepositoryMocks
    {
        public static Mock<IRepository<Book>> GetBookRepository()
        {
            var books = new List<Book>
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
                    IsAvailable = true
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
            var mockBookRepository = new Mock<IRepository<Book>>();
            mockBookRepository.Setup(repo => repo.GetAll()).Returns(books);
            mockBookRepository.Setup(repo => repo.Update(It.IsAny<Book>())).Callback(() => Console.WriteLine("Successful"));
            mockBookRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(books[0]);
            mockBookRepository.Setup(repo => repo.Delete(It.IsAny<Guid>())).Callback(() => Console.WriteLine("Successful"));

            return mockBookRepository;
        }
    }
}
