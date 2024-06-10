using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Firebase.Database;
using Firebase.Database.Query;
using PetFinderMAUI.Entities;
using PetFinderMAUI.Pages;
using PetFinderMAUI.Utils;

namespace PetFinderMAUI.ViewModels;

public class PetViewModel : INotifyPropertyChanged
{
    private static readonly FirebaseClient _firebaseClient =
        new(Configs.FirebaseDbUrl);

    private bool _isLoading;
    private bool _isRefreshing;

    private ObservableCollection<Pet> _pets;
    

    public PetViewModel()
    {
        Pets = new ObservableCollection<Pet>();
        RefreshCommand = new Command(async () => await LoadPets());
        LoadPets();
       
    }

    public ObservableCollection<Pet> Pets
    {
        get => _pets;
        set
        {
            _pets = value;
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

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            RaisePropertyChanged();
        }
    }

    public ICommand RefreshCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    private async Task LoadPets()
    {
        IsRefreshing = true;

        var pets = await _firebaseClient
            .Child("Pets")
            .OnceAsync<Pet>();

        Pets.Clear();
        foreach (var pet in pets)
        {
            pet.Object.PetId = pet.Key; // Set PetId to the key from Firebase
            Pets.Add(pet.Object);
        }

        IsRefreshing = false;
    }

    public async void LikePet(Pet pet)
    {
        pet.PetFavourite = !pet.PetFavourite;
        await _firebaseClient
            .Child("Pets")
            .Child(pet.PetId) // Use PetId as the key
            .PutAsync(pet);
    }

    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public async Task<User> GetUserByPublisherId(string publisherId)
    {
        var user = await _firebaseClient
            .Child("Users")
            .Child(publisherId)
            .OnceSingleAsync<User>();
        return user;
    }

    
}