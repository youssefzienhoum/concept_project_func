using ImperativeConcept;
using ImpretiveConceptV2.Project;

List<SaleRecord>? salesRecords = LoadData.Loading();
var CleanedData = ImperativeMissingDataHandler.HandleMissingData(salesRecords);
var standardized = DataStandardizationImperative.StandardizeDataset(CleanedData);


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
