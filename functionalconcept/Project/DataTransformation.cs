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
    }
}
