using functionalconcept.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FunctionalConcept
{
    public static class FunctionalMissingDataHandler
    {
        private static readonly string DefaultRegion = "Unknown";
        private static readonly string DefaultDate = "1900-01-01";

        // Public API: takes raw list and returns cleaned immutable list
        public static List<SaleRecord> HandleMissingData(List<SaleRecord> rawData)
        {
            if (rawData == null || rawData.Count == 0) return new List<SaleRecord>();

            // Pure function to parse sales -> decimal?
            decimal? ParseSales(object raw)
            {
                if (raw == null) return null;
                if (raw is decimal d) return d;
                if (raw is double db) return Convert.ToDecimal(db);
                if (raw is float f) return Convert.ToDecimal(f);
                if (raw is long l) return Convert.ToDecimal(l);
                if (raw is int i) return Convert.ToDecimal(i);

                if (raw is string s)
                {
                    s = s.Trim();
                    if (string.IsNullOrEmpty(s)) return null;
                    if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var val)) return val;
                    if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out val)) return val;
                }
                return null;
            }

            // Pure function to normalize date -> string or null
            string NormalizeDate(string rawDate)
            {
                if (string.IsNullOrWhiteSpace(rawDate)) return null;
                rawDate = rawDate.Trim();
                DateTime dt;
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

            // 1) compute mean from parsed sales (functional)
            var parsedSales = rawData.Select(r => ParseSales(r?.Sales));

            var validSales = parsedSales.Where(s => s.HasValue).Select(s => s.Value).ToList();

            decimal mean = 0m;
            if (validSales.Any())
            {
                // Average for decimal works via LINQ
                mean = Math.Round(validSales.Average(), 2);
            }

            // 2) map each raw record to an immutable CleanRecord
            var cleaned = rawData
                .Select(r =>
                {
                    // region defaulting
                    var region = string.IsNullOrWhiteSpace(r?.Region) ? DefaultRegion : r.Region.Trim();

                    // sales: use parsed if present else mean
                    var sVal = ParseSales(r?.Sales) ?? mean;

                    // date: normalize else default
                    var dateNorm = NormalizeDate(r?.Date.ToString()) ?? DefaultDate;

                    return new SaleRecord() { 
                        Region = region, 
                        Sales = sVal.ToString(CultureInfo.InvariantCulture), 
                        Date = DateTime.ParseExact(dateNorm, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    };
                })
                .ToList();

            return cleaned;
        }
    }
}
