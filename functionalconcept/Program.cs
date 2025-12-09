using Functional_Data_Processing;
using functionalconcept.Analysis;
using functionalconcept.Project;
using FunctionalConcept;

namespace functionalconcept
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<SaleRecord>? salesRecords = LoadData.Loading();
            var CleanedData = HandlingMissingDataFunctional.HandleMissingData(salesRecords);
            var standardized = DataStandardization.StandardizeSaleRecords(CleanedData);


            while (true)
            {
                Console.WriteLine("\nEnter Number Of Choice:");
                Console.WriteLine("1) Filter By Sales\n" +
                    "2) Compute Growth\n" +
                    "3) Aggregate By data Key\n" +
                    "4)Analyze data\n" +
                    "5) Exit\n");

                var ch = Convert.ToInt32(Console.ReadLine());

                if (ch == 5)
                    break;

                var results = OutputDataFunctional.ProcessData(standardized, ch);
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
