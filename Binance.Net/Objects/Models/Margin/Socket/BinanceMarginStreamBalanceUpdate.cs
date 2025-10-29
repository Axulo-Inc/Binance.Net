using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Models.Margin.Socket
{
    /// <summary>
    /// Margin balance update
    /// </summary>
    public class BinanceMarginStreamBalanceUpdate : BinanceMarginStreamEvent
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        
        /// <summary>
        /// Balance delta
        /// </summary>
        [JsonPropertyName("d")]
        public decimal Delta { get; set; }
        
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }
}
