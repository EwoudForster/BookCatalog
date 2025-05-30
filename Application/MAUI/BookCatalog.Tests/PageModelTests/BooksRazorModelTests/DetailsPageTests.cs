using BookCatalog.DAL;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.Tests;
using BookCatalog.Web.Pages.Books;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookCatalog.Tests.PageModelTests.BooksRazorModelTests
{
    public class DetailsPageTests
    {

        Mock<IBookRepository> _mockBookRepository = RepositoryMocks.GetBookRepository();
        Mock<IGenreRepository> _mockGenreRepository = RepositoryMocks.GetGenreRepository();
        Mock<IAuthorRepository> _mockAuthorRepository = RepositoryMocks.GetAuthorRepository();
        Mock<ILogger<DetailsModel>> _logger = new();


        [Fact]
        public async Task Books_Details_RazorPage_Valid()
        {

            // arrange
            var PageModel = new DetailsModel(_mockBookRepository.Object, _mockGenreRepository.Object, _logger.Object);

            // needed for working with the razorpages viewdata, otherwise null reference error

            var MockPageContext = new Mock<PageContext>();
            var ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            MockPageContext.SetupGet(p => p.ViewData).Returns(ViewData);

            PageModel.PageContext = MockPageContext.Object;


            // act
            var result = await PageModel.OnGet(RepositoryMocks.MockBookList[1].Id);
            var Book = PageModel.Book;

            // assert
            Assert.NotNull(result);
            Assert.IsType<DetailsModel>(PageModel);
            Assert.NotNull(Book);
            Assert.IsType<Book>(Book);
            Assert.Equal(RepositoryMocks.MockBookList[1].Id, Book.Id);
            Assert.Equal(RepositoryMocks.MockBookList[1].Title, Book.Title);
            for (int j = 0; j < Book.Authors.Count(); j++)
            {
                Assert.Equal(RepositoryMocks.MockBookList[1].Authors[j].Id, Book.Authors[j].Id);
                Assert.Equal(RepositoryMocks.MockBookList[1].Authors[j].Name, Book.Authors[j].Name);

            }
            Assert.Equal(RepositoryMocks.MockBookList[1].PublicationYear, Book.PublicationYear);

            for (int j = 0; j < Book.Genres.Count(); j++)
            {
                Assert.Equal(RepositoryMocks.MockBookList[1].Genres[j].Id, Book.Genres[j].Id);
                Assert.Equal(RepositoryMocks.MockBookList[1].Genres[j].Name, Book.Genres[j].Name);

            }
            Assert.Equal(RepositoryMocks.MockBookList[1].Publisher.Id, Book.Publisher.Id);
            Assert.Equal(RepositoryMocks.MockBookList[1].Publisher.Name, Book.Publisher.Name);
            Assert.Equal(RepositoryMocks.MockBookList[1].ISBN, Book.ISBN);
            Assert.Equal(RepositoryMocks.MockBookList[1].PageCount, Book.PageCount);
            Assert.Equal(RepositoryMocks.MockBookList[1].Price, Book.Price);
            Assert.Equal(RepositoryMocks.MockBookList[1].IsAvailable, Book.IsAvailable);
        }
        
        [Fact]
        public async Task Books_Details_RazorPage_NotValid_NotFoundException()
        {

            // arrange
            var PageModel = new DetailsModel(_mockBookRepository.Object, _mockGenreRepository.Object, _logger.Object);

            // needed for working with the razorpages viewdata, otherwise null reference error

            var MockPageContext = new Mock<PageContext>();
            var ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            MockPageContext.SetupGet(p => p.ViewData).Returns(ViewData);

            PageModel.PageContext = MockPageContext.Object;


            // act
            var result = await PageModel.OnGet(Guid.NewGuid());

            // assert
            Assert.IsType<NotFoundResult>(result);
        } 
        
        [Fact]
        public async Task Books_Details_RazorPage_NotValid_GeneralException()
        {

            // arrange
            var PageModel = new DetailsModel(_mockBookRepository.Object, _mockGenreRepository.Object, _logger.Object);

            // we mock a database error
            _mockBookRepository.Setup(r => r.GetById(It.IsAny<Guid>())).ThrowsAsync(new Exception("Database connection failed"));

            // needed for working with the razorpages viewdata, otherwise null reference error

            var MockPageContext = new Mock<PageContext>();
            var ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            MockPageContext.SetupGet(p => p.ViewData).Returns(ViewData);

            PageModel.PageContext = MockPageContext.Object;


            // act
            var Result = await PageModel.OnGet(Guid.NewGuid());

            // assert
            Assert.IsType<ObjectResult>(Result);
            Assert.Equal(500, ((ObjectResult)Result).StatusCode);
        }
    }
}
