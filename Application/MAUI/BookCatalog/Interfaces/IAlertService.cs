namespace BookCatalog.Interfaces;

public interface IAlertService
{
    Task<bool> ShowConfirmationAsync(string title, string message);
    Task ShowAlertAsync(string title, string message, string accept, string cancel);
    Task ShowAlertAsync(string title, string message, string accept);
}
