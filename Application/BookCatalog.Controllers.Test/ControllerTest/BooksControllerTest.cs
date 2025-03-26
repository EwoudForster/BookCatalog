using BookCatalog.Tests;
using BookCatalog.Views.ViewModels;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;


namespace BookCatalog.Controllers.Test
{
    public class BooksControllerTest
    {
        [Fact]
        public void ListReturn_GroupBy_listViewModel()
        {

            // arrange
            var mockBookRepository = RepositoryMocks.GetBookRepository();
            var booksController = new BooksController(mockBookRepository.Object);
            var result = booksController.List() as ViewResult;

            // act
            Assert.NotNull(result);
            Assert.IsType<ListViewModel>(result.Model);
            var listViewModel = result.Model as ListViewModel;


            // assert
            foreach (var book in listViewModel.Books)
            {
                if (book.Key == "Fantasy")
                {
                    Assert.Equal(3, book.ToList().Count());

                }
            }

        }        
        
        [Fact]
        public void DetailsReturn_GetById_DetailsViewModel()
        {

            // arrange
            var mockBookRepository = RepositoryMocks.GetBookRepository();
            var booksController = new BooksController(mockBookRepository.Object);
            var result = booksController.Details(RepositoryMocks.booksList[1].Id) as ViewResult;

            // act
            Assert.NotNull(result);
            Assert.IsType<DetailsViewModel>(result.Model);
            var listViewModel = result.Model as DetailsViewModel;


            // assert
            Assert.Equal(RepositoryMocks.booksList[1].Id, listViewModel.Book.Id);
            Assert.Equal("Harry Potter and the Philosopher's Stone", listViewModel.Book.Title);
            Assert.Equal("J.K. Rowling", listViewModel.Book.Author);
            Assert.Equal(1997, listViewModel.Book.PublicationYear);
            Assert.Equal("Fantasy", listViewModel.Book.Genre);
            Assert.Equal("9780747532743", listViewModel.Book.ISBN);
            Assert.Equal("Bloomsbury", listViewModel.Book.Publisher);
            Assert.Equal(223, listViewModel.Book.PageCount);
            Assert.Equal(9.99m, listViewModel.Book.Price);
            Assert.Equal(false, listViewModel.Book.IsAvailable);

        }
    }
}
