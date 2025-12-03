using functionalconcept.Analysis;
using functionalconcept.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text;
using System.Threading.Tasks;

namespace functionalconcept.Project
{
    public static class OutputData
    {
        private const string filePath = @"E:\Projects\ConceptProject\functionalconcept\OutputFiles";

        public static void ShowData(List<SaleRecord> standardized, int ch)
        {
            switch (ch)
            {
                case 1:
                    Console.WriteLine("Enter Sales Threshold:");
                    var threshold = Convert.ToDouble(Console.ReadLine());
                    var filteredData =
                        new DataTransformation()
                        .FilterBySales(standardized, threshold);

                    foreach (var record in filteredData)
                        Console.WriteLine(record);
                    break;
                case 2:
                    var growthData =
                        new DataTransformation()
                        .ComputeGrowth(standardized);

                    foreach (var record in growthData)
                        Console.WriteLine(record);
                    break;
                case 3:
                    var AggregateData =
                        new DataTransformation()
                        .AggregateDataByKey(standardized);

                    foreach (var record in AggregateData)
                        Console.WriteLine(record); 
                    break;
                case 4:
                    Console.WriteLine($"Mean Sales: {DataAnalysis.Mean(standardized)}");
                    Console.WriteLine($"Median Sales: {DataAnalysis.Median(standardized)}");
                    Console.WriteLine($"Variance of Sales: {DataAnalysis.Variance(standardized)}");
                    Console.WriteLine($"Correlation of Sales: {DataAnalysis.Correlation(
                        standardized.Take((int)standardized.Count / 2).ToList(),
                        standardized.Skip((int)standardized.Count / 2).ToList())}");
                    break;
            }
        }


        //public static void SaveToCsv<T>(List<T> data,string filename)
        //{
        //    var properties = typeof(T).GetProperties();
        //    var sb = new StringBuilder();

        //    // Header
        //    sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));

        //    // Rows
        //    foreach (var item in data)
        //    {
        //        var values = properties.Select(p => p.GetValue(item, null));
        //        sb.AppendLine(string.Join(",", values));
        //    }

        //    //WriteFileSafely(filePath+@$"\{filename}", sb.ToString());
        //    File.WriteAllText(filePath+@$"\{filename}", sb.ToString());
        //}

        ////public static void WriteFileSafely(string filePath, string content)
        //{
        //    string? directory = Path.GetDirectoryName(filePath);

        //    // Create directory if needed
        //    if (!Directory.Exists(directory))
        //        Directory.CreateDirectory(directory);

        //    // Just write the file — WriteAllText already creates it if missing
        //    File.WriteAllText(filePath, content);
        //}

    }
}
