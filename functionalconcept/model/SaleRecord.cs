using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace functionalconcept.model
{
    public class SaleRecord : Object
    {
        public string? Region { get; set; }
        public string? Sales { get; set; } = default!;
        public DateTime? Date { get; set; }

        ///    ده هيتحسب في مشروع 
        public double? Growth { get; set; }


        public override string ToString()
        {
            return $"Region: {Region}, Sales: {Sales}, Date: {Date?.ToString("yyyy-MM-dd")}, Growth: {Growth}";
        }
    }
   
}
