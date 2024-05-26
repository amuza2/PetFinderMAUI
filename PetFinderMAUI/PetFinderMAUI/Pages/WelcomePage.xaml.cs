namespace PetFinderMAUI.Pages;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}
	private async void btnLogin_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Login2());
	}

	private async void btnSignUp_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new SignUp2());
	}
}