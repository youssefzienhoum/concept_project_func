using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace functionalconcept.Analysis
{
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

    }
}