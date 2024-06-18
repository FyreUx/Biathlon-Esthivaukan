namespace Biathlon_Esthivaukan;

public partial class SuiviPerf : ContentPage
{
    
    private readonly string _csvFilePath;
    public SuiviPerf()
	{
		InitializeComponent();

        _csvFilePath = Path.Combine(FileSystem.AppDataDirectory, "user_data.csv");
        ReadUserData();

    }

    private void ReadUserData()
    {
        var csvHelper = new CSVHelper(_csvFilePath);
        var userDataList = csvHelper.ReadAllUserData();

        foreach (var userData in userDataList)
        {
            foreach (var field in userData)
            {
                Console.Write(field + "\t");
            }
            Console.WriteLine();
        }
    }


        private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage(),false);

    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Recap_Page(), false);

    }

    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Profil_Page(), false);

    }

    private async void OnHistoryClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SuiviPerf(), false);

    }
}