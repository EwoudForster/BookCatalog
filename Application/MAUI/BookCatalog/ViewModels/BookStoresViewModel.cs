using BookCatalog.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace BookCatalog.ViewModels
{
    public class BookStoresViewModel : BaseViewModel
    {
        private readonly IBookCatalogApiService _bookCatalog;
        private readonly ISelfPopupService _popupService;

        private BookStore? _selectedBookStore;
        public BookStore? SelectedBookStore
        {
            get => _selectedBookStore;
            set => SetProperty(ref _selectedBookStore, value);
        }

        private List<BookStore>? _bookStores;
        public List<BookStore>? BookStores
        {
            get => _bookStores;
            set => SetProperty(ref _bookStores, value);
        }

        public Map? Map { get; set; }

        public BookStoresViewModel(IBookCatalogApiService bookCatalog, ISelfPopupService popupService)
        {
            _bookCatalog = bookCatalog;
            _popupService = popupService;

            GetBookStoresCommand = new AsyncRelayCommand(AddBookStores);
            HereCommand = new AsyncRelayCommand(DoCurrentLocation);

        }

        public IAsyncRelayCommand GetBookStoresCommand { get; }
        public IAsyncRelayCommand HereCommand { get; }

        private bool _isPopupOpen = false;

        private async Task OpenPopup()
        {
            if (_isPopupOpen || SelectedBookStore == null)
                return;

            _isPopupOpen = true;
            try
            {
                await _popupService.ShowBottomPopupAsync(SelectedBookStore);
            }
            finally
            {
                _isPopupOpen = false;
            }
        }

        private async Task AddBookStores()
        {
            if (Map == null) return;

            BookStores = await _bookCatalog.GetAllAsync<BookStore>(Endpoints.BookStores);

            Map.MapElements.Clear();
            Map.Pins.Clear();

            if (BookStores == null || BookStores.Count == 0) return;

            var locations = BookStores.Select(bs => new Location(bs.Latitude, bs.Longitude)).ToList();
            var extent = new MyExtent(locations);

            foreach (var bookStore in BookStores)
            {
                Map.Pins.Add(await PinFromLocation(bookStore));
                extent.ReadLatLon(bookStore.Latitude, bookStore.Longitude, false);
            }

            ZoomToExtent(extent);
        }

        private async Task<Pin> PinFromLocation(BookStore bookStore)
        {
            var pin = new Pin
            {
                Label = string.IsNullOrWhiteSpace(bookStore.Name) ? "Unknown Bookstore" : bookStore.Name,
                Type = PinType.Place,
                Location = new Location(bookStore.Latitude, bookStore.Longitude)
            };

            pin.MarkerClicked += async (sender, e) =>
            {
                e.HideInfoWindow = true;
                SelectedBookStore = null;
                SelectedBookStore = await _bookCatalog.GetByIdAsync<BookStore>(bookStore.Id, Endpoints.BookStores);
                await OpenPopup();
            };

            return pin;
        }

        private void ZoomToExtent(MyExtent extent)
        {
            if (Map == null) return;

            var center = extent.GetCenter();
            var radius = extent.GetDistance() * 0.4;

            Map.MoveToRegion(MapSpan.FromCenterAndRadius(center, Distance.FromKilometers(radius)));
        }

        private void CenterToLatLon(string locationName, Location location)
        {
            if (Map == null) return;

            var locatedAddress = new BookStore
            {
                Name = locationName,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            var mapSpan = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(1.0));
            Map.MoveToRegion(mapSpan);
        }

        private async Task MapReset()
        {
            if (Map == null) return;

            Map.MapElements.Clear();
            Map.Pins.Clear();

            if (BookStores != null)
            {
                foreach (var bookStore in BookStores)
                {
                    Map.Pins.Add(await PinFromLocation(bookStore));
                }
            }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private CancellationTokenSource? cts;
        private bool _isCheckingLocation = false;

        private async Task DoCurrentLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    cts = new CancellationTokenSource();
                    _isCheckingLocation = true;
                    location = await Geolocation.GetLocationAsync(request, cts.Token);
                }

                if (location != null)
                {
                    await MapReset();
                    CenterToLatLon("Here", location);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
        }
    }
}
