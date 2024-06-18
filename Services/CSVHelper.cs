using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

public class CSVHelper
{
    private readonly string _filePath;

    public CSVHelper(string filePath)
    {
        _filePath = filePath;
    }

    public void WriteUserData(UserData userData)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = !File.Exists(_filePath)
        };

        using (var stream = File.Open(_filePath, FileMode.Append))
        using (var writer = new StreamWriter(stream))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteRecord(userData);
            csv.NextRecord();
        }
        var lines = File.ReadAllLines(_filePath);
        /*
        if (lines.Length > 5)
        {
            File.WriteAllText(_filePath, string.Empty); // Vide le fichier
        }

        // Réécrire l'en-tête si nécessaire après avoir vidé le fichier
        if (!File.Exists(_filePath))
        {
            using (var writer = new StreamWriter(_filePath, true))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteHeader<UserData>();
                csv.NextRecord();
            }
        }
        */
    }
}

public class UserData
{
    public string Nom { get; set; }
    public string Prénom { get; set; }
    public string Allure { get; set; }
    public string TempsFinal { get; set; }
    public string Temps200 { get; set; }
    public string Temps400 { get; set; }
    public string Temps600 { get; set; }
    public string Passage1 { get; set; }
    public string Passage2 { get; set; }
    public string Passage3 { get; set; }
    public string Précision { get; set; }

}
