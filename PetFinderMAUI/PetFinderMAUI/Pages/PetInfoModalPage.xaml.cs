using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFinderMAUI.Entities;
using PetFinderMAUI.Utils;

namespace PetFinderMAUI.Pages;

public partial class PetInfoModalPage : ContentPage
{
    public User User { get; set; }
    
    public PetInfoModalPage(User user)
    {
        InitializeComponent();
        User = user;
        BindingContext = this;
        
        Username.Text = user.Username;
        Email.Text = user.Email;
    }
    private void OnCopyPhoneNumberClicked(object sender, EventArgs e)
    {
        Clipboard.SetTextAsync(User.PhoneNumber);
    }
}