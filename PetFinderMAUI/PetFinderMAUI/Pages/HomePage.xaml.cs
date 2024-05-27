using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Newtonsoft.Json;
using PetFinderMAUI.Pages;

namespace PetFinderMAUI
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            GetProfileInfo();

            Toast.Make("Let's go!", ToastDuration.Short, 16);
        }

        private void GetProfileInfo()
        {
            var userInfo =
                JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
            UserEmail.Text = "Welcome " + userInfo!.User.Email;
        }
    }
}