using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFinderMAUI.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Clear the user's credentials from secure storage
        SecureStorage.Remove("username");
        SecureStorage.Remove("password");

        // Update the user's login status
        Preferences.Set("IsLoggedIn", false);

        // Navigate to the login page
        // await Navigation.PushAsync(new LoginPage());
        // Set the HomePage of the application to a new instance of LoginPage
        Application.Current!.MainPage = new WelcomePage();
    }
}