namespace ViewDemo.Services;

public class AlertService : IAlertService
{
    public Task<bool> ShowConfirmationAsync(string title, string message)
    {
        return Application.Current!.MainPage!.DisplayAlert(title, message, "ok", "no");
    }

    public Task ShowAlertAsync(string title, string message, string accept, string cancel)
    {
        return Application.Current!.MainPage!.DisplayAlert(title, message, accept, cancel);
    }

    public Task ShowAlertAsync(string title, string message, string accept)
    {
        return Application.Current!.MainPage!.DisplayAlert(title, message, accept);
    }
}
