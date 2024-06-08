using System.ComponentModel;
using System.Globalization;
using CommunityToolkit.Maui.Alerts;
using Firebase.Auth;
using Newtonsoft.Json;
using PetFinderMAUI.Pages;
using PetFinderMAUI.Utils;

namespace PetFinderMAUI.ViewModels;

// ViewModel for the Login Page
internal class LoginViewModel : INotifyPropertyChanged
{
    // Navigation interface
    private readonly INavigation _navigation;

    // Firebase web API key
    private readonly string webApiKey = Configs.WebApiKey;

    // Loading animation
    private bool _isLoginRunning;

    // User's username and password
    private string userName;

    private string userPassword;

    // Constructor for the ViewModel
    public LoginViewModel(INavigation navigation)
    {
        _navigation = navigation;
        RegisterBtn = new Command(RegisterBtnTappedAsync);
        LoginBtn = new Command(LoginBtnTappedAsync);
    }

    public bool IsLoginRunning
    {
        get => _isLoginRunning;
        set
        {
            _isLoginRunning = value;
            RaisePropertyChanged("IsLoginRunning");
        }
    }

    // Commands for the Register and Login buttons
    public Command RegisterBtn { get; }
    public Command LoginBtn { get; }


    // Property for the username
    public string UserName
    {
        get => userName;
        set
        {
            userName = value;
            RaisePropertyChanged("UserName");
        }
    }

    // Property for the password
    public string UserPassword
    {
        get => userPassword;
        set
        {
            userPassword = value;
            RaisePropertyChanged("UserPassword");
        }
    }

    // Event for property changed
    public event PropertyChangedEventHandler PropertyChanged;

    // Method for the Login button
    private async void LoginBtnTappedAsync(object obj)
    {
        IsLoginRunning = true;
        // Create a new Firebase Auth Provider
        var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
        try
        {
            // Try to sign in with the provided username and password
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserName, UserPassword);
            var content = await auth.GetFreshAuthAsync();
            var serializedContent = JsonConvert.SerializeObject(content);

            var userId = auth.User.LocalId; // This is the localId of the user


            // Save the token and userId to the preferences
            Preferences.Set("FreshFirebaseToken", serializedContent);
            Preferences.Set("userId", userId);

            // Save the user's credentials in secure storage
            await SecureStorage.SetAsync("username", UserName);
            await SecureStorage.SetAsync("password", UserPassword);

            // Save the user's login status
            Preferences.Set("IsLoggedIn", true);
            IsLoginRunning = false;

            // Navigate to the Main Page
            // await this._navigation.PushAsync(new HomePage());
            // Set the HomePage of the application to a new instance of HomePage
            Application.Current!.MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            IsLoginRunning = false;

            ShowSnackBar(ex.Message.Contains("INVALID_LOGIN_CREDENTIALS")
                ? "Email or password is incorrect"
                : "Unknown error");
        }
    }

    private static async void ShowSnackBar(string text)
    {
        var snackbar = new Snackbar
        {
            Text = text,
            Duration = TimeSpan.FromSeconds(3)
        };
        await snackbar.Show();
    }

    // Method for the Register button
    private async void RegisterBtnTappedAsync(object obj)
    {
        // Navigate to the SignUp Page

        await _navigation.PushAsync(new SignUpPage());
    }

    // Method to raise the PropertyChanged event
    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }
}

public class InverseBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue) return !boolValue;

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}