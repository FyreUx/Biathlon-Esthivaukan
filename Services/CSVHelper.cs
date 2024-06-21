using CsvHelper;
using CsvHelper.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Text;

public class CSVHelper
{
    private readonly string _filePath;

    public CSVHelper(string filePath)
    {
        _filePath = filePath;
    }

    public void WriteUserData(UserData userData)
    {
        EnsureFileExists();


        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = !File.Exists(_filePath) || new FileInfo(_filePath).Length == 0
        };

        var lineCount = File.ReadLines(_filePath).Count();
        Debug.WriteLine("Number of lines in the file: " + lineCount);
        if (lineCount > 5)
        {
            Debug.WriteLine("File is too big, clearing it");
            File.WriteAllText(_filePath, string.Empty);
            using (var stream = File.Open(_filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteHeader<UserData>();
                csv.NextRecord();
            }
        }


        StringBuilder result = new StringBuilder();

        // Append properties of the user object
        result.Append($"{userData.Nom},");
        result.Append($"{userData.Prénom},");
        result.Append($"{userData.Allure},");
        result.Append($"{userData.TempsFinal},");
        result.Append($"{userData.Temps200},");
        result.Append($"{userData.Temps400},");
        result.Append($"{userData.Temps600},");
        result.Append($"{userData.Passage1},");
        result.Append($"{userData.Passage2},");
        result.Append($"{userData.Passage3},");
        result.Append($"{userData.Précision}");


        string finalString = result.ToString();
        // Read all existing lines to check if userDataLine already exists
        Console.WriteLine(finalString);  
        
        bool lineExists = CompareCsvLine(_filePath, finalString);

        Debug.WriteLine("Line exists: " + lineExists.ToString());
        if (!lineExists)
        {
            using (var stream = File.Open(_filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                if (config.HasHeaderRecord)
                {
                    csv.WriteHeader<UserData>();
                    csv.NextRecord();
                }
                csv.WriteRecord(userData);
                csv.NextRecord();
            }
        }
        else
        {
            Debug.WriteLine("Line already exists in the file. Skipping write operation.");
        }
    }

    static bool CompareCsvLine(string csvFilePath, string givenLine)
    {
        bool found = false;

        using (TextFieldParser parser = new TextFieldParser(csvFilePath))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            Debug.WriteLine("Comparing: " + givenLine);
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                if (fields != null)
                {
                    string csvLine = string.Join(",", fields);
                    Debug.WriteLine("TO: " + csvLine);
                    if (csvLine == givenLine)
                    {
                        found = true; // Lines match
                        break; // Exit loop early since we found a match
                    }
                }
            }
        }

        return found; // Return whether the line was found
    }




    public List<List<string>> ReadAllUserData()
    {
        List<List<string>> userDataList = new List<List<string>>();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true // Assuming your CSV file has headers
        };

        using (var fileStream = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (var reader = new StreamReader(fileStream))
        using (var csv = new CsvReader(reader, config))
        {
            while (csv.Read())
            {
                List<string> userData = new List<string>();

                for (int j = 0; j < 11; j++) // Assuming there are 11 fields in each row
                {
                    if (!csv.TryGetField<string>(j, out string fieldValue))
                    {
                        break; // Exit the loop if no more fields are available
                    }
                    userData.Add(csv.GetField<string>(j));
                }

                userDataList.Add(userData);

                if (userDataList.Count >= 5)
                {
                    break; // Stop reading after 5 records
                }
            }
        }

        return userDataList;
    }



    private void EnsureFileExists()
    {
        if (!File.Exists(_filePath))
        {
            using (var stream = File.Create(_filePath))
            {
                // File created
            }
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
}
