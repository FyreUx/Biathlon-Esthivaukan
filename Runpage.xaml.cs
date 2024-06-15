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

        private static int countShootPageVisited = 0;
        private static List<int> shootResults = new List<int>();

        // Temps enregistr�s pour chaque session de tir
        private TimeSpan clock1;
        private TimeSpan clock2;
        private TimeSpan clock3;

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

                // Arr�tez le compteur apr�s une heure
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
            isRunning = false; // Arr�tez le chronom�tre

            countShootPageVisited++;
            Debug.WriteLine($"On a visit� la page ShootPage {countShootPageVisited} fois");

            if (countShootPageVisited <= 3)
            {
                await Navigation.PushAsync(new Shootpage(), false);
            }
            else
            {
                await DisplayAlert("Limite Atteinte", "Vous avez d�j� entr� tous les tirs !", "OK");
                CalculateFinalResult();

                // R�initialisez les r�sultats pour une nouvelle session
                shootResults.Clear();
                countShootPageVisited = 0;

                // Naviguez vers MainPage ou une autre page
                await Navigation.PushAsync(new MainPage(), false);
            }

            // Enregistrer les temps correspondants
            if (countShootPageVisited == 1)
            {
                clock1 = elapsed;
                Debug.WriteLine($"clock1 enregistr� : {clock1}");
            }
            else if (countShootPageVisited == 2)
            {
                clock2 = elapsed;
                Debug.WriteLine($"clock2 enregistr� : {clock2}");
            }
            else if (countShootPageVisited == 3)
            {
                clock3 = elapsed;
                Debug.WriteLine($"clock3 enregistr� : {clock3}");
            }
        }

        public TimeSpan getClock1()
        {
            return clock1;
        }
        public TimeSpan getClock2()
        {
            return clock2;
        }
        public TimeSpan getClock3()
        {
            return clock3;
        }

        public static void StoreShootResult(int result)
        {
            shootResults.Add(result);
            Debug.WriteLine($"R�sultat enregistr� : {result}");
        }

        public static class PublicVariablesRP
        {
            public static TimeSpan Elapsed { get; set; }
        }


        public static class PublicVariables
        {
            public static TimeSpan Elapsed { get; set; }
        }

        private async void CalculateFinalResult()
        {
            if (shootResults.Count == 0)
            {
                await DisplayAlert("Erreur", "Aucun r�sultat enregistr�", "OK");
                return;
            }

            int totalHits = shootResults.Sum();
            int totalShots = 15; // Nombre total de tirs pour toutes les sessions
            double score = (double)totalHits / totalShots * 100;
            double arrondi = Math.Round(score, 2);
            string message = $"Votre score final est de {totalHits} sur {totalShots} soit {arrondi}%.";
            Debug.WriteLine(message);
            await DisplayAlert("Score Final", message, "OK");
        }
    }
    }

