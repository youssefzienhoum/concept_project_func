using System;
using System.Collections.Generic;
using System.Linq;
using functionalconcept.model;
using MathNet.Numerics.Statistics ;

namespace functionalconcept.Analysis;

public static class DataAnalysis 
{
    
    public static double Mean(List<SaleRecord> saleRecords)
    {
        var salesValues = saleRecords.Select(r => Convert.ToDouble(r.Sales));
        return Statistics.Mean(salesValues);
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