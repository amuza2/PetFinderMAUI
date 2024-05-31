using System.Collections.ObjectModel;
using System.ComponentModel;
using Firebase.Database;
using Firebase.Database.Query;
using PetFinderMAUI.Entities;

namespace PetFinderMAUI.ViewModels;

public class PetViewModel : INotifyPropertyChanged
{
    private static readonly FirebaseClient _firebaseClient = new("https://petcare-1d322-default-rtdb.firebaseio.com/");

    private ObservableCollection<Pet> _pets;

    public PetViewModel()
    {
        Pets = new ObservableCollection<Pet>();
        LoadPets();
    }

    public ObservableCollection<Pet> Pets
    {
        get => _pets;
        set
        {
            _pets = value;
            RaisePropertyChanged("Pets");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private async void LoadPets()
    {
        var pets = await _firebaseClient
            .Child("Pets")
            .OnceAsync<Pet>();

        foreach (var pet in pets)
        {
            pet.Object.PetId = pet.Key; // Set PetId to the key from Firebase
            Pets.Add(pet.Object);
        }
    }

    public async void LikePet(Pet pet)
    {
        pet.IsLiked = !pet.IsLiked;
        await _firebaseClient
            .Child("Pets")
            .Child(pet.PetId) // Use PetId as the key
            .PutAsync(pet);
    }

    private void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}