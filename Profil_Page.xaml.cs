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



    private void OnSubmitClicked(object sender, EventArgs e)
    {
        string prenom = PrenomEntry.Text;
        string nom = NomEntry.Text;
        string email = EmailEntry.Text;

       
        // Stockage des informations utilisateur dans les variables publiques
        PublicVariables.Prenom = prenom;
        PublicVariables.Nom = nom;
        PublicVariables.Email = email;
        
        // Vous pouvez maintenant utiliser ces informations, par exemple les afficher ou les envoyer à un serveur
        DisplayAlert("Information Saisie", $"Prénom: {prenom}\nNom: {nom}\nEmail: {email}", "OK");
    }
    public static class PublicVariables
    {
        public static string Prenom { get; set; }
        public static string Nom { get; set; }
        public static string Email { get; set; }
    }


    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());

    }
}