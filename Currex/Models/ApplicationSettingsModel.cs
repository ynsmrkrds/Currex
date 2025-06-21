namespace Currex.Models
{
    public class ApplicationSettingsModel
    {
        public required ServiceUrls ServiceUrls { get; set; }

        public required FinancialAssetMargins FinancialAssetMargins { get; set; }
    }

    public class ServiceUrls
    {
        public required string TCMBFinancialRateHourly { get; set; }

        public required string TCMBFinancialRateDaily { get; set; }
    }

    public class FinancialAssetMargins
    {
        public required MarginGroup Buying { get; set; }

        public required MarginGroup Paying { get; set; }

        public required MarginGroup Selling { get; set; }
    }

    public class MarginGroup
    {
        public double? USD { get; set; }

        public double? EUR { get; set; }

        public double? SAR { get; set; }

        public double? KWD { get; set; }

        public double? AED { get; set; }

        public double? GBP { get; set; }

        public double? XAU24 { get; set; }

        public double? XAU22 { get; set; }

        public double? XAUQUARTER { get; set; }

        public double? XAUHALF { get; set; }

        public double? XAUFULL { get; set; }

        public double? XAURESAT { get; set; }

        public double? XAUTRABZONHASIR { get; set; }
    }
}
