﻿namespace Biathlon_Esthivaukan
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
      
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnRunClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Runpage());
            
        }

        private async void OnProfilClicked(object sender , EventArgs e )
        {
            await Navigation.PushAsync(new Profil_Page());
        }
    }

}
