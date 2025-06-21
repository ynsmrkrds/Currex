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

                var buyingMargin = marginProperty.GetValue(options.Value.FinancialAssetMargins.Buying) as double?;
                var payingMargin = marginProperty.GetValue(options.Value.FinancialAssetMargins.Paying) as double?;
                var sellingMargin = marginProperty.GetValue(options.Value.FinancialAssetMargins.Selling) as double?;

                asset.Buying *= buyingMargin.HasValue ? (decimal)(1 + buyingMargin.Value / 100.0) : 1;
                asset.Paying *= payingMargin.HasValue ? (decimal)(1 + payingMargin.Value / 100.0) : 0;
                asset.Selling *= sellingMargin.HasValue ? (decimal)(1 + sellingMargin.Value / 100.0) : 0;
            }
        }
    }
}
