using Currex.Helpers;
using System.Xml.Serialization;

namespace Currex.Models.TCMBFinancialRate
{
    [XmlRoot("Tarih_Date")]
    public class TCMBFinancialRateDailyDataModel
    {
        [XmlAttribute("Tarih")]
        public required string TurkishDate { get; set; }

        [XmlAttribute("Date")]
        public required string Date { get; set; }

        [XmlAttribute("Bulten_No")]
        public required string BulletinNo { get; set; }

        [XmlElement("Currency")]
        public required List<TCMBFinancialRateDailyCurrencyModel> Currencies { get; set; }
    }

    public class TCMBFinancialRateDailyCurrencyModel
    {
        [XmlAttribute("CrossOrder")]
        public int CrossOrder { get; set; }

        [XmlAttribute("Kod")]
        public required string Code { get; set; }

        [XmlAttribute("CurrencyCode")]
        public required string CurrencyCode { get; set; }

        [XmlElement("Unit")]
        public int Unit { get; set; }

        [XmlElement("Isim")]
        public required string LocalName { get; set; }

        [XmlElement("CurrencyName")]
        public required string CurrencyName { get; set; }

        [XmlElement("ForexBuying")]
        public string? ForexBuyingRaw { get; set; }

        [XmlIgnore]
        public decimal? ForexBuying => DecimalHelper.TryParseFlexible(ForexBuyingRaw, out var v) ? v : null;

        [XmlElement("ForexSelling")]
        public string? ForexSellingRaw { get; set; }

        [XmlIgnore]
        public decimal? ForexSelling => DecimalHelper.TryParseFlexible(ForexSellingRaw, out var v) ? v : null;

        [XmlElement("BanknoteBuying")]
        public string? BanknoteBuyingRaw { get; set; }

        [XmlIgnore]
        public decimal? BanknoteBuying => DecimalHelper.TryParseFlexible(BanknoteBuyingRaw, out var v) ? v : null;

        [XmlElement("BanknoteSelling")]
        public string? BanknoteSellingRaw { get; set; }

        [XmlIgnore]
        public decimal? BanknoteSelling => DecimalHelper.TryParseFlexible(BanknoteSellingRaw, out var v) ? v : null;

        [XmlElement("CrossRateUSD")]
        public string? CrossRateUSDRaw { get; set; }

        [XmlIgnore]
        public decimal? CrossRateUSD => DecimalHelper.TryParseFlexible(CrossRateUSDRaw, out var v) ? v : null;

        [XmlElement("CrossRateOther")]
        public string? CrossRateOtherRaw { get; set; }

        [XmlIgnore]
        public decimal? CrossRateOther => DecimalHelper.TryParseFlexible(CrossRateOtherRaw, out var v) ? v : null;
    }
}
