using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Biathlon_Esthivaukan
{
    public partial class Shootpage : ContentPage
    {
        DateTime startTime;
        private int cpt = 0;
        private static int countRunPageVisited = 0;
        bool isRunning = true;
        bool isRunning2min = true;
        TimeSpan elapsed;
        public ObservableCollection<CheckBoxViewModel> Items { get; set; }
        private static int compteur = 0;
        Runpage runp = new Runpage();

        public Shootpage()
        {
            InitializeComponent();

            TimeSpan elapsedTime = PublicVariables.Elapsed;
            TimeSpan elapsed2min = TimeSpan.FromSeconds(120); // Initialiser à 2 minutes
            Chrono.Text = elapsedTime.ToString(@"mm\:ss");

            Chrono2.Text = elapsed2min.ToString(@"mm\:ss");

            Items = new ObservableCollection<CheckBoxViewModel>
            {
                new CheckBoxViewModel { IsChecked = false, Text = "Tir 1" },
                new CheckBoxViewModel { IsChecked = false, Text = "Tir 2" },
                new CheckBoxViewModel { IsChecked = false, Text = "Tir 3" },
                new CheckBoxViewModel { IsChecked = false, Text = "Tir 4" },
                new CheckBoxViewModel { IsChecked = false, Text = "Tir 5" }
            };

            foreach (var item in Items)
            {
                item.PropertyChanged += CheckBoxViewModel_PropertyChanged;
            }

            BindingContext = this;

            // Print race times
            PrintRaceTimes();
            


            // Démarrer le minuteur pour 2 minutes
            StartTimer(elapsed2min);
        }

        public static class PublicVariables
        {
            public static TimeSpan Elapsed { get; set; }
        }

        private async void StartTimer(TimeSpan initialTime)
        {
            while (isRunning2min && initialTime.TotalSeconds > 0)
            {
                initialTime = initialTime.Subtract(TimeSpan.FromSeconds(1));
                Chrono2.Text = initialTime.ToString(@"mm\:ss");
                await Task.Delay(1000); // Attendre 1 seconde
            }

            if (initialTime.TotalSeconds == 0)
            {
                isRunning2min = false;
                await Navigation.PushAsync(new Runpage(), false);
            }
        }

        private void CheckBoxViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CheckBoxViewModel.IsChecked))
            {
                var checkBox = sender as CheckBoxViewModel;
                if (checkBox.IsChecked)
                {
                    compteur++;
                }
                else
                {
                    compteur--;
                }

                Debug.WriteLine($"Compteur: {compteur}");
                Runpage.StoreShootResult(compteur);
                compteur = 0;
            }
        }

        private async void OnCalculateClicked(object sender, EventArgs e)
        {
            string resultatMessage = await CalculateResultAsync(compteur);
            Debug.WriteLine(resultatMessage);
            compteur = 0;
            await Navigation.PopAsync();
        }

        private async Task<string> CalculateResultAsync(int compteur)
        {
            double totalChecked = compteur; // Nombre total de cases cochées
            double totalItems = 15; // Nombre total de cases (5 tirs * 3 sessions)

            if (compteur == 3)
            {
                double resultat = Math.Round(compteur / totalItems * 100, 2);
                string message = $"Votre score est de {compteur} / {totalItems}. ({resultat}% réussi)";
                Debug.WriteLine($"totalChecked: {totalChecked}");
                Debug.WriteLine($"totalItems: {totalItems}");
                Debug.WriteLine($"resultat: {resultat}");
                return message;
            }
            return string.Empty;
        }

        private async void OnRunPage(object sender, EventArgs e)
        {
            isRunning = false; // Arrêtez le chronomètre principal
            isRunning2min = false; // Arrêtez le minuteur de 2 minutes
            PublicVariables.Elapsed = elapsed;
            countRunPageVisited++;
            Debug.WriteLine($"On a visité la page RunPage {countRunPageVisited} fois");
            if (countRunPageVisited == 3)
            {
                cpt++;
                Debug.WriteLine("le cpt est a :");
                Debug.WriteLine(cpt);
                Debug.WriteLine("Victoire!!");
                await DisplayAlert("Limite Atteinte", "Vous avez déjà entré tous les tirs !", "OK");
                runp.CalculateFinalResult();
                ResetVariables();

                // Naviguez vers MainPage ou une autre page
                Debug.WriteLine("On a tenté de revenir sur la main!!");
                await Navigation.PushAsync(new MainPage(), false);
            }
            if (cpt != 1)
                await Navigation.PushAsync(new Runpage(), false);
        }

        private void PrintRaceTimes()
        {
            var time1 = Runpage.getTime1();
            if (time1.HasValue)
            {
                Debug.WriteLine($"Race Time 1: {time1.Value.ToString(@"mm\:ss")}");
            }

            var time2 = Runpage.getTime2();
            if (time2.HasValue)
            {
                Debug.WriteLine($"Race Time 2: {time2.Value.ToString(@"mm\:ss")}");
            }

            var time3 = Runpage.getTime3();
            if (time3.HasValue)
            {
                Debug.WriteLine($"Race Time 3: {time3.Value.ToString(@"mm\:ss")}");
            }
        }
   



        private void ResetVariables()
        {
            compteur = 0;
            countRunPageVisited = 0;
            Runpage.countShootPageVisited = 0;
            Runpage.shootResults.Clear();
            Runpage.raceTimes.Clear();
            cpt = 0;
        }
    }
}
