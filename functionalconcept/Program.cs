using functionalconcept.Analysis;
using functionalconcept.model;
using functionalconcept.Project;

namespace functionalconcept
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<SaleRecord>? salesRecords = LoadData.Loading();

            var standardized = DataStandardization.StandardizeSaleRecords(salesRecords);


            while (true)
            {
                Console.WriteLine("\nEnter Number Of Choice:");
                Console.WriteLine("1) Filter By Sales\n" +
                    "2) Compute Growth\n" +
                    "3) Aggregate By data Key\n" +
                    "4)Analyze data\n" +
                    "5) Exit");

                var ch = Convert.ToInt32(Console.ReadLine());

                if (ch == 5)
                    break;

                OutputData.ShowData(standardized, ch);
            }
        }
    }
}
