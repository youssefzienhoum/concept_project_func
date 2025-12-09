using Functional_Data_Processing;
using ImpretiveConceptV2.Project;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ImperativeConcept
{

    public static class ImperativeMissingDataHandler
    {
        private static readonly string DefaultDate = "1900-01-01";


        public static List<SaleRecord> HandleMissingData(List<SaleRecord> rawData)
        {
            if (rawData == null) return new List<SaleRecord>();

            string DefaultRegion = GetRegionMode(rawData);

            var validSales = new List<decimal>();

            var parsedSales = new List<decimal?>();

            foreach (var r in rawData)
            {
                decimal? s = ParseSales(r.Sales);
                parsedSales.Add(s);

                if (s.HasValue)
                    validSales.Add(s.Value);
            }



            decimal mean = 0m;
            if (validSales.Count > 0)
            {
                decimal sum = 0m;
                foreach (var v in validSales) sum += v;
                mean = Math.Round(sum / validSales.Count, 2);
            }

            var cleaned = new List<SaleRecord>(rawData.Count);

            for (int i = 0; i < rawData.Count; i++)
            {
                var r = rawData[i];
                var cr = new SaleRecord();

                cr.Region = string.IsNullOrWhiteSpace(r?.Region)
                                ? DefaultRegion
                                : r.Region.Trim();

                cr.Sales = (parsedSales[i] ?? mean).ToString();

                var normalized = NormalizeDate(r?.Date.ToString()!);

                if (normalized != null)
                    cr.Date = DateTime.ParseExact(normalized, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                else
                    cr.Date = DateTime.ParseExact(DefaultDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                cleaned.Add(cr);
            }
            return cleaned;
        }


        private static string GetRegionMode(List<SaleRecord> rawData)
        {
            var freq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var r in rawData)
            {
                if (!string.IsNullOrWhiteSpace(r?.Region))
                {
                    var reg = r.Region.Trim();

                    if (freq.ContainsKey(reg))
                        freq[reg]++;
                    else
                        freq[reg] = 1;
                }
            }


            string mode = null;
            int max = -1;

            foreach (var kv in freq)
            {
                if (kv.Value > max)
                {
                    max = kv.Value;
                    mode = kv.Key;
                }
            }

            return mode;
        }
        private static decimal? ParseSales(object raw)
        {
            if (raw == null) return null;

            if (raw is decimal d) return d;
            if (raw is double db) return Convert.ToDecimal(db);

            if (raw is float f) return Convert.ToDecimal(f);
            if (raw is long l) return Convert.ToDecimal(l);
            if (raw is int i) return Convert.ToDecimal(i);



            var s = raw as string;
            if (s != null)
            {
                s = s.Trim();
                if (string.IsNullOrEmpty(s)) return null;

                if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var val))


                    return val;

                if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                    return val;
                return null;
            }

            return null;
        }

        private static string NormalizeDate(string rawDate)
        {
            if (string.IsNullOrWhiteSpace(rawDate)) return null;

            rawDate = rawDate.Trim();


            DateTime dt;

            // common formats to try
            var formats = new[]
            {
                "yyyy-MM-dd", "yyyy/MM/dd", "yyyy-M-d", "yyyy/M/d",
                "MM/dd/yy", "M/d/yy", "MM/dd/yyyy", "M/d/yyyy",
                "dd/MM/yy", "dd/MM/yyyy", "d/M/yy", "d/M/yyyy",
                "yyyyMMdd"
            };

            foreach (var fmt in formats)
            {
                if (DateTime.TryParseExact(rawDate, fmt, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    return dt.ToString("yyyy-MM-dd");
            }

            if (DateTime.TryParse(rawDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) ||
                DateTime.TryParse(rawDate, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
            {
                return dt.ToString("yyyy-MM-dd");
            }

            return null;
        }
    }
}
