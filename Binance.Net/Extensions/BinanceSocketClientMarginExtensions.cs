using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Margin.Socket;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Extensions
{
    /// <summary>
    /// Extension methods for Binance margin socket operations
    /// </summary>
    public static class BinanceSocketClientMarginExtensions
    {
        /// <summary>
        /// Subscribes to margin user data updates with automatic listen key management
        /// </summary>
        public static async Task<CallResult<UpdateSubscription>> SubscribeToMarginUserDataUpdatesAsync(
            this IBinanceSocketClient socketClient,
            Action<DataEvent<BinanceMarginStreamOrderUpdate>>? onOrderUpdate = null,
            Action<DataEvent<BinanceMarginStreamAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<BinanceMarginStreamBalanceUpdate>>? onBalanceUpdate = null,
            Action<DataEvent<BinanceMarginStreamEvent>>? onListenKeyExpired = null,
            CancellationToken ct = default)
        {
            // Get listen key first
            var listenKeyResult = await socketClient.SpotApi.Account.StartUserStreamAsync(ct).ConfigureAwait(false);
            if (!listenKeyResult.Success)
                return new CallResult<UpdateSubscription>(listenKeyResult.Error!);

            var listenKey = listenKeyResult.Data;

            // Subscribe to user data
            return await socketClient.SpotApi.Account.SubscribeToMarginUserDataUpdatesAsync(
                listenKey, onOrderUpdate, onAccountUpdate, onBalanceUpdate, onListenKeyExpired, null, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes only to margin order updates
        /// </summary>
        public static Task<CallResult<UpdateSubscription>> SubscribeToMarginOrderUpdatesAsync(
            this IBinanceSocketClient socketClient,
            string listenKey,
            Action<DataEvent<BinanceMarginStreamOrderUpdate>> onOrderUpdate,
            CancellationToken ct = default)
        {
            return socketClient.SpotApi.Account.SubscribeToMarginUserDataUpdatesAsync(
                listenKey, onOrderUpdate, null, null, null, null, ct);
        }

        /// <summary>
        /// Subscribes only to margin account updates
        /// </summary>
        public static Task<CallResult<UpdateSubscription>> SubscribeToMarginAccountUpdatesAsync(
            this IBinanceSocketClient socketClient,
            string listenKey,
            Action<DataEvent<BinanceMarginStreamAccountUpdate>> onAccountUpdate,
            CancellationToken ct = default)
        {
            return socketClient.SpotApi.Account.SubscribeToMarginUserDataUpdatesAsync(
                listenKey, null, onAccountUpdate, null, null, null, ct);
        }

        /// <summary>
        /// Subscribes only to margin balance updates
        /// </summary>
        public static Task<CallResult<UpdateSubscription>> SubscribeToMarginBalanceUpdatesAsync(
            this IBinanceSocketClient socketClient,
            string listenKey,
            Action<DataEvent<BinanceMarginStreamBalanceUpdate>> onBalanceUpdate,
            CancellationToken ct = default)
        {
            return socketClient.SpotApi.Account.SubscribeToMarginUserDataUpdatesAsync(
                listenKey, null, null, onBalanceUpdate, null, null, ct);
        }
    }
}
