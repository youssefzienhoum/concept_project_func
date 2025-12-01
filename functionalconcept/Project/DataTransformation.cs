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
        public List<object> AggregateDataByKey(List<SaleRecord> saleRecords)
        {
            var result = saleRecords.GroupBy(r => r.Region)
            .Select(g => new
            {
                Region = g.Key,
                TotalSales = g.Sum(x => Convert.ToInt32(x.Sales))
            })
            .ToList()!;

            return result.Cast<object>().ToList();
        } 

        public List<object> FilterBySales(List<SaleRecord> saleRecords, double threshold)
        {
            var result = saleRecords
                .Where(r => double.TryParse(r.Sales, out double s) && s > threshold)
                .Select(r => new
                {
                    Region = r.Region,
                    Sales = r.Sales,
                    Date = r.Date
                })
                .ToList();

            return result.Cast<object>().ToList();
        }

        public List<object> ComputeGrowth(List<SaleRecord> saleRecords)
        {
            var result = saleRecords
                .Select(r =>
                {
                    double.TryParse(r.Sales, out double s);
                    return new
                    {
                        Region = r.Region,
                        Sales = r.Sales,
                        Date = r.Date,
                        Growth = s * 0.10
                    };
                })
                .ToList();

            return result.Cast<object>().ToList();
        }

    }
}
