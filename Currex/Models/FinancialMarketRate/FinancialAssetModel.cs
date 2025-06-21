using Currex.Enums.FinancialMarketRate;

namespace Currex.Models.FinancialMarketRate
{
    public class FinancialAssetModel(FinancialAssetType assetType, decimal buying, decimal paying, decimal selling)
    {
        public FinancialAssetType AssetType { get; set; } = assetType;

        public decimal Buying { get; set; } = buying;

        public decimal Paying { get; set; } = paying;

        public decimal Selling { get; set; } = selling;

        public override bool Equals(object? obj)
        {
            if (obj is FinancialAssetModel other)
            {
                return string.Equals(AssetType.Code(), other.AssetType.Code(), StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return AssetType.Code()?.ToLowerInvariant().GetHashCode() ?? 0;
        }
    }
}
