using PetFinderMAUI.Utils;

namespace PetFinderMAUI.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();

        UserEmail.Text = "Welcome " + GlobalHelper.GetUserEmail();
    }
}