using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Maui.Controls; // ou using Xamarin.Forms; selon le framework utilisé
using Microsoft.Maui.Controls.Xaml;

namespace Biathlon_Esthivaukan;

public partial class Profil_Page : ContentPage
{
    public Profil_Page()
    {
        InitializeComponent();
    }



    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(PrenomEntry.Text))
        {
            string prenom = PrenomEntry.Text;
            PublicVariablesPP.Prenom = prenom;
        }

        if (!string.IsNullOrEmpty(NomEntry.Text))
        {
            string nom = NomEntry.Text;
            PublicVariablesPP.Nom = nom;
        }
        if (!string.IsNullOrEmpty(EmailEntry.Text))
        {
            string email = EmailEntry.Text;
            PublicVariablesPP.Email = email;
        }

       
        // Stockage des informations utilisateur dans les variables publiques
        
        
        DisplayAlert("Information Saisie", $"Prénom: {PublicVariablesPP.Prenom}\nNom: {PublicVariablesPP.Nom}\nEmail: {PublicVariablesPP.Email}", "OK");
        await Navigation.PushAsync(new MainPage(), false);
    }
    public static class PublicVariablesPP
    {
        public static string Prenom { get; set; }
        public static string Nom { get; set; }
        public static string Email { get; set; }
    }


    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage(), false);

    }
    private async void OnTrackingClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Finish_Page(), false);

    }

}