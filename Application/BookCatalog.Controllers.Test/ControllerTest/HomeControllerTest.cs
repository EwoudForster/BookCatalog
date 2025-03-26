using BookCatalog.Controllers;
using BookCatalog.Views.ViewModels;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Tests
{
    public class HomeControllerTests
    {

        [Fact]
        public void Index_ReturnsViewResult_WithHomeViewModel()
        {
            // arrange
            var mockBookRepository = RepositoryMocks.GetBookRepository();
            var homeController = new HomeController(mockBookRepository.Object);
            var result = homeController.Index() as ViewResult;

            // act
            Assert.NotNull(result);
            Assert.IsType<HomeViewModel>(result.Model); 

            var viewModel = result.Model as HomeViewModel;

            // assert
            Assert.Equal(3, viewModel.Books.Count()); 
            Assert.Equal(3, viewModel.TotalBooks); 
            Assert.Equal(19.99m, viewModel.Max);
            Assert.Equal(9.99m, viewModel.Min); 
            Assert.Equal(14.323333333333333333333333333m, viewModel.Average);
            Assert.Equal("Page(s)", viewModel.StatisticsType);
        }
    }
}
