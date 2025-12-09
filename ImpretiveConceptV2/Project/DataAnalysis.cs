using Functional_Data_Processing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImpretiveConceptV2.Project
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

        public static double Correlation(List<SaleRecord> saleRecordsX, List<SaleRecord> saleRecordsY)
        {
            if (saleRecordsX.Count != saleRecordsY.Count)
            {
                throw new ArgumentException("The two lists must have the same number of elements.");
            }
            int n = saleRecordsX.Count;
            double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0, sumY2 = 0;
            for (int i = 0; i < n; i++)
            {
                double x = Convert.ToDouble(saleRecordsX[i].Sales);
                double y = Convert.ToDouble(saleRecordsY[i].Sales);
                sumX += x;
                sumY += y;
                sumXY += x * y;
                sumX2 += x * x;
                sumY2 += y * y;
            }
            double numerator = n * sumXY - sumX * sumY;
            double denominator = Math.Sqrt((n * sumX2 - sumX * sumX) * (n * sumY2 - sumY * sumY));
            if (denominator == 0)
            {
                throw new InvalidOperationException("Denominator in correlation calculation is zero.");
            }
            return numerator / denominator;
        }
    }
}