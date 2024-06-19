using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Biathlon_Esthivaukan
{
    public partial class Profil_Page : ContentPage
    {
        public Profil_Page()
        {
            InitializeComponent();
            LoadUserData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadUserData();
        }

        private void LoadUserData()
        {
            if (!string.IsNullOrEmpty(PublicVariablesPP.Prenom))
            {
                PrenomEntry.Text = PublicVariablesPP.Prenom;
            }
            if (!string.IsNullOrEmpty(PublicVariablesPP.Nom))
            {
                NomEntry.Text = PublicVariablesPP.Nom;
            }
            if (!string.IsNullOrEmpty(PublicVariablesPP.Email))
            {
                EmailEntry.Text = PublicVariablesPP.Email;
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PrenomEntry.Text))
            {
                PublicVariablesPP.Prenom = PrenomEntry.Text;
            }

            if (!string.IsNullOrEmpty(NomEntry.Text))
            {
                PublicVariablesPP.Nom = NomEntry.Text;
            }

            if (!string.IsNullOrEmpty(EmailEntry.Text))
            {
                PublicVariablesPP.Email = EmailEntry.Text;
            }

            // Stockage des informations utilisateur dans les variables publiques
            await DisplayAlert("Information Saisie", $"Prénom: {PublicVariablesPP.Prenom}\nNom: {PublicVariablesPP.Nom}\nEmail: {PublicVariablesPP.Email}", "OK");
            await Navigation.PushAsync(new MainPage(), false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(), false);
        }

        private async void OnTrackingClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SuiviPerf(), false);
        }

        public static class PublicVariablesPP
        {
            public static string Prenom { get; set; }
            public static string Nom { get; set; }
            public static string Email { get; set; }
        }
    }
}
