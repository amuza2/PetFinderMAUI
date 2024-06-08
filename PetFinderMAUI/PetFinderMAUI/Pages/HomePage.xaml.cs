using PetFinderMAUI.Entities;
using PetFinderMAUI.Utils;
using PetFinderMAUI.ViewModels;
using PetFinderMAUI.Views;

namespace PetFinderMAUI.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        BindingContext = new PetViewModel();

        // UserEmail.Text = "Welcome " + GlobalHelper.GetUserEmail();
    }

    private async void OnFabClicked(object sender, EventArgs e)
    {
        // Navigate to the NewPostPage when the FloatingActionButton is clicked
        await Navigation.PushAsync(new NewPostPage());
    }

    // private async void OnPetItemSelected(object sender, SelectionChangedEventArgs e)
    // {
    //     var pet = (Pet)e.CurrentSelection.FirstOrDefault()!;
    //     var user = await ((PetViewModel)BindingContext).GetUserByPublisherId(pet.PublisherId!);
    //
    //     var sheet = new UserInfoBottomSheet(user);
    //
    //
    //     // Display the user information in a bottom sheet
    //     await sheet.ShowAsync();
    // }

    // private void ShowBottomSheet(object sender, EventArgs e)
    // {
    //     var pet = (Pet)e.CurrentSelection.FirstOrDefault()!;
    //     var user = await ((PetViewModel)BindingContext).GetUserByPublisherId(pet.PublisherId!);
    //     var myBottomSheet = new PetInfoBottomSheet(user);
    //     
    //     
    //     myBottomSheet.IsPresented = true;
    // }
   async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // string previous = (e.PreviousSelection.FirstOrDefault() as Pet)?.PetName;
        // string current = (e.CurrentSelection.FirstOrDefault() as Pet)?.PetName!;
        // GlobalHelper.ShowToast(current, 16);
        try
        {
            var pet = (Pet)e.CurrentSelection.FirstOrDefault();
            if (pet != null)
            {
                var petViewModel = (PetViewModel)BindingContext;
                var user = await petViewModel.GetUserByPublisherId(pet.PublisherId!);
                if (user != null)
                {
                    var userInfoModalPage = new PetInfoModalPage(user);
                    await Navigation.PushModalAsync(userInfoModalPage);
                }
                else
                {
                    // Handle the case where the user is null
                    Console.WriteLine($"No user found with publisher ID {pet.PublisherId}");
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception to help with debugging
            Console.WriteLine(ex);
        }
    }
}