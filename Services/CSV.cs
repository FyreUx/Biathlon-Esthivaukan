using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;

public class CSV
{
    private readonly string _filePath;

    public CSV(string filePath)
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
    }
}

public class UserData
{
    public string Nom { get; set; }
    public string Prénom { get; set; }
    public string TempsComplet { get; set; }
    public string Allure { get; set; }
    public string Email { get; set; }
}
