using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Margin.Socket;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BinanceMarginUserDataSubscription : Subscription<BinanceSocketQueryResponse, BinanceSocketQueryResponse>
    {
        private readonly string _listenKey;

        private readonly Action<DataEvent<BinanceMarginStreamOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<BinanceMarginStreamAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<BinanceMarginStreamBalanceUpdate>>? _balanceHandler;
        private readonly Action<DataEvent<BinanceMarginStreamEvent>>? _listenKeyExpiredHandler;
        private readonly Action<DataEvent<BinanceMarginStreamEvent>>? _streamTerminatedHandler;

        /// <inheritdoc />
        public BinanceMarginUserDataSubscription(
            ILogger logger,
            string listenKey,
            Action<DataEvent<BinanceMarginStreamOrderUpdate>>? orderHandler,
            Action<DataEvent<BinanceMarginStreamAccountUpdate>>? accountHandler,
            Action<DataEvent<BinanceMarginStreamBalanceUpdate>>? balanceHandler,
            Action<DataEvent<BinanceMarginStreamEvent>>? listenKeyExpiredHandler,
            Action<DataEvent<BinanceMarginStreamEvent>>? streamTerminatedHandler) : base(logger, true)
        {
            _listenKey = listenKey;
            _orderHandler = orderHandler;
            _accountHandler = accountHandler;
            _balanceHandler = balanceHandler;
            _listenKeyExpiredHandler = listenKeyExpiredHandler;
            _streamTerminatedHandler = streamTerminatedHandler;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<BinanceCombinedStream<BinanceMarginStreamAccountUpdate>>(_listenKey + "outboundAccountPosition", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceMarginStreamBalanceUpdate>>(_listenKey + "balanceUpdate", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceMarginStreamOrderUpdate>>(_listenKey + "executionReport", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceMarginStreamEvent>>(_listenKey + "listenKeyExpired", DoHandleMessage),
                new MessageHandlerLink<BinanceCombinedStream<BinanceMarginStreamEvent>>(_listenKey + "eventStreamTerminated", DoHandleMessage)
            ]);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = new[] { _listenKey },
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new BinanceSystemQuery<BinanceSocketQueryResponse>(new BinanceSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = new[] { _listenKey },
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceMarginStreamAccountUpdate>> message)
        {
            _accountHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceMarginStreamBalanceUpdate>> message)
        {
            _balanceHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceMarginStreamOrderUpdate>> message)
        {
            _orderHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BinanceCombinedStream<BinanceMarginStreamEvent>> message)
        {
            if (message.Data.Stream.EndsWith("listenKeyExpired"))
                _listenKeyExpiredHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            else if (message.Data.Stream.EndsWith("eventStreamTerminated"))
                _streamTerminatedHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            
            return CallResult.SuccessResult;
        }
    }
}
