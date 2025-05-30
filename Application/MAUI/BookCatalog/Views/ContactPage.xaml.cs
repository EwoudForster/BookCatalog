namespace BookCatalog.Views;

public partial class ContactPage : ContentPage
{
	public ContactPage(ContactViewModel contactViewModel)
	{
        InitializeComponent();
		BindingContext = contactViewModel;
    }
}