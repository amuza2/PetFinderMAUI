using PetFinderMAUI.Utils;
using PetFinderMAUI.ViewModels;

namespace PetFinderMAUI.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        BindingContext = new PetViewModel();

        // UserEmail.Text = "Welcome " + GlobalHelper.GetUserEmail();
    }
}