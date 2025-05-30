using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using BookCatalog.Web.Pages;

namespace BookCatalog.Tests.PageModelTests;

public class IndexPageTest
{

    Mock<IBookRepository> _mockBookRepository = RepositoryMocks.GetBookRepository();
    Mock<IGenreRepository> _mockGenreRepository = RepositoryMocks.GetGenreRepository();
    Mock<IAuthorRepository> _mockAuthorRepository = RepositoryMocks.GetAuthorRepository();
    Mock<ILogger<IndexModel>> _logger = new();

    [Fact]
    public async Task Index_Valid()
    {
        // arrange
        var PageModel = new IndexModel(_mockBookRepository.Object, _mockGenreRepository.Object, _logger.Object);

        // needed for working with the razorpages viewdata, otherwise null reference error
        var MockPageContext = new Mock<PageContext>();
        var ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        MockPageContext.SetupGet(p => p.ViewData).Returns(ViewData);

        PageModel.PageContext = MockPageContext.Object;

        // act
        var ActualList = RepositoryMocks.MockBookList.OrderBy(e => e.PublicationYear).ToList();
        var Result = await PageModel.OnGet();
        var Books = PageModel.Books.ToList();
        var Carousel = PageModel.Carousel.ToList();
        var StatisticsPrice = PageModel.StatisticsPrice;

        AssertBooks(ActualList, Books);        
        Assert.Equal(RepositoryMocks.MockBookList.Count(), Carousel.Count());
        AssertBooks(ActualList, Carousel);

        Assert.Equal(5, StatisticsPrice.Count);
        Assert.Equal("€", StatisticsPrice.FirstOrDefault(e => e.Key == StatisticsOptions.StatisticsType).Value);
        Assert.Equal(14.323333333333333333333333333M, StatisticsPrice.FirstOrDefault(e => e.Key == StatisticsOptions.Average).Value);
        Assert.Equal(3, StatisticsPrice.FirstOrDefault(e => e.Key == StatisticsOptions.TotalBooks).Value);
        Assert.Equal(19.99M, StatisticsPrice.FirstOrDefault(e => e.Key == StatisticsOptions.Max).Value);
        Assert.Equal(9.99M, StatisticsPrice.FirstOrDefault(e => e.Key == StatisticsOptions.Min).Value);

        // assert
        Assert.IsType<PageResult>(Result);

    }
    
    [Fact]
    public async Task Index_InValid()
    {
        // arrange
        var pageModel = new IndexModel(_mockBookRepository.Object, _mockGenreRepository.Object, _logger.Object);

        // needed for working with the razorpages viewdata, otherwise null reference error
        var mockPageContext = new Mock<PageContext>();
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

        pageModel.PageContext = mockPageContext.Object;       
        
        // we mock a database error
        _mockBookRepository.Setup(a => a.OrderBookBy(It.IsAny<BookByOptions>(), It.IsAny<bool>())).ThrowsAsync(new Exception("Database connection failed"));

        // act
        var result = await pageModel.OnGet();

        // assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, ((ObjectResult)result).StatusCode);
    }

    [Fact]
    public async Task Index_EmptyBookRepo_RazorPage_NotValid_NotFoundException()
    {

        // arrange
        var pageModel = new IndexModel(_mockBookRepository.Object, _mockGenreRepository.Object, _logger.Object);

        _mockBookRepository.Setup(b => b.OrderBookBy(It.IsAny<BookByOptions>(), It.IsAny<bool>())).ReturnsAsync(new List<Book>());

        // needed for working with the razorpages viewdata, otherwise null reference error

        var mockPageContext = new Mock<PageContext>();
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
        mockPageContext.SetupGet(p => p.ViewData).Returns(viewData);

        pageModel.PageContext = mockPageContext.Object;


        // act
        var result = await pageModel.OnGet();

        // assert
        Assert.IsType<NotFoundResult>(result);
    }

    private void AssertBooks(List<Book> Actual, List<Book> booksPageModel)
    {
        for (int i = 0; i < Actual.Count; i++)
        {
            Assert.Equal(Actual[i].Id, booksPageModel[i].Id);
            Assert.Equal(Actual[i].Title, booksPageModel[i].Title);
            for (int j = 0; j < booksPageModel[i].Authors.Count(); j++)
            {
                Assert.Equal(Actual[i].Authors[j].Id, booksPageModel[i].Authors[j].Id);
                Assert.Equal(Actual[i].Authors[j].Name, booksPageModel[i].Authors[j].Name);

            }
            Assert.Equal(Actual[i].PublicationYear, booksPageModel[i].PublicationYear);

            for (int j = 0; j < booksPageModel[i].Genres.Count(); j++)
            {
                Assert.Equal(Actual[i].Genres[j].Id, booksPageModel[i].Genres[j].Id);
                Assert.Equal(Actual[i].Genres[j].Name, booksPageModel[i].Genres[j].Name);

            }
            Assert.Equal(Actual[i].Publisher.Id, booksPageModel[i].Publisher.Id);
            Assert.Equal(Actual[i].Publisher.Name, booksPageModel[i].Publisher.Name);
            Assert.Equal(Actual[i].ISBN, booksPageModel[i].ISBN);
            Assert.Equal(Actual[i].PageCount, booksPageModel[i].PageCount);
            Assert.Equal(Actual[i].Price, booksPageModel[i].Price);
            Assert.Equal(Actual[i].IsAvailable, booksPageModel[i].IsAvailable);
        }
    }
}
