using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using functionalconcept.model;

namespace functionalconcept.Project;

public static class DataStandardization
{
    private static List<SaleRecord> StandardizeRow(List<SaleRecord> raw)
    {
        foreach (var records in raw)
        {
            var Region = StandardizeRegion(records.Region);
            var Sales = StandardizeSales(records.Sales);
            var Date = StandardizeDate(records.Date);
            var Growth = records.Growth;
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
