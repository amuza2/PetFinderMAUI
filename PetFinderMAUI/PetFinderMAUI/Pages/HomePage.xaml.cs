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

    private async void OnFabClicked(object sender, EventArgs e)
    {
        // Navigate to the NewPostPage when the FloatingActionButton is clicked
        await Navigation.PushAsync(new NewPostPage());
    }
}