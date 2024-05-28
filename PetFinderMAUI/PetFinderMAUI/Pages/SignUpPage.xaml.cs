using PetFinderMAUI.ViewModels;

namespace PetFinderMAUI.Pages;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
        BindingContext = new RegisterViewModel(Navigation);
    }
}