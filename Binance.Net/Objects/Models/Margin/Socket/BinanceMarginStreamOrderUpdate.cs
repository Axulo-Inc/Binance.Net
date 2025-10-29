using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Models.Margin.Socket
{
    /// <summary>
    /// Margin order update
    /// </summary>
    public class BinanceMarginStreamOrderUpdate : BinanceMarginStreamEvent
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        
        /// <summary>
        /// The new client order id
        /// </summary>
        [JsonPropertyName("c")]
        public string ClientOrderId { get; set; } = string.Empty;
        
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonPropertyName("S")]
        public string Side { get; set; } = string.Empty;
        
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("o")]
        public string OrderType { get; set; } = string.Empty;
        
        /// <summary>
        /// The time in force
        /// </summary>
        [JsonPropertyName("f")]
        public string TimeInForce { get; set; } = string.Empty;
        
        /// <summary>
        /// The quantity of the order
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        
        /// <summary>
        /// The stop price
        /// </summary>
        [JsonPropertyName("P")]
        public decimal StopPrice { get; set; }
        
        /// <summary>
        /// The iceberg quantity
        /// </summary>
        [JsonPropertyName("F")]
        public decimal IcebergQuantity { get; set; }
        
        /// <summary>
        /// The original client order id
        /// </summary>
        [JsonPropertyName("C")]
        public string OriginalClientOrderId { get; set; } = string.Empty;
        
        /// <summary>
        /// The current execution type
        /// </summary>
        [JsonPropertyName("x")]
        public string ExecutionType { get; set; } = string.Empty;
        
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonPropertyName("X")]
        public string OrderStatus { get; set; } = string.Empty;
        
        /// <summary>
        /// The reason for the rejection
        /// </summary>
        [JsonPropertyName("r")]
        public string RejectReason { get; set; } = string.Empty;
        
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        
        /// <summary>
        /// The quantity of the last filled trade
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LastFilledQuantity { get; set; }
        
        /// <summary>
        /// The quantity of all filled trades
        /// </summary>
        [JsonPropertyName("z")]
        public decimal CumulativeFilledQuantity { get; set; }
        
        /// <summary>
        /// The price of the last filled trade
        /// </summary>
        [JsonPropertyName("L")]
        public decimal LastFilledPrice { get; set; }
        
        /// <summary>
        /// The commission of the last filled trade
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Commission { get; set; }
        
        /// <summary>
        /// The asset of the commission
        /// </summary>
        [JsonPropertyName("N")]
        public string CommissionAsset { get; set; } = string.Empty;
        
        /// <summary>
        /// The time of the transaction
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
        
        /// <summary>
        /// The trade id
        /// </summary>
        [JsonPropertyName("t")]
        public long TradeId { get; set; }
        
        /// <summary>
        /// Is this trade the maker side
        /// </summary>
        [JsonPropertyName("m")]
        public bool IsMaker { get; set; }
        
        /// <summary>
        /// Is this order on the order book
        /// </summary>
        [JsonPropertyName("w")]
        public bool IsOrderBook { get; set; }
        
        /// <summary>
        /// Is this trade for margin
        /// </summary>
        [JsonPropertyName("M")]
        public bool IsMargin { get; set; } = true;
    }
}
