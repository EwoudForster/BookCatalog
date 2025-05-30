using BookCatalog.DAL;
using BookCatalog.DAL.Repositories;
using BookCatalog.Tests;
using BookCatalog.Web.Pages.Authors;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookCatalog.Tests.PageModelTests.AuthorRazorModelTests
{
    public class DetailsPageTests
    {

        Mock<IBookRepository> _mockBookRepository = RepositoryMocks.GetBookRepository();
        Mock<IGenreRepository> _mockGenreRepository = RepositoryMocks.GetGenreRepository();
        Mock<IAuthorRepository> _mockAuthorRepository = RepositoryMocks.GetAuthorRepository();
        Mock<ILogger<DetailsModel>> _logger = new();


        [Fact]
        public async Task Authors_Details_RazorPage_Valid()
        {

            // arrange
            var pageModel = new DetailsModel(_mockBookRepository.Object, _mockAuthorRepository.Object, _logger.Object);

            // needed for working with the razorpages viewdata, otherwise null reference error

            var mockPageContext = new Mock<PageContext>();
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

            pageModel.PageContext = mockPageContext.Object;


            // act
            var result = await pageModel.OnGet(RepositoryMocks.MockBookList[1].Authors[0].Id);
            var author = pageModel.Author;

            // assert
            Assert.NotNull(result);
            Assert.IsType<DetailsModel>(pageModel);
            Assert.NotNull(author);
            Assert.IsType<Author>(author);
            Assert.Equal(RepositoryMocks.MockBookList[1].Authors[0].Id, author.Id);
            Assert.Equal(RepositoryMocks.MockBookList[1].Authors[0].Name, author.Name);
        }
        
        [Fact]
        public async Task Authors_Details_RazorPage_NotValid_NotFoundException()
        {

            // arrange
            var pageModel = new DetailsModel(_mockBookRepository.Object, _mockAuthorRepository.Object, _logger.Object);

            // needed for working with the razorpages viewdata, otherwise null reference error

            var mockPageContext = new Mock<PageContext>();
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

            pageModel.PageContext = mockPageContext.Object;


            // act
            var result = await pageModel.OnGet(Guid.NewGuid());

            // assert
            Assert.IsType<NotFoundResult>(result);
        } 
        
        [Fact]
        public async Task Authors_Details_RazorPage_NotValid_GeneralException()
        {

            // arrange
            var pageModel = new DetailsModel(_mockBookRepository.Object, _mockAuthorRepository.Object, _mockMapper.Object, _logger.Object);

            // we mock a database error
            _mockAuthorRepository.Setup(r => r.GetById(It.IsAny<Guid>())).ThrowsAsync(new Exception("Database connection failed"));

            // needed for working with the razorpages viewdata, otherwise null reference error

            var mockPageContext = new Mock<PageContext>();
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

            pageModel.PageContext = mockPageContext.Object;


            // act
            var result = await pageModel.OnGet(Guid.NewGuid());

            // assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}
