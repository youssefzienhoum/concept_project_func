using System.Text;

namespace ImpretiveConceptV2.Project
{
    public static class OutputData
    {
        public static void ShowData(List<SaleRecord> standardized, int ch)
        {
            switch (ch)
            {
                //case 1:
                //    Console.WriteLine("Enter Sales Threshold:");
                //    var threshold = Convert.ToDouble(Console.ReadLine());
                //    var filteredData =
                //        (List<dynamic>)new DataTransformation()
                //        .FilterBySales(standardized, threshold);
                //    foreach (var record in filteredData)
                //        Console.WriteLine(record); break;
                //case 2:
                //    var growthData =
                //        (List<dynamic>)new DataTransformation()
                //        .ComputeGrowth(standardized);
                //foreach (var record in growthData)
                //                Console.WriteLine(record);
                //    break;
                case 3:
                    var AggregateData = new DataTransformation()
                        .AggregateDataByKey_Imperative(standardized);

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
    }
}
