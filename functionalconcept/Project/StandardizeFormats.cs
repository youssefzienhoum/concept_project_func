using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using functionalconcept.model;

namespace functionalconcept.Project
{
    public static class DataStandardization
    {
        private static List <SaleRecord> StandardizeRow (List<SaleRecord> raw)
        {
            foreach(var records in raw)
            {
                Region = StandardizeRegion(records.Region);
                Sales = StandardizeSales(records.Sales);
                Date = StandardizeDate(records.Date);
                Growth = records.Growt;
            }
        }
        private static string StandardizeRegion(string region)
        {
            return region.Trim();
        }
    
        private static string StandardizeSales(string sales)
        {
            if (double.TryParse(sales, out double value))
                return value.ToString("F2", CultureInfo.InvariantCulture);
        }

        private static DateTime StandardizeDate(DateTime? date)
        {
            return date.Value;
        }
    }
}
