using Firebase.Auth;
using System.ComponentModel;

namespace PetFinderMAUI.ViewModels;

internal class RegisterViewModel : INotifyPropertyChanged
{
    public string webApiKey = "AIzaSyDHyJAIn5P56uvAmFcSoq7M_MsP_azTmn0";

    private INavigation _navigation;
    private string email;
    private string _signUpPassword;

    public event PropertyChangedEventHandler PropertyChanged;

    public string SignUpEmail
    {
        get => email;
        set
        {
            email = value;
            RaisePropertyChanged("SignUpEmail");
        }
    }

    public string SignUpPassword
    {
        get => _signUpPassword; set
        {
            _signUpPassword = value;
            RaisePropertyChanged("SignUpPassword");
        }
    }

    public Command SignUpBtn { get; }

    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }

    public RegisterViewModel(INavigation navigation)
    {
        this._navigation = navigation;

        SignUpBtn = new Command(RegisterUserTappedAsync);
    }

    private async void RegisterUserTappedAsync(object obj)
    {
        try
        {
            // var testEmail = SignUpEmail;
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(SignUpEmail, SignUpPassword);
            string token = auth.FirebaseToken;
            if (token != null)
                await App.Current.MainPage.DisplayAlert("Alert", "User Registered successfully", "OK");
            await this._navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            throw;
        }
    }
}