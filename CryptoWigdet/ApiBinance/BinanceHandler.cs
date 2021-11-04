﻿using RestSharp;
using System;
using System.Collections.Generic;
using Binance.Client.Websocket;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Threading;
using Binance.Client.Websocket.Client;
using Binance.Client.Websocket.Subscriptions;
using Binance.Client.Websocket.Websockets;

namespace CryptoWigdet
{
    class BinanceHandler
    {
        public static string ApiLink24hTicker = "https://www.binance.com/api/v3/ticker/24hr";
        public static string ApiLinkTicker = "https://binance.com/api/v3/ticker/price";
        private static DispatcherTimer _TimerUpdater;
        public static bool isUpdaterAlive = false;
        private static PairFullInfo[] _currenciesInfo;
        
        public static PairFullInfo[] CurrenciesInfo
        {
            get
            { //"{\"code\":-1003,\"msg\":\"Too much request weight used; current limit is 1200 request weight per 1 MINUTE. Please use the websocket for live updates to avoid polling the API.\"}"
                if (isUpdaterAlive == false)
                {
                    isUpdaterAlive = true;
                    UpdaterCurrs().Start();
                }
                return _currenciesInfo;
            }
        }

        private static Thread UpdaterCurrs()
        {
            var th = new Thread(() => {
                while (isUpdaterAlive)
                {
                    try
                    {

                        var client = new RestClient(ApiLink24hTicker);
                        var request = new RestRequest(Method.GET);
                        var qur = client.Execute(request).Content;
                        JsonSerializer sr = new JsonSerializer();
                        _currenciesInfo = JsonConvert.DeserializeObject<PairFullInfo[]>(qur);
                    }
                    catch 
                    {
                        
                    }
                    Thread.Sleep(800);
                }
            });
            return th;
        }

        /*public static async void Connect()
        {

            var exitEvent = new ManualResetEvent(false);
            
            var communicator = new BinanceWebsocketCommunicator(url);
            var client = new BinanceWebsocketClient(communicator);

       
            {
                
                    
                    //client.Streams.TradesStream.Subscribe(response =>
                    //{
                    //    var trade = response.Data;
                    //    Console.WriteLine($"Trade executed [{trade.Symbol}] price: {trade.Price}");
                    //});

                    //client.SetSubscriptions(
                    //    new TradeSubscription("btcusdt"),
                    //    new TradeSubscription("ethbtc"),
                    //    new TradeSubscription("bnbbtc")
            
                    //);
                    await communicator.Start();

                    exitEvent.WaitOne(TimeSpan.FromSeconds(30));
                
            }
        }*/
        public static bool IsCurrencyValid(string cn)
        {
            foreach (PairFullInfo pair in CurrenciesInfo)
            {
                if (pair.symbol == cn)
                    return true;
            }
            return false;
        }


    }
}
