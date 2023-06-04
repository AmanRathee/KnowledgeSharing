using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using CsvHelper;
using CsvHelper.Configuration;
using CSVHelper.Models;

class Program
{
    static readonly CancellationTokenSource cts = new CancellationTokenSource();
    static void Main(string[] args)
    {        
        List<Person> people = ReadCsv(GetCsvFilePath());
        people.ForEach(i => Console.WriteLine("{0}-{1}-{2}", i.Name, i.Age, i.Country));
        Console.ReadKey();
    }
    private static string GetCsvFilePath()
    {
        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
        return Path.Combine(projectDirectory, "wwwroot", "person.csv");
    }
    private static List<Person> ReadCsv(string filePath)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            var records = csv.GetRecords<Person>();
            return records.ToList();
        }
    }

}