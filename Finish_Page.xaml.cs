using MailKit.Net.Smtp;
using MimeKit;
using System;
using static Biathlon_Esthivaukan.Runpage;
using static Biathlon_Esthivaukan.Profil_Page;
using System.Data;
using CsvHelper;
using CsvHelper.Configuration;


namespace Biathlon_Esthivaukan;

public partial class Finish_Page : ContentPage
{
	public double minutesPerKilometer;
    public Finish_Page()
	{
		InitializeComponent();

        //_csvFilePath = Path.Combine(FileSystem.AppDataDirectory, "user_data.csv");


        TempsFinal.Text=PublicVariablesRP.Elapsed.ToString(@"mm\:ss");
        double distance = 1.2;
        double elapsedMinutes = PublicVariablesRP.Elapsed.TotalMinutes;
        minutesPerKilometer = elapsedMinutes / distance;
        Allure.Text = $"{minutesPerKilometer:F2}m/km";

        SaveUserData();
	}

    private void SaveUserData()
    {
        var userData = new UserData
        {
            Nom = PublicVariablesPP.Nom,
            Prénom = PublicVariablesPP.Prenom,
            TempsComplet = PublicVariablesRP.Elapsed.ToString(@"mm\:ss"),
            Allure = $"{minutesPerKilometer:F2} m/km",
            Email = PublicVariablesPP.Email
        };

        //var csvHelper = new CSVHelper(_csvFilePath);
        //csvHelper.WriteUserData(userData);
    }
    private async void OnShareClicked(object sender, EventArgs e)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("BiathlonEstivaukan", "biathlon.vaukan@gmail.com"));
        message.To.Add(new MailboxAddress("", "fyruxyt@gmail.com"));
        message.To.Add(new MailboxAddress("", PublicVariablesPP.Email));
        message.Subject = $"Course de {PublicVariablesPP.Nom} {PublicVariablesPP.Prenom} ";

        message.Body = new TextPart("plain")
        {
            Text = $"Course de {PublicVariablesPP.Nom} {PublicVariablesPP.Prenom}. \n"+ /*"Temps au 200m : {Temps_200}\tTemps au 400m : {Temps_400}\tTemps au 600m : {Temps_600}"*/
            $"Temps Complet : {PublicVariablesRP.Elapsed.ToString(@"mm\:ss")}" +
            $"\n Allure :{minutesPerKilometer:F2} m/km"

        };

        using (var client = new SmtpClient())
        {
            try
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Note: only needed if the SMTP server requires authentication
                await client.AuthenticateAsync("biathlon.vaukan@gmail.com", "ftqv dilq epmj vgbt");

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., display an error message)
                Console.WriteLine(ex.Message);
            }
        }
    }


    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage(), false);

    }


    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Profil_Page(), false);

    }
    
}