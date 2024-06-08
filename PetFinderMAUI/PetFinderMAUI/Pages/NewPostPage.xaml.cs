using PetFinderMAUI.ViewModels;

namespace PetFinderMAUI.Pages;

public partial class NewPostPage : ContentPage
{
    private readonly NewPostViewModel _viewModel;

    public NewPostPage()
    {
        InitializeComponent();
        _viewModel = new NewPostViewModel();
        BindingContext = _viewModel;
    }

    private async void OnPickImageButtonClicked(object sender, EventArgs e)
    {
        await _viewModel.PickAndUploadImage();
    }

    private async void OnPostButtonClicked(object sender, EventArgs e)
    {
        await _viewModel.PostPet();
        await Navigation.PopAsync(); // Navigate back to the previous page
    }
}