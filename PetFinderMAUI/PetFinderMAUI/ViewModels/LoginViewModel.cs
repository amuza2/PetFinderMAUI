using System.ComponentModel;
using Firebase.Auth;
using Newtonsoft.Json;
using PetFinderMAUI.Pages;

namespace PetFinderMAUI.ViewModels;

// ViewModel for the Login Page
internal class LoginViewModel : INotifyPropertyChanged
{
    // Firebase web API key
    public string webApiKey =  "AIzaSyDHyJAIn5P56uvAmFcSoq7M_MsP_azTmn0";
    
    // Navigation interface
    private INavigation _navigation;
    
    // User's username and password
    private string userName;
    private string userPassword;

    // Event for property changed
    public event PropertyChangedEventHandler PropertyChanged;

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

    // Constructor for the ViewModel
    public LoginViewModel(INavigation navigation)
    {
        this._navigation = navigation;
        RegisterBtn = new Command(RegisterBtnTappedAsync);
        LoginBtn = new Command(LoginBtnTappedAsync);
    }

    // Method for the Login button
    private async void LoginBtnTappedAsync(object obj)
    {
        // Create a new Firebase Auth Provider
        var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
        try
        {
            // Try to sign in with the provided username and password
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserName, UserPassword);
            var content = await auth.GetFreshAuthAsync();
            var serializedContent = JsonConvert.SerializeObject(content);
            
            // Save the token to the preferences
            Preferences.Set("FreshFirebaseToken", serializedContent);
            
            // Navigate to the Main Page
            await this._navigation.PushAsync(new MainPage());
        }
        catch (Exception ex)
        {
            // Show an alert if there's an error
            await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            throw;
        }
    }

    // Method for the Register button
    private async void RegisterBtnTappedAsync(object obj)
    {
        // Navigate to the Sign Up Page
        await this._navigation.PushAsync(new SignUpPage());
    }

    // Method to raise the PropertyChanged event
    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }
}