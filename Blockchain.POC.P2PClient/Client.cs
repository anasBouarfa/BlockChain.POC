using Blockchain.POC.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Blockchain.POC.Manager;
using WebSocketSharp;

namespace Blockchain.POC.P2PClient
{
    public class Client
    {
        private IGlobalManager _globalManager;

        public Client(IGlobalManager globalManager)
        {
            _globalManager = globalManager;
        }
        public WebSocket Connect(string url, BlockChain chain)
        {
            WebSocket ws = new WebSocket(url);
            ws.OnMessage += (sender, e) =>
            {
                if (e.Data == "Hi Client")
                {
                    Console.WriteLine(e.Data);
                }
                else
                {
                    BlockChain newChain = JsonConvert.DeserializeObject<BlockChain>(e.Data);

                    if (_globalManager.IsLocalBlockChainUpToDate(chain, newChain))
                    {
                        List<Transaction> newTransactions = new List<Transaction>();
                        newTransactions.AddRange(newChain.PendingTransactions);
                        newTransactions.AddRange(chain.PendingTransactions);

                        newChain.PendingTransactions?.AddRange(chain.PendingTransactions);

                        chain = newChain;

                        _globalManager.SaveBlockChain(chain);
                    }
                }
            };

            ws.Connect();

            if (chain != null)
            {
                ws.Send(JsonConvert.SerializeObject(chain));
            }

            return ws;
        }
    }
}
