using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace functionalconcept.model
{
    public class AggregatedRegionSales
    {
        public string Region { get; set; }
        public int TotalSales { get; set; }

        public override string ToString()
        {
            return $"Region: {Region}, TotalSales: {TotalSales}";
        }
    }
}
