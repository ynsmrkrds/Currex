namespace Currex.Models.FinancialMarketRate
{
    public class FinancialMarketRateModel(DateTime requestedDate, HashSet<FinancialAssetModel> assets)
    {
        public DateTime RequestedDate { get; set; } = requestedDate;

        public HashSet<FinancialAssetModel> Assets { get; set; } = assets;
    }
}
