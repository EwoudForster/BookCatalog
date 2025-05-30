using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;



namespace BookCatalog.ViewModels;
public class ContactViewModel : BaseViewModel
{

    private string _name;

    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }

    private string _email;
    public string Email
    {
        get { return _email; }
        set { SetProperty(ref _email, value); }
    }

    private string _message;
    public string Message
    {
        get { return _message; }
        set { SetProperty(ref _message, value); }
    }
    private string _statusMessage;
    public string StatusMessage
    {
        get { return _statusMessage; }
        set { SetProperty(ref _statusMessage, value); }
    }
    private bool _isStatusVisible;
    public bool IsStatusVisible
    {
        get { return _isStatusVisible; }
        set { SetProperty(ref _isStatusVisible, value); }
    }

    private async Task SendAsync()
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Message))
        {
            StatusMessage = "Please fill out all fields.";
            IsStatusVisible = true;
            return;
        }

        // Simulate sending a message (replace with real API call)
        await Task.Delay(1000);

        StatusMessage = "Message sent! We'll get back to you soon.";
        IsStatusVisible = true;

        // Clear the form
        Name = Email = Message = string.Empty;
    }

    private readonly IBookCatalogApiService _bookCatalogApiService;
    public IAsyncRelayCommand SendCommand { get; private set; }
    public ContactViewModel(IBookCatalogApiService bookCatalogApiService)
    {
        _bookCatalogApiService = bookCatalogApiService;
        SendCommand = new AsyncRelayCommand(SendAsync);
    }
}
