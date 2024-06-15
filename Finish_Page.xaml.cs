using MailKit.Net.Smtp;
using MimeKit;
using System;


namespace Biathlon_Esthivaukan;

public partial class Finish_Page : ContentPage
{
	public Finish_Page()
	{
		InitializeComponent();
	}

	private async void OnShareClicked(object sender, EventArgs e)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Your Name", "biathlon.vaukan@gmail.com"));
        message.To.Add(new MailboxAddress("Recipient Name", "fyruxyt@gmail.com"));
        message.Subject = "subject";

        message.Body = new TextPart("plain")
        {
            Text = "body"
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

}