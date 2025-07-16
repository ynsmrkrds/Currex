namespace Currex.Enums.FinancialMarketRate
{
    public enum FinancialAssetType
    {
        USD = 1, // ABD DOLARI
        EUR = 2, // EURO
        SAR = 3, // SUUDİ ARABİSTAN RİYALİ
        KWD = 4, // KUVEYT DİNARI
        AED = 5, // BİRLEŞİK ARAP EMİRLİKLERİ DİRHEMİ
        GBP = 6, // İNGİLİZ STERLİNİ     
        XAU = 7, // ALTIN GRAM 1000/1000
        XAU24 = 8, // ALTIN GRAM 24 AYAR
        XAU22 = 9, // ALTIN GRAM 22 AYAR
        XAUQUARTER = 10, // ALTIN ÇEYREK
        XAUHALF = 11, // ALTIN YARIM
        XAUFULL = 12, // ALTIN TAM
        XAURESAT = 13, // ALTIN REŞAT
        XAUTRABZONHASIR = 14, // ALTIN TRABZON HASIR
    }

    public static class FinancialAssetTypeExtensions
    {
        public static string Code(this FinancialAssetType assetType)
        {
            return assetType.ToString();
        }

        public static string ToDisplayName(this FinancialAssetType assetType)
        {
            return assetType switch
            {
                FinancialAssetType.USD => "ABD DOLARI",
                FinancialAssetType.EUR => "EURO",
                FinancialAssetType.SAR => "SUUDİ ARABİSTAN RİYALİ",
                FinancialAssetType.KWD => "KUVEYT DİNARI",
                FinancialAssetType.AED => "BİRLEŞİK ARAP EMİRLİKLERİ DİRHEMİ",
                FinancialAssetType.GBP => "İNGİLİZ STERLİNİ",
                FinancialAssetType.XAU => "GRAM ALTIN",
                FinancialAssetType.XAU24 => "24 AYAR",
                FinancialAssetType.XAU22 => "22 AYAR BİLEZİK",
                FinancialAssetType.XAUQUARTER => "ÇEYREK ALTIN",
                FinancialAssetType.XAUHALF => "YARIM ALTIN",
                FinancialAssetType.XAUFULL => "TAM ALTIN",
                FinancialAssetType.XAURESAT => "REŞAT ALTIN",
                FinancialAssetType.XAUTRABZONHASIR => "TRABZON HASIR",
                _ => assetType.ToString()
            };
        }

        public static bool IsGold(this FinancialAssetType assetType)
        {
            return assetType == FinancialAssetType.XAU ||
                   assetType == FinancialAssetType.XAU24 ||
                   assetType == FinancialAssetType.XAU22 ||
                   assetType == FinancialAssetType.XAUQUARTER ||
                   assetType == FinancialAssetType.XAUHALF ||
                   assetType == FinancialAssetType.XAUFULL ||
                   assetType == FinancialAssetType.XAURESAT ||
                   assetType == FinancialAssetType.XAUTRABZONHASIR;
        }
    }
}
