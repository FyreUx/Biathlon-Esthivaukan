namespace Biathlon_Esthivaukan;

public partial class SuiviPerf : ContentPage
{
	public SuiviPerf()
	{
		InitializeComponent();

    }


    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage(),false);

    }

    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Profil_Page(), false);

    }

    private async void OnFinishClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Finish_Page(), false);

    }
}