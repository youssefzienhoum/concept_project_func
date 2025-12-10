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

                var ch = Console.ReadLine();

                if (string.IsNullOrEmpty(ch))
                {
                    Console.WriteLine("Input is empty");
                }
                else if (ch.Length == 1 && !char.IsLetter(ch[0]))
                {
                    Console.WriteLine("Valid - Not a letter");
                }
                else if (ch.Length == 1 && char.IsLetter(ch[0]))
                {
                    Console.WriteLine("Invalid - This is a letter");
                }
                else
                {
                    Console.WriteLine("Invalid - Input must be a single character");
                }

                var ch1 = Convert.ToInt32(ch);

                if (ch1 == 5)
                    break;

                var results = OutputDataFunctional.ProcessData(standardized, ch1);
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
