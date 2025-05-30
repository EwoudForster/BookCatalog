using BookCatalog.DAL.Repositories;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using BookCatalog.Web.Pages.Authors;
using BookCatalog.DAL;

namespace BookCatalog.Tests.PageModelTests.AuthorRazorModelTests;

public class IndexPageTest
{

    Mock<IBookRepository> _mockBookRepository = RepositoryMocks.GetBookRepository();
    Mock<IGenreRepository> _mockGenreRepository = RepositoryMocks.GetGenreRepository();
    Mock<IAuthorRepository> _mockAuthorRepository = RepositoryMocks.GetAuthorRepository();
    Mock<ILogger<IndexModel>> _logger = new();

    [Fact]
    public async Task Authors_Index_Valid()
    {
        // arrange
        var pageModel = new IndexModel(_mockBookRepository.Object, _mockGenreRepository.Object, _mockAuthorRepository.Object, _logger.Object);

        // needed for working with the razorpages viewdata, otherwise null reference error
        var mockPageContext = new Mock<PageContext>();
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

        pageModel.PageContext = mockPageContext.Object;

        // act
        var result = await pageModel.OnGet();
        var Authors = pageModel.Authors.ToList();
        var ExpectedAuthors = RepositoryMocks.MockBookList;
        Assert.Equal(Authors.Count, Authors.Count);

        // assert
        Assert.IsType<IndexModel>(pageModel);
        for (int i = 0; i < ExpectedAuthors.Count(); i++)
        {
            for (int j = 0; j < ExpectedAuthors[i].Authors.Count(); j++)
            {
                Assert.Equal(ExpectedAuthors[i].Authors[j].Id, Authors[i].Id);
                Assert.Equal(ExpectedAuthors[i].Authors[j].Name, Authors[i].Name);
                Assert.Equal(3m, Authors[i].Average);
                Assert.Equal(9m, Authors[i].Max);
                Assert.Equal(1m, Authors[i].Min);

            }
        }
    }
    
    [Fact]
    public async Task Authors_Index_InValid()
    {
        // arrange
        var pageModel = new IndexModel(_mockBookRepository.Object, _mockGenreRepository.Object, _mockAuthorRepository.Object, _logger.Object);

        // needed for working with the razorpages viewdata, otherwise null reference error
        var mockPageContext = new Mock<PageContext>();
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

        pageModel.PageContext = mockPageContext.Object;       
        
        // we mock a database error
        _mockAuthorRepository.Setup(a => a.GetAllWithBookStats()).ThrowsAsync(new Exception("Database connection failed"));

        // act
        var result = await pageModel.OnGet();

        // assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, ((ObjectResult)result).StatusCode);
    }
}
