using System.Collections.Generic;
namespace Binance.Net.Objects.Models.Margin.Socket
{
    /// <summary>
    /// Margin account update
    /// </summary>
    public class BinanceMarginStreamAccountUpdate : BinanceMarginStreamEvent
    {
        /// <summary>
        /// Reason type
        /// </summary>
        [JsonPropertyName("m")]
        public string ReasonType { get; set; } = string.Empty;
        
        /// <summary>
        /// Balances
        /// </summary>
        [JsonPropertyName("B")]
        public IEnumerable<BinanceMarginStreamBalance> Balances { get; set; } = Array.Empty<BinanceMarginStreamBalance>();
    }

    /// <summary>
    /// Margin stream balance
    /// </summary>
    public class BinanceMarginStreamBalance
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        
        /// <summary>
        /// Free amount
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Free { get; set; }
        
        /// <summary>
        /// Locked amount
        /// </summary>
        [JsonPropertyName("l")]
        public decimal Locked { get; set; }
        
        /// <summary>
        /// Borrowed amount
        /// </summary>
        [JsonPropertyName("b")]
        public decimal Borrowed { get; set; }
        
        /// <summary>
        /// Interest amount
        /// </summary>
        [JsonPropertyName("i")]
        public decimal Interest { get; set; }
        
        /// <summary>
        /// Net asset
        /// </summary>
        [JsonPropertyName("n")]
        public decimal NetAsset { get; set; }
    }
}
