

namespace ImperativeConcept
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
        }
    }
}
