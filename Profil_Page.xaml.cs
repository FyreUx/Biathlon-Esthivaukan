using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Maui.Controls; // ou using Xamarin.Forms; selon le framework utilis�
using Microsoft.Maui.Controls.Xaml;

namespace Biathlon_Esthivaukan;

public partial class Profil_Page : ContentPage
{
    public Profil_Page()
    {
        InitializeComponent();
    }



    private void OnSubmitClicked(object sender, EventArgs e)
    {
        string prenom = PrenomEntry.Text;
        string nom = NomEntry.Text;
        string email = EmailEntry.Text;

       
        // Stockage des informations utilisateur dans les variables publiques
        PublicVariablesPP.Prenom = prenom;
        PublicVariablesPP.Nom = nom;
        PublicVariablesPP.Email = email;
        
        // Vous pouvez maintenant utiliser ces informations, par exemple les afficher ou les envoyer � un serveur
        DisplayAlert("Information Saisie", $"Pr�nom: {prenom}\nNom: {nom}\nEmail: {email}", "OK");
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
        await Navigation.PushAsync(new SuiviPerf(), false);

    }

}