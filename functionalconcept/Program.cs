using functionalconcept.model;
using functionalconcept.Project;

namespace functionalconcept
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<SaleRecord>? salesRecords = Project.LoadData.Loading();
            foreach (var item in salesRecords)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Enter Number Of Choice:");
            Console.WriteLine("1) Aggregate By data Key\n" +
                "");
            var ch = Convert.ToInt32(Console.ReadLine());

            switch(ch)
            {
                case 1:
                    var data = (List<dynamic>)new DataTransformation().AggregateDataByKey(salesRecords);
                    foreach (var region in data)
                    {
                        Console.WriteLine($"Region : {region.Region} : " +
                            $"TotalSales : {region.TotalSales}");
                    }
                    break;
            }
        }
    }
}
