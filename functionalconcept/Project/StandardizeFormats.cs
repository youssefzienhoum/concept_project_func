using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using functionalconcept.model;

namespace functionalconcept.Project;

public static class DataStandardization
{
    public static List<SaleRecord> StandardizeSaleRecords(List<SaleRecord> SaleRecords)
    {
        foreach (var record in SaleRecords)
        {
            StandardizeRegion(record.Region);
            StandardizeSales(record.Sales);
            StandardizeDate(record.Date);
            var Growth = record.Growth;
        }
        return SaleRecords;
    }
    private static string StandardizeRegion(string region)
    {
        return region.Trim();
    }

    private static string StandardizeSales(string sales)
    {
        if (double.TryParse(sales, out double value))
            return value.ToString("F2", CultureInfo.InvariantCulture);
        return sales;
    }

    private static DateTime StandardizeDate(DateTime? date)
    {
        return date.Value;
    }
}
