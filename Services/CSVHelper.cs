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
        
    }

    public List<List<string>> ReadAllUserData()
    {
        List<List<string>> userDataList = new List<List<string>>();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true // Assuming your CSV file has headers
        };

        using (var reader = new StreamReader(_filePath))
        using (var csv = new CsvReader(reader, config))
        {
            while (csv.Read())
            {
                List<string> userData = new List<string>();

                // When only want to showcase 5 rows of the csv file
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 11; j++) // Assuming there are 11 fields in each row
                    {
                        if (!csv.TryGetField<string>(j, out string fieldValue))
                        {
                            break; // Exit the inner loop if no more fields are available
                        }
                        userData.Add(csv.GetField<string>(j));
                    }
                    
                    userDataList.Add(userData);
                }

                
            }
        }

        return userDataList;

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

}
