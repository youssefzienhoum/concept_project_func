using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Statistics ;

namespace functionalconcept.Analysis;

public static class DataAnalysis 
{

    public static double Mean(List<SaleRecord> saleRecords)
    {
        if (saleRecords == null || saleRecords.Count == 0)
            return 0;

        double sum = SumSalesRecursive(saleRecords, 0);
        return sum / saleRecords.Count;
    }

    private static double SumSalesRecursive(List<SaleRecord> saleRecords, int index)
    {
        if (index >= saleRecords.Count)
        {
            return 0;
        }

        return Convert.ToDouble(saleRecords[index].Sales) + SumSalesRecursive(saleRecords, index + 1);
    }

    public static double Median(List<SaleRecord> saleRecords)
    {
        var salesValues = saleRecords.Select(r => Convert.ToDouble(r.Sales));
        return Statistics.Median(salesValues);
    }

    public static double Variance(List<SaleRecord> saleRecords)
    {
        var salesValues = saleRecords.Select(r => Convert.ToDouble(r.Sales));
        return Statistics.Variance(salesValues);
    }
    public static double Correlation(List<SaleRecord> saleRecordsOne, List<SaleRecord> saleRecordsTwo)
    {
        if (saleRecordsOne.Count != saleRecordsTwo.Count)
            throw new ArgumentException("Both lists must have the same number of elements.");

        var salesOne = saleRecordsOne.Select(r => Convert.ToDouble(r.Sales));
        var salesTwo = saleRecordsTwo.Select(r => Convert.ToDouble(r.Sales));
        return MathNet.Numerics.Statistics.Correlation.Pearson(salesOne, salesTwo);
    }
}