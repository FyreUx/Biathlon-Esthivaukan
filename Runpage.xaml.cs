using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Biathlon_Esthivaukan
{
    public partial class Runpage : ContentPage
    {
        bool isRunning = true;
        DateTime startTime;
        TimeSpan elapsed;

        public static int countShootPageVisited = 0;
        public static List<int> shootResults = new List<int>();
        public static List<TimeSpan> raceTimes = new List<TimeSpan>(); // Store race times

        public Runpage()
        {
            InitializeComponent();
            StartCounter();
        }

        private async void StartCounter()
        {
            startTime = DateTime.Now;

            while (isRunning)
            {
                elapsed = DateTime.Now - startTime;
                StopwatchLabel.Text = elapsed.ToString(@"mm\:ss");

                // Arrêtez le compteur après une heure
                if (elapsed >= TimeSpan.FromMinutes(60))
                {
                    isRunning = false;
                }

                await Task.Delay(1000); // Attendre 1 seconde
            }
        }

        private async void OnShootClicked(object sender, EventArgs e)
        {
            PublicVariables.Elapsed = elapsed;
            isRunning = false; // Arrêtez le chronomètre

            countShootPageVisited++;
            Debug.WriteLine($"On a visité la page ShootPage {countShootPageVisited} fois");

            // Store the race time
            if (countShootPageVisited <= 3)
            {
                raceTimes.Add(elapsed);
            }

            if (countShootPageVisited <= 3)
            {
                await Navigation.PushAsync(new Shootpage(), false);
            }
        }

        public static void StoreShootResult(int result)
        {
            shootResults.Add(result);
            Debug.WriteLine($"Résultat enregistré : {result}");
        }

        public static class PublicVariablesRP
        {
            public static TimeSpan Elapsed { get; set; }
        }

        public static class PublicVariables
        {
            public static TimeSpan Elapsed { get; set; }
        }

        public async void CalculateFinalResult()
        {
            if (shootResults.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Aucun résultat enregistré", "OK");
                return;
            }

            int totalHits = shootResults.Sum();
            int totalShots = 15; // Nombre total de tirs pour toutes les sessions
            double score = (double)totalHits / totalShots * 100;
            double arrondi = Math.Round(score, 2);
            string message = $"Votre score final est de {totalHits} sur {totalShots} soit {arrondi}%.";
            Debug.WriteLine(message);
            await Application.Current.MainPage.DisplayAlert("Score Final", message, "OK");
            ResetVariables();

            // Navigate to MainPage
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage(), false);
        }
        public static TimeSpan? getTime1()
        {
            if (Runpage.raceTimes.Count > 0)
            {
                return Runpage.raceTimes[0];
            }
            return null;
        }

        public static TimeSpan? getTime2()
        {
            if (Runpage.raceTimes.Count > 1)
            {
                return Runpage.raceTimes[1];
            }
            return null;
        }

        public static TimeSpan? getTime3()
        {
            if (Runpage.raceTimes.Count > 2)
            {
                return Runpage.raceTimes[2];
            }
            return null;
        }
        private void ResetVariables()
        {
            countShootPageVisited = 0;
            shootResults.Clear();
            raceTimes.Clear(); // Reset race times
        }
    }
}
