using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.DataProcessors
{
    public static class CabDataProcessor
    {
        public static void CleanUpData() 
        {
            string inputFilePath = "../../../../SampleCabDataETLProject.DAL/Seeding/sample-cab-data.csv";
            string uniqueFilePath = "../../../../SampleCabDataETLProject.DAL/Seeding/unique.csv";
            string duplicatesFilePath = "../../../../SampleCabDataETLProject.DAL/Seeding/duplicates.csv";

            List<string[]> rows = ReadCsv(inputFilePath);
            rows = rows.Skip(1).ToList();

            foreach (var row in rows)
            {
                if (row[6] == "N")
                    row[6] = "No";
                else if (row[6] == "Y")
                    row[6] = "Yes";
                else row[6] = "NaN";
            }

            var grouped = rows.GroupBy(row => $"{row[1]}|{row[2]}|{row[3]}");

            List<string[]> uniqueRecords = new();
            List<string[]> duplicateRecords = new();

            foreach (var group in grouped)
            {
                var records = group.ToList();
                uniqueRecords.Add(records.First());

                if (records.Count > 1)
                {
                    duplicateRecords.AddRange(records.Skip(1));
                }
            }

            WriteCsv(uniqueFilePath, uniqueRecords);
            WriteCsv(duplicatesFilePath, duplicateRecords);

            Console.WriteLine("Data processing complete! Unique and duplicate records are saved.");
        }

        public static List<string[]> ReadCsv(string filePath)
        {
            var lines = new List<string[]>();
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(',');

                        if (!values.Any(string.IsNullOrWhiteSpace))
                        {
                            lines.Add(values);
                        }
                    }
                }
            }
            return lines;
        }

        private static void WriteCsv(string filePath, List<string[]> data)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var row in data)
                {
                    writer.WriteLine(string.Join(",", row));
                }
            }
        }
    }
}
