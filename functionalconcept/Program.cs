using functionalconcept.model;
using functionalconcept.Project;

namespace functionalconcept
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<SaleRecord>? salesRecords = Project.LoadData.Loading();
            var standardized = DataStandardization.StandardizeDataset(salesRecords);

            Console.WriteLine("=== Standardized Data ===");
            foreach (var item in standardized)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nEnter Number Of Choice:");
            Console.WriteLine("1) Aggregate By data Key\n");

            var ch = Convert.ToInt32(Console.ReadLine());

            switch (ch)
            {
                case 1:
                    var data =
                        (List<dynamic>)new DataTransformation()
                        .AggregateDataByKey(standardized);

                    foreach (var region in data)
                    {
                        Console.WriteLine(
                            $"Region : {region.Region} : TotalSales : {region.TotalSales}"
                        );
                    }
                    break;
            }
        }
    }
}
