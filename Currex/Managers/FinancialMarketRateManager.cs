using Currex.Enums.FinancialMarketRate;
using Currex.Models;
using Currex.Models.FinancialMarketRate;
using Currex.Services.Abstract;
using Currex.Services.Concrete;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Currex.Managers
{
    public class FinancialMarketRateManager(HttpClient httpClient, IOptions<ApplicationSettingsModel> options)
    {
        private readonly IFinancialMarketRateService _financialMarketRateService = new TCMBFinancialRateService(httpClient, options);

        public async Task<FinancialMarketRateModel> GetCurrentAsync()
        {
            FinancialMarketRateModel financialMarketRate = await _financialMarketRateService.GetCurrent();

            ApplyMargins(financialMarketRate);

            financialMarketRate.Assets.RemoveWhere(x => x.AssetType.Code() == FinancialAssetType.XAU.ToString() || x.Buying == 0);

            return financialMarketRate;
        }

        private void ApplyMargins(FinancialMarketRateModel financialMarketRate)
        {
            var marginProperties = typeof(MarginGroup).GetProperties();

            foreach (var asset in financialMarketRate.Assets)
            {
                PropertyInfo? marginProperty = marginProperties.FirstOrDefault(p => string.Equals(p.Name, asset.AssetType.Code(), StringComparison.OrdinalIgnoreCase));
                if (marginProperty == null) continue;

                var buyingMargin = (MarginValue?)marginProperty.GetValue(options.Value.FinancialAssetMargins.Buying);
                var payingMargin = (MarginValue?)marginProperty.GetValue(options.Value.FinancialAssetMargins.Paying);
                var sellingMargin = (MarginValue?)marginProperty.GetValue(options.Value.FinancialAssetMargins.Selling);

                asset.Buying *= buyingMargin is not null
                    ? (decimal)(1 + (buyingMargin.IsPositive ? 1 : -1) * buyingMargin.Value / 100.0)
                    : 1;
                asset.Paying *= payingMargin is not null
                    ? (decimal)(1 + (payingMargin.IsPositive ? 1 : -1) * payingMargin.Value / 100.0)
                    : 0;
                asset.Selling *= sellingMargin is not null
                    ? (decimal)(1 + (sellingMargin.IsPositive ? 1 : -1) * sellingMargin.Value / 100.0)
                    : 0;

                // If asset is gold, round values to nearest multiple of 5 with no decimals
                if (asset.AssetType.IsGold())
                {
                    asset.Buying = RoundUpToNearestFive(asset.Buying);
                    asset.Paying = RoundUpToNearestFive(asset.Paying);
                    asset.Selling = RoundUpToNearestFive(asset.Selling);
                }
            }
        }

        private static decimal RoundUpToNearestFive(decimal value)
        {
            return Math.Ceiling(value / 5) * 5;
        }
    }
}
