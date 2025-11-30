using System;
using System.Collections.Generic;
using System.Linq;

namespace functionalconcept.Analysis
{
    public static class DataAnalysis
    {
        public static double Mean(List<SaleRecord> saleRecords)
        {
            List<double> salesValues = new List<double>();
            double sum = 0;

            for (int i = 0; i < saleRecords.Count; i++)
            {
                salesValues.Add(Convert.ToDouble(saleRecords[i].Sales));
            }
            
            for (int i = 0; i < salesValues.Count; i++)
            {
                sum += salesValues[i];
            }

            return sum / salesValues.Count;
        }

        public static double Median(List<SaleRecord> saleRecords)
        {
            List<double> salesValues = new List<double>();

            for (int i = 0; i < saleRecords.Count; i++)
            {
                salesValues.Add(Convert.ToDouble(saleRecords[i].Sales));
            }
            salesValues.Sort();
            int count = salesValues.Count;

            if (count % 2 == 0)
            {
                return (salesValues[count/2] + salesValues[(count/2) - 1]) /2;
            }
            else
            {
                return salesValues[count/2];
            }
        }

        public static double Variance(List<SaleRecord> saleRecords)
        {
            double sum = 0;
            double sumVariance = 0; 
            int count = saleRecords.Count;
            for (int i = 0; i < count; i++)
            {
                sum += Convert.ToDouble(saleRecords[i].Sales);
            }
            var meanValue = sum/count;

            for (int i = 0; i < count; i++)
            {
                var value = Convert.ToDouble(saleRecords[i].Sales) - meanValue;
                sumVariance += Math.Pow(value,2);
            }
            return sumVariance/count;
        }

    }
}