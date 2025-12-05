using ImpretiveConceptV2.Project;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ImperativeConcept
{

    public static class ImperativeMissingDataHandler
    {
        private static readonly string DefaultRegion = "Unknown";
        private static readonly string DefaultDate = "1900-01-01";

        // Public API
        public static List<SaleRecord> HandleMissingData(List<SaleRecord> rawData)
        {
            if (rawData == null) return new List<SaleRecord>();

            // 1) First pass: parse sales and collect valid decimals
            var validSales = new List<decimal>();
            var parsedSales = new List<decimal?>(); // aligned with rawData indices

            foreach (var r in rawData)
            {
                decimal? s = ParseSales(r.Sales);  
                parsedSales.Add(s);

                if (s.HasValue)
                    validSales.Add(s.Value);
            }


            // 2) compute mean (fallback to 0 if none)
            decimal mean = 0m;
            if (validSales.Count > 0)
            {
                decimal sum = 0m;
                foreach (var v in validSales) sum += v;
                mean = Math.Round(sum / validSales.Count, 2); // rounding to 2 decimals
            }

            // 3) Second pass: build cleaned list, apply defaults and normalize date
            var cleaned = new List<SaleRecord>(rawData.Count);

            for (int i = 0; i < rawData.Count; i++)
            {
                var r = rawData[i];
                var cr = new SaleRecord();

                // Region
                cr.Region = string.IsNullOrWhiteSpace(r?.Region)
                                ? DefaultRegion
                                : r.Region.Trim();

                // Sales: convert back to string
                cr.Sales = (parsedSales[i] ?? mean).ToString();

                // Date handling
                var normalized = NormalizeDate(r?.Date.ToString()!);

                if (normalized != null)
                    cr.Date = DateTime.ParseExact(normalized, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                else
                    cr.Date = DateTime.ParseExact(DefaultDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                cleaned.Add(cr);
            }
            return cleaned;
        }

        // Try to parse sales from various possible raw forms
        private static decimal? ParseSales(object raw)
        {
            if (raw == null) return null;

            // If it's already a numeric type
            if (raw is decimal d) return d;
            if (raw is double db) return Convert.ToDecimal(db);
            if (raw is float f) return Convert.ToDecimal(f);
            if (raw is long l) return Convert.ToDecimal(l);
            if (raw is int i) return Convert.ToDecimal(i);



            // If it's a string, try to parse
            var s = raw as string;
            if (s != null)
            {
                s = s.Trim();
                if (string.IsNullOrEmpty(s)) return null;
                // try decimal parse with InvariantCulture (supports dot)
                if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var val))
                    return val;
                // try with current culture as fallback (commas)
                if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                    return val;
                return null; // not parseable
            }

            return null;
        }

        // Try to parse different date formats; return ISO string or null if fail
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

            // Try exact formats first (invariant)
            foreach (var fmt in formats)
            {
                if (DateTime.TryParseExact(rawDate, fmt, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    return dt.ToString("yyyy-MM-dd");
            }

            // Fallback to general TryParse
            if (DateTime.TryParse(rawDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) ||
                DateTime.TryParse(rawDate, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
            {
                return dt.ToString("yyyy-MM-dd");
            }

            return null;
        }
    }
}