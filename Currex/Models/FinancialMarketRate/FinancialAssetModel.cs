namespace Currex.Models.FinancialMarketRate
{
    public class FinancialAssetModel(string name, string code, decimal buying, decimal paying, decimal selling)
    {
        public string Name { get; set; } = name;

        public string Code { get; set; } = code;

        public decimal Buying { get; set; } = buying;

        public decimal Paying { get; set; } = paying;

        public decimal Selling { get; set; } = selling;

        public override bool Equals(object? obj)
        {
            if (obj is FinancialAssetModel other)
            {
                return string.Equals(Code, other.Code, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Code?.ToLowerInvariant().GetHashCode() ?? 0;
        }
    }
}
