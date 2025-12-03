using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpretiveConceptV2.Project
{
    public class DataTransformation
    {
        public List<AggregatedRegionSales> AggregateDataByKey_Imperative(List<SaleRecord> saleRecords)
        {
            Dictionary<string, int> regionSales = new Dictionary<string, int>();

            foreach (var record in saleRecords)
            {
                var region = record.Region;
                int sales = Convert.ToInt32(record.Sales);

                if (regionSales.ContainsKey(region))
                {
                    regionSales[region] += sales;
                }
                else
                {
                    regionSales[region] = sales;
                }
            }

            List<AggregatedRegionSales> result = new List<AggregatedRegionSales>();

            foreach (var kvp in regionSales)
            {
                result.Add(new AggregatedRegionSales
                {
                    Region = kvp.Key,
                    TotalSales = kvp.Value
                });
            }

            return result;
        }
    }
}
