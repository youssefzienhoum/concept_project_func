using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace functionalconcept.Project;

public static class DataStandardization
{
    public static List<SaleRecord> StandardizeSaleRecords(List<SaleRecord> SaleRecords)
    {
        if (SaleRecords == null || SaleRecords.Count == 0)
            return SaleRecords;

        StandardizeRegion(SaleRecords[0].Region);
        StandardizeSales(SaleRecords[0].Sales);
        StandardizeDate(SaleRecords[0].Date);

        if (SaleRecords.Count > 1)
            StandardizeSaleRecords(SaleRecords.Skip(1).ToList());

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
