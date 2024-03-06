using System;

namespace BE
{
    public class TradingPair
    {
        public int PairId { get; set; }
        public int BaseCurrencyId { get; set; }
        public string BaseCurrencyName { get; set; } 
        public string BaseCurrencyCountry { get; set; }
        public string BaseCurrencyAbbreviation { get; set; }
        public int TargetCurrencyId { get; set; }
        public string TargetCurrencyName { get; set; }
        public string TargetCurrencyCountry { get; set; }
        public string TargetCurrencyAbbreviation { get; set; }
        public decimal Value { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
    }

}
