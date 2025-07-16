using Currex.Models.Admin;
using Currex.Services.Abstract;
using System.Text.Json;

namespace Currex.Services.Concrete
{
    public class FinancialAssetMarginService(IHostEnvironment env) : IFinancialAssetMarginService
    {
        private readonly string _filePath = Path.Combine(env.ContentRootPath, "financialAssetMargins.json");
        private readonly object _lock = new();

        public List<FinancialAssetMarginModel> GetAll()
        {
            if (!File.Exists(_filePath))
                return [];

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<FinancialAssetMarginModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? [];
        }

        public void UpdateAll(List<FinancialAssetMarginModel> models)
        {
            var json = JsonSerializer.Serialize(models, new JsonSerializerOptions { WriteIndented = true });
            lock (_lock)
            {
                File.WriteAllText(_filePath, json);
            }
        }
    }
}
