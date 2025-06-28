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
        public MarginValue? USD { get; set; }

        public MarginValue? EUR { get; set; }

        public MarginValue? SAR { get; set; }

        public MarginValue? KWD { get; set; }

        public MarginValue? AED { get; set; }

        public MarginValue? GBP { get; set; }

        public MarginValue? XAU24 { get; set; }

        public MarginValue? XAU22 { get; set; }

        public MarginValue? XAUQUARTER { get; set; }

        public MarginValue? XAUHALF { get; set; }

        public MarginValue? XAUFULL { get; set; }

        public MarginValue? XAURESAT { get; set; }

        public MarginValue? XAUTRABZONHASIR { get; set; }
    }

    public class MarginValue
    {
        public bool IsPositive { get; set; }
        public double Value { get; set; }
    }
}
