using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Firebase.Database;
using Firebase.Database.Query;
using PetFinderMAUI.Entities;
using PetFinderMAUI.Utils;
using User = Firebase.Auth.User;

namespace PetFinderMAUI.ViewModels;

public class SettingsViewModel : INotifyPropertyChanged
{
    private static readonly FirebaseClient _firebaseClient = new(Configs.FirebaseDbUrl);

    private ObservableCollection<Pet> _userPets;

    private bool _isRefreshing;
    public ICommand URefreshCommand { get; set; }

    public SettingsViewModel()
    {
        // Get the user's id
        var userId = Preferences.Get("userId", null);

        if (userId != null)
        {
            // GlobalHelper.ShowToast(userId, 16);

            UserPets = new ObservableCollection<Pet>();
            URefreshCommand = new Command(async () => await LoadUserPets(userId!));
            LoadUserPets(userId!);
        }
    }

    public ObservableCollection<Pet> UserPets
    {
        get => _userPets;
        set
        {
            _userPets = value;
            RaisePropertyChanged();
        }
    }

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set
        {
            _isRefreshing = value;
            RaisePropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private async Task LoadUserPets(string userId)
    {
        try
        {
            IsRefreshing = true;

            // Fetch all pets from the database
            var allPets = await _firebaseClient
                .Child("Pets")
                .OnceAsync<Pet>();

            UserPets.Clear();
            foreach (var pet in allPets)
            {
                pet.Object.PetId = pet.Key; // Set PetId to the key from Firebase
                if (pet.Object.PublisherId == userId)
                {
                    UserPets.Add(pet.Object);
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception to help with debugging
            // Console.WriteLine($"An error occurred while loading the user's pets: {ex.Message}");
            GlobalHelper.ShowToast($"An error occurred while loading the user's pets: {ex.Message}", 16);
            IsRefreshing = false;
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public async void DeletePet(Pet pet)
    {
        await _firebaseClient
            .Child("Pets")
            .Child(pet.PetId) // Use PetId as the key
            .DeleteAsync();
        UserPets.Remove(pet);
    }
}