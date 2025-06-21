using System.Xml.Serialization;

namespace Currex.Models.TCMBFinancialRate
{
    [XmlRoot("tcmbVeri")]
    public class TCMBFinancialRateHourlyDataModel
    {
        [XmlElement("baslik_bilgi")]
        public required TCMBFinancialRateHourlyHeaderInfoModel HeaderInfo { get; set; }

        [XmlElement("doviz_kur_liste")]
        public required TCMBFinancialRateHourlyExchangeRateListModel ExchangeRateList { get; set; }

        [XmlElement("aciklama")]
        public required string Description { get; set; }
    }

    public class TCMBFinancialRateHourlyHeaderInfoModel
    {
        [XmlElement("kod")]
        public required string Code { get; set; }

        [XmlElement("veri_tipi")]
        public required string DataType { get; set; }

        [XmlElement("veri_tanim")]
        public required string DataDescription { get; set; }

        [XmlElement("yayimlayan")]
        public required string Publisher { get; set; }

        [XmlElement("tel")]
        public required string Phone { get; set; }

        [XmlElement("faks")]
        public required string Fax { get; set; }

        [XmlElement("eposta")]
        public required string Email { get; set; }

        [XmlElement("zaman_etiketi")]
        public DateTime Timestamp { get; set; }
    }

    public class TCMBFinancialRateHourlyExchangeRateListModel
    {
        [XmlAttribute("gecerlilik_tarihi")]
        public required string ValidityDate { get; set; }

        [XmlAttribute("saat")]
        public required string Hour { get; set; }

        [XmlElement("kur")]
        public required List<TCMBFinancialRateHourlyExchangeRateModel> Rates { get; set; }
    }

    public class TCMBFinancialRateHourlyExchangeRateModel
    {
        [XmlElement("doviz_cinsi_tabani")]
        public required string BaseCurrency { get; set; }

        [XmlElement("doviz_cinsi")]
        public required string Currency { get; set; }

        [XmlElement("birim")]
        public int Unit { get; set; }

        [XmlElement("alis")]
        public required string BuyPrice { get; set; }

        [XmlElement("sira_no")]
        public int OrderNumber { get; set; }
    }
}
