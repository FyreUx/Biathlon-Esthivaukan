using System;
using Microsoft.Maui.Controls;

namespace Biathlon_Esthivaukan
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
      


        private async void OnRunClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Runpage(),false);
            
        }

        private async void OnTrackingClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SuiviPerf(),false);

        }

        private async void OnProfilClicked(object sender , EventArgs e )
        {
            await Navigation.PushAsync(new Profil_Page(), false);
        }

        private void On400mClicked(object sender, EventArgs e)
        {
            // Logique à exécuter lorsque le bouton 400m est cliqué
            DisplayAlert("400m Button Clicked", "You clicked the 400 meters button.", "OK");
        }

        private void On200mClicked(object sender, EventArgs e)
        {
            // Logique à exécuter lorsque le bouton 200m est cliqué
            DisplayAlert("200m Button Clicked", "You clicked the 200 meters button.", "OK");
        }

        private void On600mClicked(object sender, EventArgs e)
        {
            // Logique à exécuter lorsque le bouton 600m est cliqué
            DisplayAlert("600m Button Clicked", "You clicked the 600 meters button.", "OK");
        }
    }
}
