
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Functional_Data_Processing
{
    public class LoadData
    {
        public static List<SaleRecord>? Loading()
        {
            // Read the JSON file
            //var sale = File.ReadAllText("D:\\route\\C#\\ConceptProject\\functionalconcept\\DataSet\\dataset.json"); ==> Zenhom path 
            //var sale = File.ReadAllText("C:\\Users\\Dell\\source\\repos\\youssefzienhoum\\concept_project_func\\functionalconcept\\DataSet\\dataset.json"); ==> islam
            var sale = File.ReadAllText("E:\\Projects\\ConcepProject\\functionalconcept\\DataSet\\dataset.json");

            // Deserialize the JSON data
            var salesRecords = JsonConvert.DeserializeObject<List<SaleRecord>>(sale);

            if (salesRecords is not null)
            {
                return salesRecords;
            }
            else
            {
                return null;
            }
        }
    }
}
