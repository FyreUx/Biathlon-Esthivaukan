using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static Biathlon_Esthivaukan.Shootpage;
using static Biathlon_Esthivaukan.MainPage;



namespace Biathlon_Esthivaukan
{
    public partial class Runpage : ContentPage
    {
        bool isRunning = true;
        DateTime startTime;
        TimeSpan elapsed;
        TimeSpan elapsed0;
        TimeSpan elapsedsum;
        public static int countShootPageVisited = 0;
        public static List<int> shootResults = new List<int>();
        public static List<TimeSpan> raceTimes = new List<TimeSpan>(); // Store race times

        public Runpage(TimeSpan time)
        {
            InitializeComponent();
            elapsed0 = time;
            Console.WriteLine($"Total elapsed time: {elapsed0}");

            StartCounter();
            Showclock();
        }
        private async void Showclock()
        {
            while (isRunning)

            {
                elapsedsum = elapsed0 + elapsed;
                StopwatchLabel.Text = elapsedsum.ToString(@"mm\:ss");
                await Task.Delay(1000); // Attendre 1 seconde

            }
        }

        private async void StartCounter()
        {
            startTime = DateTime.Now;
            while (isRunning)
            {
                
                elapsed = DateTime.Now - startTime;
                

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
            PublicVariablesRP.Elapsed = elapsedsum;
            isRunning = false; // Arrêtez le chronomètre



            countShootPageVisited++;
            Debug.WriteLine($"On a visité la page ShootPage {countShootPageVisited} fois");

            // Store the race time
            if (countShootPageVisited <= 3)
            {
                raceTimes.Add(elapsedsum);
                if (countShootPageVisited == 1)
                {
                    if (PublicVariablesMP.fdistance.Contains("2"))
                    {
                        PublicVariablesSP.time200 = elapsed;
                    }
                    else if (PublicVariablesMP.fdistance.Contains("4"))
                    {
                        PublicVariablesSP.time400 = elapsed;
                    }
                    else if (PublicVariablesMP.fdistance.Contains("6"))
                    {
                        PublicVariablesSP.time600 = elapsed;
                    }
                }
                if (countShootPageVisited == 2)
                {
                    if (PublicVariablesMP.sdistance.Contains("2"))
                    {
                        PublicVariablesSP.time200 = elapsed;
                    }
                    else if (PublicVariablesMP.sdistance.Contains("4"))
                    {
                        PublicVariablesSP.time400 = elapsed;
                    }
                    else if (PublicVariablesMP.sdistance.Contains("6"))
                    {
                        PublicVariablesSP.time600 = elapsed;
                    }
                }
                if (countShootPageVisited == 3)
                {
                    if (PublicVariablesMP.tdistance.Contains("2"))
                    {
                        PublicVariablesSP.time200 = elapsed;
                    }
                    else if (PublicVariablesMP.tdistance.Contains("4"))
                    {
                        PublicVariablesSP.time400 = elapsed;
                    }
                    else if (PublicVariablesMP.tdistance.Contains("6"))
                    {
                        PublicVariablesSP.time600 = elapsed;
                    }
                }

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

            public static double Averageshot { get; set; }
        }

        public async void CalculateFinalResult()
        {

            int totalHits = shootResults.Sum();
            int totalShots = 15; // Nombre total de tirs pour toutes les sessions
            double score = (double)totalHits / totalShots * 100;
            double arrondi = Math.Round(score, 2);
            PublicVariablesRP.Averageshot = arrondi;

            if (shootResults.Count == 0)
            {
                string message = $"Votre score final est de {totalHits} sur {totalShots} soit {arrondi}%.";
                Debug.WriteLine(message);
                await Application.Current.MainPage.DisplayAlert("Score Final", message, "OK");
                ResetVariables();

                // Navigate to MainPage
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage(), false);
            }
            else
            {
                string message = $"Votre score final est de {totalHits} sur {totalShots} soit {arrondi}%.";
                Debug.WriteLine(message);
                await Application.Current.MainPage.DisplayAlert("Score Final", message, "OK");
                ResetVariables();

                // Navigate to MainPage
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage(), false);
            }

            // Navigate to MainPage
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage(), false);
        }
        public static TimeSpan? GetTime1()
        {
            if (Runpage.raceTimes.Count > 0)
            {
                var firstRaceTime = Runpage.raceTimes.FirstOrDefault();
                Debug.WriteLine("Premier temps : " + firstRaceTime.ToString(@"mm\:ss"));
                return firstRaceTime;
            }

            Debug.WriteLine("Aucun temps enregistré pour le premier chrono.");
            return null;
        }


        public static TimeSpan? GetTime2()
        {
            if (Runpage.raceTimes.Count > 1)
            {
                var secondRaceTime = Runpage.raceTimes.ElementAtOrDefault(1);
                Debug.WriteLine("Deuxième temps : " + secondRaceTime.ToString(@"mm\:ss"));
                return secondRaceTime;
            }

            Debug.WriteLine("Aucun temps enregistré pour le deuxième chrono.");
            return null;
        }

        public static TimeSpan? GetTime3()
        {
            if (Runpage.raceTimes.Count > 2)
            {
                var thirdRaceTime = Runpage.raceTimes.ElementAtOrDefault(2);
                Debug.WriteLine("Troisième temps : " + thirdRaceTime.ToString(@"mm\:ss"));
                return thirdRaceTime;
            }

            Debug.WriteLine("Aucun temps enregistré pour le troisième chrono.");
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
