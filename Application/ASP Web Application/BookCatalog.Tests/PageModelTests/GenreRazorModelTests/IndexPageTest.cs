using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL;
using Moq;
using Microsoft.Extensions.Logging;
using BookCatalog.Web.Pages.Genres;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Tests.PageModelTests.GenreRazorModelTests;

public class IndexPageTest
{

    Mock<IBookRepository> _mockBookRepository = RepositoryMocks.GetBookRepository();
    Mock<IGenreRepository> _mockGenreRepository = RepositoryMocks.GetGenreRepository();
    Mock<IAuthorRepository> _mockAuthorRepository = RepositoryMocks.GetAuthorRepository();
    Mock<ILogger<IndexModel>> _logger = new();

    [Fact]
    public async Task Genres_Index_Valid()
    {
        // Arrange
        var pageModel = new IndexModel(_mockBookRepository.Object, _mockGenreRepository.Object, _logger.Object);

        var mockPageContext = new Mock<PageContext>();
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);
        pageModel.PageContext = mockPageContext.Object;

        // Act
        var result = await pageModel.OnGet();
        var Genres = pageModel.Genres.ToList();

        // Assert
        Assert.IsType<IndexModel>(pageModel);

        var ExpectedGenres = RepositoryMocks.MockBookList;

        Assert.Equal(ExpectedGenres.Count, Genres.Count);

        // assert
        Assert.IsType<IndexModel>(pageModel);
        for (int i = 0; i < ExpectedGenres.Count(); i++)
        {
            for (int j = 0; j < ExpectedGenres[i].Genres.Count(); j++)
            {
                Assert.Equal(ExpectedGenres[i].Genres[j].Id, Genres[i].Id);
                Assert.Equal(ExpectedGenres[i].Genres[j].Name, Genres[i].Name);
                Assert.Equal(3m, Genres[i].Average);
                Assert.Equal(9m, Genres[i].Max);
                Assert.Equal(1m, Genres[i].Min);

            }
        }
    }


    [Fact]
    public async Task Genres_Index_InValid()
    {
        // arrange
        var pageModel = new IndexModel(_mockBookRepository.Object, _mockGenreRepository.Object, _logger.Object);

        // needed for working with the razorpages viewdata, otherwise null reference error
        var mockPageContext = new Mock<PageContext>();
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

        pageModel.PageContext = mockPageContext.Object;       
        
        // we mock a database error
        _mockGenreRepository.Setup(a => a.GetAllWithBookStats()).ThrowsAsync(new Exception("Database connection failed"));

        // act
        var result = await pageModel.OnGet();

        // assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, ((ObjectResult)result).StatusCode);
    }
}
