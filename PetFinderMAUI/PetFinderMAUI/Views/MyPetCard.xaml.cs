using PetFinderMAUI.Entities;
using PetFinderMAUI.ViewModels;

namespace PetFinderMAUI.Views;

public partial class MyPetCard : ContentView
{
    private readonly SettingsViewModel _viewModel;

    public MyPetCard()
    {
        InitializeComponent();
        _viewModel = new SettingsViewModel();
        BindingContext = _viewModel;
    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is Pet pet) _viewModel.DeletePet(pet);
    }
}