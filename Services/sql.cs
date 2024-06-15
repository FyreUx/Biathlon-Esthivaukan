using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace YourNamespace
{
    // Modèle de données
    public class Runner
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
    }

    // Interface du service de base de données
    public interface IDatabaseService
    {
        Task<List<Runner>> GetRunnersAsync();
        Task<Runner> GetRunnerAsync(int id);
        Task<int> SaveRunnerAsync(Runner runner);
        Task<int> DeleteRunnerAsync(Runner runner);
    }

    // Implémentation du service de base de données
    public class DatabaseService : IDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "runners.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Runner>().Wait();
        }

        public Task<List<Runner>> GetRunnersAsync()
        {
            return _database.Table<Runner>().ToListAsync();
        }

        public Task<Runner> GetRunnerAsync(int id)
        {
            return _database.Table<Runner>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveRunnerAsync(Runner runner)
        {
            if (runner.Id != 0)
            {
                return _database.UpdateAsync(runner);
            }
            else
            {
                return _database.InsertAsync(runner);
            }
        }

        public Task<int> DeleteRunnerAsync(Runner runner)
        {
            return _database.DeleteAsync(runner);
        }
    }

    // Initialisation de la base de données dans App.xaml.cs
    public partial class App : Application
    {
        private static IDatabaseService _databaseService;

        public static IDatabaseService DatabaseService
        {
            get
            {
                if (_databaseService == null)
                {
                    _databaseService = new DatabaseService();
                }
                return _databaseService;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }

    // Page pour enregistrer les temps de course
    public partial class RunPage : ContentPage
    {
        public RunPage()
        {
            InitializeComponent();
        }

        private async void OnSaveTimeClicked(object sender, EventArgs e)
        {
            var runner = new Runner
            {
                Name = "Example Runner",
                Distance = 10.0, // Exemple de distance
                Time = PublicVariables.Elapsed,
                Date = DateTime.Now
            };

            await App.DatabaseService.SaveRunnerAsync(runner);
        }
    }

    // Page pour afficher les coureurs enregistrés
    public partial class RunnersPage : ContentPage
    {
        public RunnersPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            RunnersListView.ItemsSource = await App.DatabaseService.GetRunnersAsync();
        }
    }

    // Classe pour les variables publiques
    public static class PublicVariables
    {
        public static TimeSpan Elapsed { get; set; }
    }

    // AppShell.xaml pour la navigation
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RunPage), typeof(RunPage));
            Routing.RegisterRoute(nameof(RunnersPage), typeof(RunnersPage));
        }
    }
}
