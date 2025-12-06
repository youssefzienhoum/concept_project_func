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
            Dictionary<string, decimal> regionSales = new Dictionary<string, decimal>();

            foreach (var record in saleRecords)
            {
                var region = record.Region;
                decimal sales = Convert.ToDecimal(record.Sales);

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

        public List<SaleRecord> FilterBySales_Imperative(List<SaleRecord> saleRecords, double threshold)
        {
            List<SaleRecord> result = new List<SaleRecord>();

            foreach (SaleRecord r in saleRecords)
            {
                double salesValue;

                if (double.TryParse(r.Sales, out salesValue))
                {
                    if (salesValue > threshold)
                    {
                        SaleRecord record = new SaleRecord
                        {
                            Region = r.Region,
                            Sales = r.Sales,
                            Date = r.Date
                        };

                        result.Add(record);
                    }
                }
            }

            return result;
        }

        public List<SaleRecord> ComputeGrowth_Imperative(List<SaleRecord> saleRecords)
        {
            List<SaleRecord> result = new List<SaleRecord>();

            foreach (SaleRecord r in saleRecords)
            {
                double salesValue = 0;

                double.TryParse(r.Sales, out salesValue);

                SaleRecord record = new SaleRecord
                {
                    Region = r.Region,
                    Sales = r.Sales,
                    Date = r.Date,
                    Growth = salesValue * 0.10
                };

                result.Add(record);
            }

            return result;
        }


    }
}
