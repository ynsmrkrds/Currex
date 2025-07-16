using Currex.Models.Admin;

namespace Currex.Services.Abstract
{
    public interface IFinancialAssetMarginService
    {
        List<FinancialAssetMarginModel> GetAll();

        void UpdateAll(List<FinancialAssetMarginModel> margins);
    }
}
