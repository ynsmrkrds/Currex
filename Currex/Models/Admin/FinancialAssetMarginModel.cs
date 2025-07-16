using Currex.Enums.FinancialMarketRate;

namespace Currex.Models.Admin
{
    public class FinancialAssetMarginModel
    {
        public FinancialAssetType AssetType { get; set; }

        public FinancialAssetMarginDetailModel Values { get; set; } = new();
    }

    public class FinancialAssetMarginDetailModel
    {
        public FinancialAssetMarginValueModel Buying { get; set; } = new();

        public FinancialAssetMarginValueModel Paying { get; set; } = new();

        public FinancialAssetMarginValueModel Selling { get; set; } = new();
    }

    public class FinancialAssetMarginValueModel
    {
        public bool IsPositive { get; set; }

        public double Value { get; set; }
    }
}