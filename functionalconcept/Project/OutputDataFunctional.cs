using functionalconcept.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace functionalconcept.Project
{
    public class OutputDataFunctional
    {
        public static IEnumerable<string> ProcessData(List<SaleRecord> standardized, int ch)
        {
            var transform = new DataTransformation();

            return ch switch
            {
                1 => transform.FilterBySales(standardized, 1).Select(sr => sr.ToString()),
                2 => transform.ComputeGrowth(standardized).Select(sr => sr.ToString()),
                3 => transform.AggregateDataByKey(standardized).Select(ar => ar.ToString()),
                4 => new[]
                    {
                        $"Mean Sales: {DataAnalysis.Mean(standardized)}",
                        $"Median Sales: {DataAnalysis.Median(standardized)}",
                        $"Variance: {DataAnalysis.Variance(standardized)}",
                        $"Correlation: {DataAnalysis.Correlation(
                            standardized.Take(standardized.Count / 2).ToList(),
                            standardized.Skip(standardized.Count / 2).ToList())}"
                    },
                _ => Enumerable.Empty<string>(),
            };
        }
    }
}
