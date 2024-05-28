using System.ComponentModel;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using PetFinderMAUI.Utils;

namespace PetFinderMAUI.ViewModels;

internal class RegisterViewModel : INotifyPropertyChanged
{
    private static readonly FirebaseClient _firebaseClient = new("https://petcare-1d322-default-rtdb.firebaseio.com/");

    private readonly INavigation _navigation;

    private readonly string webApiKey = "AIzaSyDHyJAIn5P56uvAmFcSoq7M_MsP_azTmn0";
    private string _signUpEmail;
    private string _signUpPassword;

    public RegisterViewModel(INavigation navigation)
    {
        _navigation = navigation;
        SignUpBtn = new Command(RegisterUserTappedAsync);
    }

    public string SignUpEmail
    {
        get => _signUpEmail;
        set
        {
            _signUpEmail = value;
            RaisePropertyChanged("SignUpEmail");
        }
    }

    public string SignUpPassword
    {
        get => _signUpPassword;
        set
        {
            _signUpPassword = value;
            RaisePropertyChanged("SignUpPassword");
        }
    }

    public Command SignUpBtn { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }

    private async void RegisterUserTappedAsync(object obj)
    {
        try
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(SignUpEmail, SignUpPassword);
            var token = auth.FirebaseToken;
            var userId = auth.User.LocalId;
            if (token != null)
            {
                var isStoreUserInfoSuccessful =
                    await StoreUserInfo(SignUpEmail, userId); // Pass user ID to StoreUserInfo
                if (isStoreUserInfoSuccessful)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "User Registered successfully", "OK");
                    await _navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert",
                        "Failed to store user information in Firestore",
                        "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            throw;
        }
    }

    private async Task<bool> StoreUserInfo(string userEmail, string userId)
    {
        try
        {
            var userInfo = new User
            {
                FirstName = null,
                LastName = null,
                IsAdmin = false,
                PhoneNumber = null,
                Address = null,
                UserId = userId,
                Username = userEmail.Split('@')[0], // Use the part of the _signUpEmail before the @ as the username
                Email = userEmail,
                Gender = null
            };

            var userJson = JsonConvert.SerializeObject(userInfo); // Convert User object to JSON string

            await _firebaseClient
                .Child("Users")
                .Child(userId)
                .PutAsync(userJson);

            return true;
        }
        catch (Exception ex)
        {
            GlobalHelper.ShowToast(ex.Message, 16);
            return false;
        }
    }
}