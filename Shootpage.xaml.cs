using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using static Biathlon_Esthivaukan.Runpage;


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
        Runpage runp = new Runpage(TimeSpan.Zero);

        public Shootpage()
        {
            InitializeComponent();

            TimeSpan elapsedTime = PublicVariablesRP.Elapsed;
            TimeSpan elapsed2min = TimeSpan.FromSeconds(120); // Initialiser � 2 minutes
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
            


            // D�marrer le minuteur pour 2 minutes
            StartTimer(elapsed2min);
        }

        public static class PublicVariablesSP
        {
            public static TimeSpan Elapsed { get; set; }
            public static TimeSpan time200 { get; set; }
            public static TimeSpan time400 { get; set; }
            public static TimeSpan time600 { get; set; }
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
                await OnRunPage();
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
            double totalChecked = compteur; // Nombre total de cases coch�es
            double totalItems = 15; // Nombre total de cases (5 tirs * 3 sessions)

            if (compteur == 3)
            {
                double resultat = Math.Round(compteur / totalItems * 100, 2);
                string message = $"Votre score est de {compteur} / {totalItems}. ({resultat}% r�ussi)";
                Debug.WriteLine($"totalChecked: {totalChecked}");
                Debug.WriteLine($"totalItems: {totalItems}");
                Debug.WriteLine($"resultat: {resultat}");
                return message;
            }
            return string.Empty;
        }

        private async Task OnRunPage()
        {
            isRunning = false; // Arr�tez le chronom�tre principal
            isRunning2min = false; // Arr�tez le minuteur de 2 minutes
            elapsed = PublicVariablesRP.Elapsed;            
            countRunPageVisited++;
            Debug.WriteLine($"On a visit� la page RunPage {countRunPageVisited} fois");
            if (countRunPageVisited == 3)
            {
                cpt++;
                Debug.WriteLine("le cpt est a :");
                Debug.WriteLine(cpt);
                Debug.WriteLine("Victoire!!");
                await DisplayAlert("Limite Atteinte", "Vous avez d�j� entr� tous les tirs !", "OK");
                runp.CalculateFinalResult();
                ResetVariables();

                // Naviguez vers finish page
                Debug.WriteLine("On a tent� de revenir sur la main!!");
                await Navigation.PushAsync(new Finish_Page(), false);
            }
            if (cpt != 1)
                await Navigation.PushAsync(new Runpage(elapsed), false);
        }

        private void PrintRaceTimes()
        {
            var time1 = Runpage.GetTime1();
            if (time1.HasValue)
            {
                Debug.WriteLine($"Race Time 1: {time1.Value.ToString(@"mm\:ss")}");
            }

            var time2 = Runpage.GetTime2();
            if (time2.HasValue)
            {
                Debug.WriteLine($"Race Time 2: {time2.Value.ToString(@"mm\:ss")}");

            }

            var time3 = Runpage.GetTime3();
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
