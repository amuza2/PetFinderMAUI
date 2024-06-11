using Firebase.Database;
using PetFinderMAUI.Entities;
using PetFinderMAUI.Utils;
using Plugin.LocalNotification;
using System.Collections.Generic;

namespace PetFinderMAUI;

public partial class AppShell : Shell
{
    private static readonly FirebaseClient _firebaseClient = new(Configs.FirebaseDbUrl);
    private readonly HashSet<string> _notifiedPetIds = new HashSet<string>();
    private readonly DateTime _appLaunchTime = DateTime.Now;

    public AppShell()
    {
        InitializeComponent();
        SetupFirebaseListener();
    }

    private void SetupFirebaseListener()
    {
        _firebaseClient
            .Child("Pets")
            .AsObservable<Pet>()
            .Subscribe(addedPet =>
            {
                if (addedPet.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate &&
                    addedPet.Object != null && addedPet.Object.Timestamp > _appLaunchTime &&
                    !_notifiedPetIds.Contains(addedPet.Object.PetId!))
                {
                    ShowLocalNotification(addedPet.Object);
                    _notifiedPetIds.Add(addedPet.Object.PetId!);
                }
            });
    }

    private void ShowLocalNotification(Pet newPet)
    {
        var notification = new NotificationRequest
        {
            NotificationId = new Random().Next(),
            Title = "New Pet Added",
            Description = $"You have a new pet to check: {newPet.PetName}",
            ReturningData =
                $"petId={newPet.PetId}" // Pass the pet id to open the pet page when the notification is clicked
        };
        LocalNotificationCenter.Current.Show(notification);
    }
}