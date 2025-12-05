using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpretiveConceptV2.Project
{
    public class AggregatedRegionSales
    {
        public string Region { get; set; }
        public decimal TotalSales { get; set; }
        public override string ToString()
        {
            return $"Region: {Region}, TotalSales: {TotalSales}";
        }
    }
}
