using Functional_Data_Processing;
using System.Text;

namespace ImpretiveConceptV2.Project
{
    public static class OutputDataImpretive
    {
        public static void ShowData(List<SaleRecord> standardized, int ch)
        {
            switch (ch)
            {
                case 1:
                    Console.WriteLine("Enter Sales Threshold:");
                    var threshold = Convert.ToDouble(Console.ReadLine());
                    var filteredData =
                        new DataTransformation()
                        .FilterBySales_Imperative(standardized, threshold);
                    Console.WriteLine("\n====================>>> Filter Sales <<<====================");
                    foreach (var record in filteredData)
                        Console.WriteLine(record);
                    Console.WriteLine();
                    break;
                case 2:
                    var growthData =
                        new DataTransformation()
                        .ComputeGrowth_Imperative(standardized);
                    Console.WriteLine("\n====================>>> Groth Of Sales <<<====================");
                    foreach (var record in growthData)
                        Console.WriteLine(record);
                    Console.WriteLine();
                    break;
                case 3:
                    var AggregateData = new DataTransformation()
                        .AggregateDataByKey_Imperative(standardized);
                    Console.WriteLine("\n====================>>> Aggregate Sales <<<====================");
                    foreach (var record in AggregateData)
                        Console.WriteLine(record);
                    Console.WriteLine();
                    break;
                case 4:
                    Console.WriteLine("\n====================>>> Analyzed Data <<<====================");
                    Console.WriteLine($"Mean Sales: {DataAnalysis.Mean(standardized)}");
                    Console.WriteLine($"Median Sales: {DataAnalysis.Median(standardized)}");
                    Console.WriteLine($"Variance of Sales: {DataAnalysis.Variance(standardized)}");
                    Console.WriteLine($"Correlation of Sales: {DataAnalysis.Correlation(
                        standardized.Take((int)standardized.Count / 2).ToList(),
                        standardized.Skip((int)standardized.Count / 2).ToList())}\n");
                    break;
            }
        }
    }
}
