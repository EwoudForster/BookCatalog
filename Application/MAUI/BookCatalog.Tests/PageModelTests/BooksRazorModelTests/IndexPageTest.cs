using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL;
using Moq;
using Microsoft.Extensions.Logging;
using BookCatalog.Web.Pages.Books;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Tests.PageModelTests.BooksRazorModelTests;

public class IndexPageTest
{

    Mock<IBookRepository> _mockBookRepository = RepositoryMocks.GetBookRepository();
    Mock<IGenreRepository> _mockGenreRepository = RepositoryMocks.GetGenreRepository();
    Mock<IAuthorRepository> _mockAuthorRepository = RepositoryMocks.GetAuthorRepository();
    Mock<ILogger<IndexModel>> _logger = new();

    [Fact]
    public async Task Books_Index_Valid()
    {
        // arrange
        var pageModel = new IndexModel(_mockBookRepository.Object, _mockGenreRepository.Object, _logger.Object);

        // needed for working with the razorpages viewdata, otherwise null reference error
        var mockPageContext = new Mock<PageContext>();
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

        pageModel.PageContext = mockPageContext.Object;

        // act
        var result = await pageModel.OnGet();
        var Genres = pageModel.Genres.ToList();

        // assert
        Assert.IsType<IndexModel>(pageModel);
        for (int i = 0; i < RepositoryMocks.MockBookList.Count(); i++)
        {
            for (int j = 0; j < RepositoryMocks.MockBookList[i].Authors.Count(); j++)
            {
                Assert.Equal(RepositoryMocks.MockBookList[i].Genres[j].Id, Genres[i].Id);
                Assert.Equal(RepositoryMocks.MockBookList[i].Genres[j].Name, Genres[i].Name);
            }
        }
    }
    
    [Fact]
    public async Task Books_Index_InValid()
    {
        // arrange
        var pageModel = new IndexModel(_mockBookRepository.Object, _mockGenreRepository.Object, _logger.Object);

        // needed for working with the razorpages viewdata, otherwise null reference error
        var mockPageContext = new Mock<PageContext>();
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

        pageModel.PageContext = mockPageContext.Object;       
        
        // we mock a database error
        _mockGenreRepository.Setup(a => a.GetAll()).ThrowsAsync(new Exception("Database connection failed"));

        // act
        var result = await pageModel.OnGet();

        // assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, ((ObjectResult)result).StatusCode);
    }
}
