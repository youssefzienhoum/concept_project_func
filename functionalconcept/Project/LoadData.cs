using functionalconcept.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace functionalconcept.Project
{
    public class LoadData
    {
        public static List<SaleRecord>? Loading()
        {
            // Read the JSON file
            var sale = File.ReadAllText("D:\\route\\C#\\ConceptProject\\functionalconcept\\DataSet\\dataset.json");

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
