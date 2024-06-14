using System;
using Microsoft.Maui.Controls;

namespace Biathlon_Esthivaukan;

public partial class Runpage : ContentPage
{
    bool isRunning = true;
    DateTime startTime;
    TimeSpan elapsed;

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
        isRunning = false; // Stop the timer

        // Store the elapsed time in a public variable
        PublicVariables.Elapsed = elapsed;
        await Navigation.PushAsync(new Shootpage());
    }

    public static class PublicVariables
    {
        public static TimeSpan Elapsed { get; set; }
    }
}