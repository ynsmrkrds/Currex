using System.Globalization;

namespace Currex.Helpers
{
    public static class DecimalHelper
    {
        public static bool TryParseFlexible(string? input, out decimal result)
        {
            if (input == null)
            {
                result = 0;
                return false;
            }                

            // 2. Replace dot with comma and try using Turkish culture
            var replacedComma = input.Replace(".", ",");
            if (decimal.TryParse(replacedComma, NumberStyles.Any, new CultureInfo("tr-TR"), out result))
                return true;

            // 3. Replace comma with dot and try using invariant culture
            var replacedDot = input.Replace(",", ".");
            if (decimal.TryParse(replacedDot, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return true;

            // 4. Parsing failed for all formats
            result = 0;
            return false;
        }
    }
}
