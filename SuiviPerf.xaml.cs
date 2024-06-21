namespace Biathlon_Esthivaukan;

public partial class SuiviPerf : ContentPage
{
    public string? ButtonText1 { get; set; }
    public string? ButtonText2 { get; set; }
    public string? ButtonText3 { get; set; }
    public string? ButtonText4 { get; set; }
    
    private readonly string _csvFilePath;
    public SuiviPerf()
	{
		InitializeComponent();

        _csvFilePath = Path.Combine(FileSystem.AppDataDirectory, "user_data.csv");

        ReadUserData();
        

    }

    private void ReadUserData()
    {

        if (!File.Exists(_csvFilePath))
        {
            Console.WriteLine("CSV file does not exist.");
            Btn1.IsVisible = false;
            Btn1.IsEnabled = false;
            Btn2.IsVisible = false;
            Btn2.IsEnabled = false;
            Btn3.IsVisible = false;
            Btn3.IsEnabled = false;
            Btn4.IsVisible = false;
            Btn4.IsEnabled = false;
            return;
        }
        var csvHelper = new CSVHelper(_csvFilePath);
        var userDataList = csvHelper.ReadAllUserData();
        int length = userDataList.Count;
        
        if (length == 0)
        {
            Console.WriteLine("CSV file is empty.");
            Btn1.IsVisible = false;
            Btn1.IsEnabled = false;
            Btn2.IsVisible = false;
            Btn2.IsEnabled = false;
            Btn3.IsVisible = false;
            Btn3.IsEnabled = false;
            Btn4.IsVisible = false;
            Btn4.IsEnabled = false;
            return;
        }
        var userData_1 = length > 1 ? userDataList[length -1] : null;
        var userData_2 = length > 2 ? userDataList[length - 2] : null;
        var userData_3 = length > 3 ? userDataList[length - 3] : null;
        var userData_4 = length > 4 ? userDataList[length - 4] : null;

        if (userData_1 != null)
        {
            PublicVariablesSP.ud1 = userData_1;
            ButtonText1 = $"Run n° 1 Temps Total {userData_1[3]}";

        }
        else
        {
            Console.WriteLine("userData_1 is null");
            Btn1.IsVisible = false;
            Btn1.IsEnabled = false;

        }
        if (userData_2 != null)
        {
            PublicVariablesSP.ud2 = userData_2;
            ButtonText2 = $"Run n° 2 Temps Total {userData_2[3]}";
        }
        else
        {
            Console.WriteLine("userData_2 is null");
            Btn2.IsVisible = false;
            Btn2.IsEnabled = false;
        }
        if (userData_3 != null)
        {
            PublicVariablesSP.ud3 = userData_3;
            ButtonText3 = $"Run n° 3 Temps Total {userData_3[3]}";
        }
        else
        {
            Console.WriteLine("userData_3 is null");
            Btn3.IsVisible = false;
            Btn3.IsEnabled = false;
        }
        if (userData_4 != null)
        {
            PublicVariablesSP.ud4 = userData_4;
            ButtonText4 = $"Run n° 4 Temps Total {userData_4[3]}";
        }
        else
        {
            Console.WriteLine("userData_4 is null");
            Btn4.IsVisible = false;
            Btn4.IsEnabled = false;
        }

        BindingContext = this;

    }

    public static class PublicVariablesSP
    {
        public static List<string>? ud1 { get; set; }

        public static List<string>? ud2 { get; set; }

        public static List<string>? ud3 { get; set; }

        public static List<string>? ud4 { get; set; }

    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage(),false);

    }

    private async void OnButton1Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new History(PublicVariablesSP.ud1), false);

    }
    private async void OnButton2Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new History(PublicVariablesSP.ud2), false);

    }
    private async void OnButton3Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new History(PublicVariablesSP.ud3), false);

    }
    private async void OnButton4Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new History(PublicVariablesSP.ud4), false);

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