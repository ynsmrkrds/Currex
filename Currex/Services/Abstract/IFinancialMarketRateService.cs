using Currex.Models.FinancialMarketRate;

namespace Currex.Services.Abstract
{
    public interface IFinancialMarketRateService
    {
        Task<FinancialMarketRateModel> GetCurrent();
    }
}
