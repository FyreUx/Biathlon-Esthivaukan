using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;

namespace Biathlon_Esthivaukan;

public partial class Runpage : ContentPage
{
    bool isRunning = true;
    DateTime startTime;
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
            TimeSpan elapsed = DateTime.Now - startTime;
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
        isRunning = false; // Stop the timer
        countShootPageVisited++;
        Debug.WriteLine("Test !!!!");
        Debug.WriteLine($"On a visité la page ShootPage {countShootPageVisited} fois");
        if (countShootPageVisited <= 3)
        {
            await Navigation.PushAsync(new Shootpage());
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
