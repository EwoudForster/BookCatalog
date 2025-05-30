using BookCatalog.DAL;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.Tests;
using BookCatalog.Web.Pages.Publishers;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookCatalog.Tests.PageModelTests.PublisherRazorModelTests
{
    public class DetailsPageTests
    {

        Mock<IBookRepository> _mockBookRepository = RepositoryMocks.GetBookRepository();
        Mock<IPublisherRepository> _mockPublisherRepository = RepositoryMocks.GetPublisherRepository();
        Mock<IAuthorRepository> _mockAuthorRepository = RepositoryMocks.GetAuthorRepository();
        Mock<ILogger<DetailsModel>> _logger = new();


        [Fact]
        public async Task Publishers_Details_RazorPage_Valid()
        {

            // arrange
            var pageModel = new DetailsModel(_mockBookRepository.Object, _mockPublisherRepository.Object, _logger.Object);

            // needed for working with the razorpages viewdata, otherwise null reference error

            var mockPageContext = new Mock<PageContext>();
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

            pageModel.PageContext = mockPageContext.Object;


            // act
            var result = await pageModel.OnGet(Guid.Parse("33fc1964-0f77-44dc-9262-dc3e2f450e53"));
            var publisher = pageModel.Publisher;

            // assert
            Assert.NotNull(result);
            Assert.IsType<PageResult>(result);
            Assert.NotNull(publisher);
            Assert.IsType<Publisher>(publisher);
            Assert.Equal(RepositoryMocks.MockBookList[0].Publisher.Id, publisher.Id);
            Assert.Equal(RepositoryMocks.MockBookList[0].Publisher.Name, publisher.Name);
        }
        
        [Fact]
        public async Task Publishers_Details_RazorPage_NotValid_NotFoundException()
        {

            // arrange
            var pageModel = new DetailsModel(_mockBookRepository.Object, _mockPublisherRepository.Object, _logger.Object);

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
        public async Task Publishers_Details_RazorPage_NotValid_GeneralException()
        {

            // arrange
            var pageModel = new DetailsModel(_mockBookRepository.Object, _mockPublisherRepository.Object, _logger.Object);

            // we mock a database error
            _mockPublisherRepository.Setup(r => r.GetById(It.IsAny<Guid>())).ThrowsAsync(new Exception("Database connection failed"));

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
