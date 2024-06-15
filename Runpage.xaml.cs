using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;

namespace Biathlon_Esthivaukan;

public partial class Runpage : ContentPage
{
    bool isRunning = true;
    DateTime startTime;
    TimeSpan elapsed;

    private int countShootPageVisited = 0;
    private static List<int> shootResults = new List<int>();

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

            // Stop the counter after one hour
            if (elapsed >= TimeSpan.FromMinutes(60))
            {
                isRunning = false;
            }

            await Task.Delay(1000); // Wait for 1 second
        }
    }

    private async void OnShootClicked(object sender, EventArgs e)
    {
        PublicVariablesRP.Elapsed = elapsed;
        isRunning = false; // Stop the timer
        countShootPageVisited++;
        Debug.WriteLine("Test !!!!");
        Debug.WriteLine($"On a visité la page ShootPage {countShootPageVisited} fois");
        if (countShootPageVisited <= 3)
        {
            await Navigation.PushAsync(new Shootpage(), false);
        }
        else
        {
            await DisplayAlert("Limite Atteinte", "Vous avez déjà entré tous les tirs !", "OK");
            CalculateFinalResult();
        }
    }

    public static void StoreShootResult(int result)
    {
        shootResults.Add(result);
    }

    public static class PublicVariablesRP
    {
        public static TimeSpan Elapsed { get; set; }
    }
    private void CalculateFinalResult()
    {
        int totalHits = shootResults.Sum();
        int totalShots = shootResults.Count * 5; // Assuming 5 shots per session
        double score = (double)totalHits / totalShots * 100;
        string message = $"Votre score final est de {totalHits} / {totalShots}. ({score}% réussi)";
        Debug.WriteLine(message);
        DisplayAlert("Score Final", message, "OK");
    }
}
