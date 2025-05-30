using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL;
using Moq;
using Microsoft.Extensions.Logging;
using BookCatalog.Web.Pages.Publishers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Humanizer;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookCatalog.Tests.PageModelTests.PublisherRazorModelTests;

public class IndexPageTest
{

    Mock<IBookRepository> _mockBookRepository = RepositoryMocks.GetBookRepository();
    Mock<IGenreRepository> _mockGenreRepository = RepositoryMocks.GetGenreRepository();
    Mock<IPublisherRepository> _mockPublisherRepository = RepositoryMocks.GetPublisherRepository();
    Mock<IAuthorRepository> _mockAuthorRepository = RepositoryMocks.GetAuthorRepository();
    Mock<ILogger<IndexModel>> _logger = new();

    [Fact]
    public async Task Publishers_Index_Valid()
    {
        // arrange
        var pageModel = new IndexModel(_mockBookRepository.Object, _mockPublisherRepository.Object, _logger.Object);

        // needed for working with the razorpages viewdata, otherwise null reference error
        var mockPageContext = new Mock<PageContext>();
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

        pageModel.PageContext = mockPageContext.Object;

        // act
        var result = await pageModel.OnGet();
        var Publishers = pageModel.Publishers.ToList();
        var ActualPublishers = RepositoryMocks.MockBookList.Select(p => p.Publisher).ToList();
        // assert
        Assert.IsType<IndexModel>(pageModel);
        Assert.Equal(ActualPublishers.Count(), Publishers.Count());
        for (int i = 0; i < RepositoryMocks.MockBookList.Count(); i++)
        {
          
            Assert.Equal(ActualPublishers[i].Id, Publishers[i].Id);
            Assert.Equal(ActualPublishers[i].Name, Publishers[i].Name);
            Assert.Equal(3, Publishers[i].Average);
            Assert.Equal(9m, Publishers[i].Max);
            Assert.Equal(1m, Publishers[i].Min);
        }
    }
    
    [Fact]
    public async Task Publishers()
    {
        // arrange
        var pageModel = new IndexModel(_mockBookRepository.Object, _mockPublisherRepository.Object, _logger.Object);

        // needed for working with the razorpages viewdata, otherwise null reference error
        var mockPageContext = new Mock<PageContext>();
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

        pageModel.PageContext = mockPageContext.Object;       
        
        // we mock a database error
        _mockPublisherRepository.Setup(a => a.GetAllWithBookStats()).ThrowsAsync(new Exception("Database connection failed"));

        // act
        var result = await pageModel.OnGet();

        // assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, ((ObjectResult)result).StatusCode);
    }
}
