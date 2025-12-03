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
                        (List<dynamic>)new DataTransformation()
                        .FilterBySales(standardized, threshold);
                    SaveToCsv(filteredData, "filteredData");
                    break;
                case 2:
                    var growthData =
                        (List<dynamic>)new DataTransformation()
                        .ComputeGrowth(standardized);
                    SaveToCsv(growthData, "growthData");
                    break;
                case 3:
                    var AggregateData =
                        (List<dynamic>)new DataTransformation()
                        .AggregateDataByKey(standardized);

                    SaveToCsv(AggregateData, "AggregateData");
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


        public static void SaveToCsv<T>(List<T> data,string filename)
        {
            var properties = typeof(T).GetProperties();
            var sb = new StringBuilder();

            // Header
            sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));

            // Rows
            foreach (var item in data)
            {
                var values = properties.Select(p => p.GetValue(item, null));
                sb.AppendLine(string.Join(",", values));
            }

            File.WriteAllText(filePath+@$"\{filename}", sb.ToString());
        }



    }
}
