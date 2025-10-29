using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Models.Margin.Socket
{
    /// <summary>
    /// Base margin user stream event
    /// </summary>
    public class BinanceMarginStreamEvent
    {
        /// <summary>
        /// The type of the event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
        
        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonPropertyName("E"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime EventTime { get; set; }
    }
}
