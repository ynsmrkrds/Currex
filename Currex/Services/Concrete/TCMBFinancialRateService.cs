using Currex.Enums.FinancialMarketRate;
using Currex.Enums.TCMBFinancialRate;
using Currex.Helpers;
using Currex.Models;
using Currex.Models.FinancialMarketRate;
using Currex.Models.TCMBFinancialRate;
using Currex.Services.Abstract;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Xml.Serialization;

namespace Currex.Services.Concrete
{
    public class TCMBFinancialRateService(HttpClient httpClient, IOptions<ApplicationSettingsModel> options) : IFinancialMarketRateService
    {
        public async Task<FinancialMarketRateModel> GetCurrent()
        {
            TCMBFinancialRateHourlyDataModel hourlyDataModel = await GetHourly();
            TCMBFinancialRateDailyDataModel dailyDataModel = await GetDaily();

            return new FinancialMarketRateModel(
                requestedDate: DateTime.Now,
                assets: [
                    .. hourlyDataModel.ExchangeRateList.Rates
                        .Where(r => Enum.GetValues<FinancialAssetType>().ToList().Any(t => t.ToString() == r.Currency))
                        .Select(r => new FinancialAssetModel(
                                assetType: Enum.Parse<FinancialAssetType>(r.Currency),
                                buying: DecimalHelper.TryParseFlexible(r.BuyPrice, out decimal b) ? b : 0,
                                paying: DecimalHelper.TryParseFlexible(r.BuyPrice, out decimal p) ? p : 0,
                                selling: DecimalHelper.TryParseFlexible(r.BuyPrice, out decimal s) ? s : 0
                            )
                    ),
                    .. dailyDataModel.Currencies
                        .Where(c => Enum.GetValues<FinancialAssetType>().ToList().Any(t => t.ToString() == c.Code))
                        .Select(c => new FinancialAssetModel(
                            assetType: Enum.Parse<FinancialAssetType>(c.Code),
                            buying: (c.ForexBuying ?? c.BanknoteBuying) ?? 0,
                            paying: (c.ForexBuying ?? c.BanknoteBuying) ?? 0,
                            selling: (c.ForexSelling ?? c.BanknoteSelling) ?? 0
                        )
                    ),
                    .. Enum.GetValues<FinancialAssetType>().ToList()
                        .Select(t =>
                        {
                            var XAU = hourlyDataModel.ExchangeRateList.Rates.FirstOrDefault(r => r.Currency == FinancialAssetType.XAU.ToString());

                            return new FinancialAssetModel(
                                assetType: t,
                                buying: DecimalHelper.TryParseFlexible(XAU?.BuyPrice, out decimal b) ? b : 0,
                                paying: DecimalHelper.TryParseFlexible(XAU?.BuyPrice, out decimal p) ? p : 0,
                                selling: DecimalHelper.TryParseFlexible(XAU?.BuyPrice, out decimal s) ? s : 0
                            );
                        }
                    )
                ]
            );
        }

        private async Task<TCMBFinancialRateHourlyDataModel> GetHourly()
        {
            var (date, startHour) = GetInitialDateAndHourIndex();

            var validHours = Enum.GetValues<TCMBFinancialRateHourlyValidHours>()
                                 .OrderByDescending(h => (int)h)
                                 .ToList();

            int startIndex = validHours.FindIndex(h => ((int)h).ToString() == startHour);
            if (startIndex == -1) startIndex = 0;

            while (true)
            {
                for (int i = startIndex; i < validHours.Count; i++)
                {
                    string url = BuildUrl(date, ((int)validHours[i]).ToString());

                    var result = await GetHourly(url);
                    if (result != null) return result;
                }

                date = date.AddDays(-1);
                startIndex = 0;
            }
        }

        private (DateTime date, string hour) GetInitialDateAndHourIndex()
        {
            var now = DateTime.Now;

            return now.TimeOfDay switch
            {
                var t when t < TimeSpan.FromHours(10) => (now.Date.AddDays(-1), ((int)TCMBFinancialRateHourlyValidHours.t1500).ToString()),
                var t when t < TimeSpan.FromHours(13) => (now.Date, ((int)TCMBFinancialRateHourlyValidHours.t1000).ToString()),
                var t when t < TimeSpan.FromHours(15) => (now.Date, ((int)TCMBFinancialRateHourlyValidHours.t1300).ToString()),
                _ => (now.Date, ((int)TCMBFinancialRateHourlyValidHours.t1500).ToString())
            };
        }

        private string BuildUrl(DateTime date, string hour)
        {
            return options.Value.ServiceUrls.TCMBFinancialRateHourly
                .Replace("{dateYYYYMM}", $"{date:yyyyMM}")
                .Replace("{dayGGAAYYYY}", $"{date:ddMMyyyy}")
                .Replace("{hourHHMM}", hour);
        }

        private async Task<TCMBFinancialRateHourlyDataModel?> GetHourly(string url)
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var xmlString = await response.Content.ReadAsStringAsync();

                var serializer = new XmlSerializer(typeof(TCMBFinancialRateHourlyDataModel));

                using var reader = new StringReader(xmlString);
                return (TCMBFinancialRateHourlyDataModel)serializer.Deserialize(reader)!;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<TCMBFinancialRateDailyDataModel> GetDaily()
        {
            var response = await httpClient.GetAsync(options.Value.ServiceUrls.TCMBFinancialRateDaily);
            response.EnsureSuccessStatusCode();

            var xmlString = await response.Content.ReadAsStringAsync();

            var serializer = new XmlSerializer(typeof(TCMBFinancialRateDailyDataModel));

            using var reader = new StringReader(xmlString);
            return (TCMBFinancialRateDailyDataModel)serializer.Deserialize(reader)!;
        }
    }
}
