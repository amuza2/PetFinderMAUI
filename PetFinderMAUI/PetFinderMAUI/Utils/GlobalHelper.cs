using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Firebase.Auth;
using Newtonsoft.Json;

namespace PetFinderMAUI.Utils;

public class GlobalHelper
{
    public static async void ShowToast(string text, double textSize)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var toast = Toast.Make(text, ToastDuration.Short, textSize);
        await toast.Show(cancellationTokenSource.Token);
    }

    public static string GetUserEmail()
    {
        var userInfo =
            JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
        return userInfo!.User.Email;
    }
}