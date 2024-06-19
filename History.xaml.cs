using static Biathlon_Esthivaukan.SuiviPerf;



namespace Biathlon_Esthivaukan;

public partial class History : ContentPage
{

    public static List<string>? udx;

    public History(List<string>? ud)
	{
        InitializeComponent();
        udx = ud;
        Allure.Text = ud[2] ;
        TempsFinal.Text = ud[3] ;
        Temps200.Text = ud[4];
        Temps400.Text = ud[5];
        Temps600.Text = ud[6];
        P1.Text = ud[7];
        P2.Text = ud[8];
        P3.Text = ud[9];
        Précision.Text = ud[10];
    }


    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Profil_Page(), false);

    }
    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage(), false);

    }

    private async void OnTrackingClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SuiviPerf(), false);

    }
}
