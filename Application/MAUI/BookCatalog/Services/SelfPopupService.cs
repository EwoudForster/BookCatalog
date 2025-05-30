using CommunityToolkit.Maui.Views;

namespace BookCatalog.Services
{
    public class SelfPopupService : ISelfPopupService
    {
        private readonly Page _mainPage;
        public SelfPopupService()
        {
            _mainPage = Application.Current?.MainPage;

        }

        public async Task ShowBottomPopupAsync(BookStore selectedBookStore)
        {
            if (_mainPage is null)
                return;
            var viewModel = new DetailBookStoreViewModel(selectedBookStore.Id);
            await viewModel.LoadBookStoreCommand.ExecuteAsync(null);
            await viewModel.LoadPicturesCommand.ExecuteAsync(null);

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                var popup = new DetailBookStorePopup(viewModel);
                await _mainPage.ShowPopupAsync(popup);
            });

        }

        public async Task ShowOrderPopup()
        {
            if (_mainPage is null)
                return;
            var viewModel = new OrderPlacedViewModel();
            var popup = new OrderPlacedPopup(viewModel);
            await _mainPage.ShowPopupAsync(popup);

        }

    }

}
