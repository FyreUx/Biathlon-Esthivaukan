using MailKit.Net.Smtp;
using MimeKit;
using System;
using static Biathlon_Esthivaukan.Runpage;
using static Biathlon_Esthivaukan.Profil_Page;
using static Biathlon_Esthivaukan.Shootpage;

using System.Data;



namespace Biathlon_Esthivaukan;

public partial class Finish_Page : ContentPage
{
	public double minutesPerKilometer;
    private readonly string _csvFilePath;

    public Finish_Page()
	{
		InitializeComponent();

        _csvFilePath = Path.Combine(FileSystem.AppDataDirectory, "historique_des_courses.csv");


        TempsFinal.Text=PublicVariablesRP.Elapsed.ToString(@"mm\:ss");
        double distance = 1.2;
        double elapsedMinutes = PublicVariablesRP.Elapsed.TotalMinutes;
        minutesPerKilometer = elapsedMinutes / distance;
        Allure.Text = $"{minutesPerKilometer:F2}m/km";
        Temps200.Text = PublicVariablesSP.time200.ToString(@"mm\:ss");
        Temps400.Text = PublicVariablesSP.time400.ToString(@"mm\:ss");
        Temps600.Text = PublicVariablesSP.time600.ToString(@"mm\:ss");
        Précision.Text = PublicVariablesRP.Averageshot.ToString("G");
        P1.Text = PublicVariablesRP.ShootResults[0].ToString("G");
        P2.Text = PublicVariablesRP.ShootResults[1].ToString("G");
        P3.Text = PublicVariablesRP.ShootResults[2].ToString("G");

        SaveUserData();
    }

    private void SaveUserData()
    {
        var userData = new CSVHelper.UserData
        {
            Nom = PublicVariablesPP.Nom,
            Prénom = PublicVariablesPP.Prenom,
            TempsFinal = PublicVariablesRP.Elapsed.ToString(@"mm\:ss"),
            Allure = $"{minutesPerKilometer:F2} m/km",
            Temps200 = PublicVariablesSP.time200.ToString(@"mm\:ss"),
            Temps400 = PublicVariablesSP.time400.ToString(@"mm\:ss"),
            Temps600 = PublicVariablesSP.time600.ToString(@"mm\:ss"),
            Précision = PublicVariablesRP.Averageshot.ToString("G2"),
            Passage1 = PublicVariablesRP.ShootResults[0].ToString("G"),
            Passage2 = PublicVariablesRP.ShootResults[1].ToString("G"),
            Passage3 = PublicVariablesRP.ShootResults[2].ToString("G")
        };

        var csvHelper = new CSVHelper(_csvFilePath);
        csvHelper.WriteUserData(userData);
    }

    
    private async void OnShareClicked(object sender, EventArgs e)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("BiathlonEstivaukan", "biathlon.vaukan@gmail.com"));
        message.To.Add(new MailboxAddress("", "fyruxyt@gmail.com"));
        message.To.Add(new MailboxAddress("", PublicVariablesPP.Email));
        message.Subject = $"Course de {PublicVariablesPP.Nom} {PublicVariablesPP.Prenom} ";
        var body = new TextPart("plain")
        {
            Text = "Body text here..." // Replace with your email body content
        };

        var attachment = new MimePart()
        {
            Content = new MimeContent(File.OpenRead(Path.Combine(FileSystem.AppDataDirectory, "user_data.csv")), ContentEncoding.Default),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = Path.GetFileName(Path.Combine(FileSystem.AppDataDirectory, "user_data.csv")) // Set the filename for the attachment
        };


        var multipart = new Multipart { body, attachment };
        message.Body = multipart;

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
    private async void OnTrackingClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SuiviPerf(), false);

    }


    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Profil_Page(), false);

    }
    
}