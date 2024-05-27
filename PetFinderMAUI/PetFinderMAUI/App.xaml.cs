using PetFinderMAUI.Pages;

namespace PetFinderMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Check if the user is logged in
            bool isLoggedIn = Preferences.Get("IsLoggedIn", false);

            // If the user is logged in, navigate to the main page
            MainPage = isLoggedIn
                ? new NavigationPage(new AppShell())
                :
                // If the user is not logged in, navigate to the login page
                new NavigationPage(new WelcomePage());
        }
    }
}