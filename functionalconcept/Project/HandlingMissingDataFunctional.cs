using functionalconcept;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FunctionalConcept
{
    public static class HandlingMissingDataFunctional
    {
        private static readonly string DefaultDate = "1900-01-01";
        private static readonly DateTime DefaultDateTime =
            DateTime.ParseExact(DefaultDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        public static List<SaleRecord> HandleMissingData(List<SaleRecord> rawData)
        {
            if (rawData == null) return new List<SaleRecord>();

            var defaultRegion = GetRegionMode(rawData);

            var parsedSales = rawData
                .Select(r => ParseSales(r?.Sales))
                .ToList();

            var presentSales = parsedSales.Where(x => x.HasValue).Select(x => x!.Value).ToList();
            var mean = presentSales.Any()
                ? Math.Round(presentSales.Average(), 2)
                : 0m;

            var cleaned = rawData
                .Select((r, idx) =>
                {
                    var parsed = parsedSales[idx];
                    var salesText = (parsed ?? mean).ToString(CultureInfo.InvariantCulture);

                    var normalizedDate = NormalizeDate(r?.Date?.ToString());
                    var date = normalizedDate ?? DefaultDateTime;

                    return new SaleRecord
                    {
                        Region = string.IsNullOrWhiteSpace(r?.Region) ? defaultRegion : r.Region.Trim(),
                        Sales = salesText,
                        Date = date
                    };
                })
                .ToList();

            return cleaned;
        }

        private static decimal? ParseSales(object raw)
        {
            if (raw == null) return null;

            switch (raw)
            {
                case decimal d:
                    return d;
                case double db:
                    return Convert.ToDecimal(db);
                case float f:
                    return Convert.ToDecimal(f);
                case long l:
                    return Convert.ToDecimal(l);
                case int i:
                    return Convert.ToDecimal(i);
                case string s:
                    {
                        s = s.Trim();
                        if (string.IsNullOrEmpty(s)) return null;

                        if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var val))
                            return val;
                        if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out val))
                            return val;
                        return null;
                    }
                default:
                    return null;
            }
        }
        /////////////////////////////////////////////
        /// 

        private static DateTime? NormalizeDate(string rawDate)
        {
            if (string.IsNullOrWhiteSpace(rawDate)) return null;
            rawDate = rawDate.Trim();

            var formats = new[]
            {
                "yyyy-MM-dd", "yyyy/MM/dd", "yyyy-M-d", "yyyy/M/d",
                "MM/dd/yy", "M/d/yy", "MM/dd/yyyy", "M/d/yyyy",
                "dd/MM/yy", "dd/MM/yyyy", "d/M/yy", "d/M/yyyy",
                "yyyyMMdd"
            };

            if (DateTime.TryParseExact(rawDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt.Date;

            if (DateTime.TryParse(rawDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) ||
                DateTime.TryParse(rawDate, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
            {
                return dt.Date;
            }

            return null;
        }
        ////////////////////////////////////////////////////////////
        /// 

        private static string GetRegionMode(IEnumerable<SaleRecord> rawData)
        {
            var cleanedRegions = rawData
                .Where(r => !string.IsNullOrWhiteSpace(r?.Region))
                .Select(r => r.Region.Trim());

            var grouped = cleanedRegions
                .GroupBy(name => name, StringComparer.OrdinalIgnoreCase);

            var topGroup = grouped
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .FirstOrDefault();

            return topGroup?.Key ?? "Unknown";
        }

    }
}