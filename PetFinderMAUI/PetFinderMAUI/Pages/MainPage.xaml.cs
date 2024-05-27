using Newtonsoft.Json;

namespace PetFinderMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetProfileInfo();
        }

        private void GetProfileInfo()
        {
            var userInfo =
                JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
            UserEmail.Text = "Welcome " + userInfo!.User.Email;
        }
    }
}