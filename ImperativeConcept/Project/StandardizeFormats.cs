using System;
using System.Collections.Generic;
using System.Globalization;
using functionalconcept.model;

namespace functionalconcept.Project
{
    public class DataStandardizationImperative
    {
        public List<SaleRecord> StandardizeDataset(List<SaleRecord> records)
        {
            for (int i = 0; i < records.Count; i++)
            {
                var row = records[i];
                row.Region = row.Region.Trim();
                if (double.TryParse(row.Sales, NumberStyles.Any, 
                    CultureInfo.InvariantCulture, out double value))
                {
                    row.Sales = value.ToString("F2", CultureInfo.InvariantCulture);
                }
                if (row.Date != null)
                {
                    row.Date = DateTime.Now;
                }

                records[i] = row;
            }

            return records;
        }
    }
}
