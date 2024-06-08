using PetFinderMAUI.Entities;
using PetFinderMAUI.ViewModels;

namespace PetFinderMAUI.Views;

public partial class PetCard : ContentView
{
    private readonly PetViewModel _viewModel;

    public PetCard()
    {
        InitializeComponent();
        _viewModel = new PetViewModel();
        BindingContext = _viewModel;
    }

    private void OnLikeButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is Pet pet) _viewModel.LikePet(pet);
    }
}