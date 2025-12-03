using functionalconcept.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace functionalconcept.Project
{
    public class DataTransformation
    {
        public List<AggregatedRegionSales> AggregateDataByKey(List<SaleRecord> saleRecords)
        {
            return saleRecords.GroupBy(r => r.Region)
            .Select(g => new AggregatedRegionSales
            {
                Region = g.Key,
                TotalSales = g.Sum(x => (int)Convert.ToDecimal(x.Sales))
            })
            .ToList()!;
        } 

        public List<SaleRecord> FilterBySales(List<SaleRecord> saleRecords, double threshold)
        {
            var result = saleRecords
                .Where(r => double.TryParse(r.Sales, out double s) && s > threshold)
                .Select(r => new SaleRecord
                {
                    Region = r.Region,
                    Sales = r.Sales,
                    Date = r.Date
                })
                .ToList();

            return result.ToList();
        }

        public List<SaleRecord> ComputeGrowth(List<SaleRecord> saleRecords)
        {
            var result = saleRecords
                .Select(r =>
                {
                    double.TryParse(r.Sales, out double s);
                    return new SaleRecord
                    {
                        Region = r.Region,
                        Sales = r.Sales,
                        Date = r.Date,
                        Growth = s * 0.10
                    };
                })
                .ToList();

            return result.ToList();
        }

    }
}
