using System;
using System.Collections.Generic;
using functionalconcept.model;
using functionalconcept.Project;

namespace functionalconcept
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<SaleRecord>? salesRecords = LoadData.Loading();

            var standardizer = new DataStandardizationImperative();
            var standardized = standardizer.StandardizeDataset(salesRecords);

            Console.WriteLine("=== Standardized Data ===");
            foreach (var item in standardized)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nEnter Number Of Choice:");
            Console.WriteLine("1) Aggregate By Region");

            var ch = Convert.ToInt32(Console.ReadLine());

            switch (ch)
            {
                case 1:
                    var aggregated =
                        new DataTransformation().AggregateDataByKey(standardized);

                    foreach (var region in aggregated)
                    {
                        Console.WriteLine(
                            $"Region: {region.Region}, TotalSales: {region.TotalSales}"
                        );
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}
