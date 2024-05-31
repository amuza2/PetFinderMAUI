using System;
using PetFinderMAUI.ViewModels;
using PetFinderMAUI.Entities;

namespace PetFinderMAUI.Views;

public partial class PetCard : ContentView
{
    private PetViewModel _viewModel;

    public PetCard()
    {
        InitializeComponent();
        _viewModel = new PetViewModel();
        BindingContext = _viewModel;
    }

    private void OnLikeButtonClicked(object sender, EventArgs e)
    {
        if (BindingContext is Pet pet)
        {
            _viewModel.LikePet(pet);
        }
    }
}